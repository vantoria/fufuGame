using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    enum PL_STATE
    {
        NON,
        JUMP,
        FALL
    }

    //重力
    private const float GRAVITY_SCALE = 0.01f;

    [SerializeField]
    public const float JUMP_POWER = 0.4f;

    //プレイヤースピード
    [SerializeField]
    public  float playerSpeed;

    //プレイヤー情報
    private PlayerController PlayerCon;

    //最初のプレイヤーY座標
    private float firstPosY;

    //加速度
    private Vector3 velocity;

    PL_STATE state;

    //ジャンプパワー
    private float jumpP;


    //プレイヤー移動制限地点
    private GameObject lowObj, highObj;
    private Vector3 lowPoint, highpoint;

    

    //=============================================================================
    //  初期化
    //=============================================================================
    void Start ()
    {
        //プレイヤー情報
        PlayerCon = GetComponent<PlayerController>();

        //座標設定
        firstPosY = this.transform.position.y;

        velocity = new Vector3(0.0f, 0.0f, 0.0f);

        state = PL_STATE.NON;

        jumpP = JUMP_POWER;


        switch (PlayerCon.mode)
        {
            case PlayerController.E_MODE.PLAYER_1:
                lowObj = GameObject.Find("P1Low");
                highObj = GameObject.Find("P1High");
                break;
            case PlayerController.E_MODE.PLAYER_2:
                lowObj = GameObject.Find("P2Low");
                highObj = GameObject.Find("P2High");
                break;
            default:
                break;
        }
        lowPoint = lowObj.transform.position;
        highpoint = highObj.transform.position;
    }


    //=============================================================================
    //  更新
    //=============================================================================
    void Update()
    {
        // x成分は初期化でyは保持
        velocity = new Vector2(0, velocity.y);


        //Chargeボタンが押されていなければ
        if(!PlayerCon.GetBreathChargeFlg())
        {
            //右に移動であれば
            if (PlayerCon.GetMove().x == 1)
            {
                if (this.transform.position.x <= highpoint.x)
                {
                    velocity.x += playerSpeed;
                }

                //仮移動
            }
            //左に移動であれば
            else if (PlayerCon.GetMove().x == -1)
            {
                if (this.transform.position.x >= lowPoint.x)
                {
                    //仮移動
                    velocity.x -= playerSpeed;
                }
            }

            //ジャンプ
            if ((PlayerCon.GetMove().y == 1) && state == PL_STATE.NON)
            {

                state = PL_STATE.JUMP;
                //ジャンプ力をセット
                velocity.y = JUMP_POWER;
            }

            //ジャンプ中であれば
            if (state == PL_STATE.JUMP)
            {
                //ジャンプ処理
                PlayerJump();
            }

            
        }

        //地面にいなかったら
        if (this.transform.position.y > firstPosY)
        {
            //重力適用
            velocity.y -= GRAVITY_SCALE;

            if(PlayerCon.GetBreathChargeFlg() && state != PL_STATE.FALL)
            {
                state = PL_STATE.FALL;
                velocity.y = 0.0f;
            }
            
        }

        //落下中で着地位置であれば
        if (state == PL_STATE.FALL && this.transform.position.y <= firstPosY)
        {
            this.transform.position = new Vector3(this.transform.position.x, firstPosY, this.transform.position.z);
            velocity.y = 0.0f;
            state = PL_STATE.NON;

        }




        //移動

        this.transform.position += velocity;
    }

    //=============================================================================
    //  ジャンプ
    //=============================================================================
    void PlayerJump()
    {
        //ジャンプ頂点まで行ったら
        if (velocity.y < 0 && state != PL_STATE.FALL)
        {
            //初期化
            velocity.y = 0.0f;
            state = PL_STATE.FALL;
        }

        //ジャンプ中であれば
        if (state == PL_STATE.JUMP)
        {
            velocity.y -= 0.02f;
        }
    }


}
