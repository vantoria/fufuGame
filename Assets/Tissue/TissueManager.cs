using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TissueManager : MonoBehaviour
{
    public RuntimeAnimatorController rideAnim, fallAnim;

	private float speed = 0.08f;
    private float rideDown,rideRot;

    private float eulerRot = 3f;
    private Vector3 move;
    private Vector3 pos;
    private Vector3 euler;
    private float timer;

    //基準ベクトル
    private Vector3 baseMove1 = new Vector3(0, 1, 0);
    private Vector3 baseMove2 = new Vector3(1, 0, 0);

    Coroutine coroutine;

    public bool gameOverFlg = false;//ゲームオーバー


    private GameObject lowObj1, highObj1;
    private Vector3 lowPoint1, highpoint1;
    private GameObject lowObj2, highObj2;
    private Vector3 lowPoint2, highpoint2;
    private GameObject boxHigh;
    private Vector3 boxHighPoint;

    public enum E_Area
    {
        Player1,
        Player2,
        Center
    }
    public E_Area area;

    public enum E_mode
    {
        Glide,
        Roll,
        Stop,
        Ride
    }
    public E_mode mode = E_mode.Glide;


    IEnumerator Start()
    {
        enabled = false;
        yield return new WaitForSeconds(2);
        enabled = true;

        pos = this.transform.position;
        euler = this.transform.eulerAngles;
        timer = 0.4f;
        coroutine = StartCoroutine(delayGlide(timer));

        lowObj1 = GameObject.Find("P1Low");
        highObj1 = GameObject.Find("P1High");

        lowPoint1 = lowObj1.transform.position;
        highpoint1 = highObj1.transform.position;

        lowObj2 = GameObject.Find("P2Low");
        highObj2 = GameObject.Find("P2High");

        lowPoint2 = lowObj2.transform.position;
        highpoint2 = highObj2.transform.position;

        boxHigh = GameObject.Find("BoxHigh");

        boxHighPoint = boxHigh.transform.position;


    }

	void Update()
	{

        pos = this.transform.position;
        euler = this.transform.eulerAngles;

        float r = euler.z;
        if (r > 180) r -= 360;

        switch (mode)
        {
            case E_mode.Glide:

                //画像の傾きから移動方向を算出
                move = addition(baseMove2, radian(r)).normalized;
                break;

            case E_mode.Roll:

                //画像の傾きから移動方向を算出
                move = addition(baseMove2, radian(r)).normalized;
                //画像を傾かせる
                euler.z += eulerRot;
                //速度を下げる
                speed -= 0.006f;
                if(speed <=0)
                {
                    speed = 0;
                    mode = E_mode.Stop;
                    coroutine = StartCoroutine(delayStop(0.1f));
                }

                break;
            case E_mode.Stop:
                break;

            case E_mode.Ride:
                if (cross(baseMove1, move) < 0)
                {
                    move = addition(move, radian(-rideRot)).normalized;
                }
                else
                {
                    move = addition(move, radian(rideRot)).normalized;
                }
                break;
            default:
                break;
        }


        Vector3 vecMove = move;

        if(pos.x >= highpoint1.x && pos.y<=boxHighPoint.y&& area == E_Area.Player1)
        {
            if (vecMove.x >= 0)
            {
                vecMove.x = 0;

            }
        }
        else if (pos.x <= lowPoint2.x && pos.y <= boxHighPoint.y&& area == E_Area.Player2)
        {
            if(vecMove.x<=0)
            {
                vecMove.x = 0;
            }
        }
        else if(pos.y<=boxHighPoint.y&&area == E_Area.Center)
        {
            vecMove.y = 0;
        }

        Debug.Log(vecMove);
        pos += vecMove * speed;
        this.transform.position = pos;
        this.transform.eulerAngles = euler;


        //位置判定
        if (pos.x < highpoint1.x)
        {
            area = E_Area.Player1;
        }
        else if (pos.x > lowPoint2.x)
        {
            area = E_Area.Player2;
        }
        else
        {
            area = E_Area.Center;
        }

        if(pos.y < lowPoint1.y)
        {
            enabled = false;
            gameOverFlg = true;
        }


    }

    IEnumerator delayGlide(float _timer)
    {
        yield return new WaitForSeconds(_timer);
        _timer *= 0.8f;
        mode = E_mode.Roll;

    }
    IEnumerator delayStop(float _timer)
    {
        yield return new WaitForSeconds(_timer);
        mode = E_mode.Glide;
        speed = 0.08f;
        baseMove2 *= -1;
        eulerRot *= -1;
        coroutine = StartCoroutine(delayGlide(timer));

    }

    IEnumerator delayRideDown()
    {
        yield return new WaitForSeconds(0.1f);
        speed -= rideDown;

        if (speed<=0)
        {
            mode = mode = E_mode.Glide;
            speed = 0.08f;
            this.GetComponent<Animator>().runtimeAnimatorController = fallAnim;
            coroutine = StartCoroutine(delayGlide(timer));
        }
        else
        {
            StartCoroutine(delayRideDown());
        }
    }

    public void BreathAddForce(Vector3 vec, float force)
    {
        //Vector3 vec1 = vec.normalized * force;
        //Vector3 vec2 = move * speed;

        //Vector3 vec3 = vec1 + vec2;



        //モード・アニメ変換
        mode = E_mode.Ride;
        this.GetComponent<Animator>().runtimeAnimatorController = rideAnim;

        move = vec.normalized;
        speed = force ;

        rideDown = speed / 8;


        if (cross(baseMove1, move) < 0)
        {
            euler = new Vector3(0, 0, -25);
            this.transform.eulerAngles = euler;
            baseMove2 = new Vector3(1, 0, 0);
            eulerRot = 3;
        }
        else
        {
            euler = new Vector3(0, 0, 25);
            this.transform.eulerAngles = euler;
            baseMove2 = new Vector3(-1, 0, 0);
            eulerRot = -3;
        }
        

        rideRot = dot(baseMove1, move);
        rideRot /= 5;

        StopAllCoroutines();
        StartCoroutine(delayRideDown());
    }


    private Vector3 addition(Vector3 _move, float rot)
	{
		Vector2 thisVec;
        thisVec.x = _move.x * Mathf.Cos(rot) - _move.y * Mathf.Sin(rot);
        thisVec.y = _move.x * Mathf.Sin(rot) + _move.y * Mathf.Cos(rot);


        return new Vector3(thisVec.x, thisVec.y, 0);
	}

    float dot(Vector2 Vec1, Vector2 Vec2)//内積(角度)
    {
        float f = Vec1.x * Vec2.x + Vec1.y * Vec2.y;
        if (f >= 1) f = 1;
        else if (f <= -1) f = -1;
        float i = Mathf.Acos(f);
        return i;
    }
    float cross(Vector2 Vec1, Vector2 Vec2)//外積(左右判定　＋→左、－→右)
    {
        return (Vec1.x * Vec2.y - Vec1.y * Vec2.x);
    }

    float radian(float deg)//度→ラジアン
    {
        deg = deg * Mathf.PI / 180;
        return deg;
    }
    float degree(float rad)//ラジアン→度
    {
        rad = rad * 180 / Mathf.PI;
        return rad;
    }
}
