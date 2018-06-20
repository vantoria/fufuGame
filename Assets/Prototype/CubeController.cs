using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public Vector3 pos;//位置
    public Vector3 move;//移動方向
    public float rot;//ハンドル
    public float speed;//速さ

    private Vector3 baseVec = new Vector3(0, -1, 0);

    IEnumerator Start()
    {
        enabled = false;
        yield return new WaitForSeconds(2);
        enabled = true;

        pos = this.transform.position;
        move = new Vector3(-1, 0, 0);
        speed = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        pos += move * speed;
        this.transform.position = pos;

        float _dot = dot(baseVec, move);
        if (cross(baseVec,move)<0)
        {
            if(_dot < 135&& _dot > 45)
            {
                rot = 0.05f;
            }
            else
            {
                rot = 0.02f;
            }
            
        }
        else
        {
            if (_dot < 135 && _dot > 45)
            {
                rot = -0.05f;
            }
            else
            {
                rot = -0.02f;
            }
            
        }
        if(dot(baseVec,move)<90)
        {
            speed += 0.001f;
            if (speed >= 0.05f) speed = 0.05f;
        }
        else
        {
            speed -= 0.0001f;
            if (speed < 0) speed = 0;
        }

        move = addition(move, rot).normalized;

	}

    public void BreathAddForce(Vector3 vec,float force)
    {
        Vector3 vec1 = vec.normalized * force;
        Vector3 vec2 = move * speed;

        Vector3 vec3 = vec1 + vec2;

        move = vec3.normalized;
        speed = force*1.5f;
    }

    private Vector3 addition(Vector3 _move, float rot)
    {
        Vector2 thisVec;
        thisVec.x = _move.x * Mathf.Cos(rot) - _move.y * Mathf.Sin(rot);
        thisVec.y = _move.x * Mathf.Sin(rot) + _move.y * Mathf.Cos(rot);

        return new Vector3(thisVec.x, thisVec.y, 0);
    }
    float cross(Vector2 Vec1, Vector2 Vec2)//外積(左右判定　＋→左、－→右)
    {
        return (Vec1.x * Vec2.y - Vec1.y * Vec2.x);
    }
    float dot(Vector2 Vec1, Vector2 Vec2)//内積(角度)
    {
        float i = Mathf.Acos(Vec1.x * Vec2.x + Vec1.y * Vec2.y);
        return i;
    }
    float magnitude(Vector3 vec)//斜辺の長さ
    {
        float z = Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
        return z;
    }
}
