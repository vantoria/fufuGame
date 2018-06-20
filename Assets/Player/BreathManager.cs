using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathManager : MonoBehaviour {

    //=============================================================================
    //  ※ 変数にアクセスしたい場合は一番下のアクセサ関数から呼び出してください※
    //=============================================================================

    //息パワーMAX秒(この値秒でMAX値に到達)
    public const int   PLAYER_BREATH_SECOND           = 1;
    //息パワーMAX値(メーター値)
    public const float PLAYER_BREATH_MAX              = 1.0f;
    public const float PLAYER_BREATH_ONE_PERCENT      = (PLAYER_BREATH_MAX / (60.0f * PLAYER_BREATH_SECOND));

    [SerializeField]
    public float breathRecoverySpeed;

    //息距離MAX
    public const float PLAYER_BREATH_DEST_MAX = 8.0f;

    public const float PLAYER_BREATH_DEST_ONE_PERCENT = 1.0f / PLAYER_BREATH_DEST_MAX;


    //息パワー
    private float breathPower;
    //息メーター
    private float breathMeter;
    private float breathMeterSave;
    //息(距離とパワー)
    private float breath;
    //ティッシュと息との距離
    private float dest;

    //PlayerController
    private PlayerController PlayerCon;
    //テッシュ情報
    private GameObject TissueObj;
    //息あたり判定
    private bool HitbreathFlg;

    public GameObject head;

    public BannerManager BM;



    //=============================================================================
    //  初期化
    //=============================================================================
    void Start ()
    {
        //プレイヤー情報
        //GameObject playerObj = GameObject.Find("Player");
        PlayerCon            = this.GetComponent<PlayerController>();
        //ティッシュ情報
        //TissueObj =  GameObject.Find("Cube");
        TissueObj = GameObject.Find("Tissue");

        //息パワー
        breathPower = 0.0f;
        //息メーター
        breathMeter = PLAYER_BREATH_MAX;
        //息(距離とパワー)
        breath = 0.0f;
        //距離
        dest = 0.0f;
        //あたり判定
        HitbreathFlg = false;

        breathMeterSave = breathMeter;


    }

    //=============================================================================
    //  更新
    //=============================================================================
    void Update ()
    {

        

        //息キーが押されて尚且つチャージボタンは押されていない
        if(PlayerCon.GetBreathFlg() && !PlayerCon.GetBreathChargeFlg())
        {
            

            //MAX値にならない限り
            if (breathPower < breathMeter)
            {
                breathPower += PLAYER_BREATH_ONE_PERCENT;

                //breathMeter -= PLAYER_BREATH_ONE_PERCENT;

            }
            else breathPower = breathMeter;
        }
        else //離されている
        {
            //息キーを離した瞬間だったら
            if (PlayerCon.GetBreathUpFlg() && breathMeter>=0.05f)
            {
                BM.startCutIn();

                //ここにメーター消費があった
                //メーター消費
                breathMeter = breathMeter - breathPower;
                //ティッシュ間との距離を取得
                dest = Vector3.Distance(head.transform.position, TissueObj.transform.position);

                //距離を見てパワー再調整-------------------------------------------

                //距離が息の届く距離であれば
                if (dest <= PLAYER_BREATH_DEST_MAX)
                {

                    HitbreathFlg = true;
                    //もともとの息
                    breath = breathPower;
                    //距離を換算したときの息
                    breath = breath - (breath * (dest * PLAYER_BREATH_DEST_ONE_PERCENT));
                   // Debug.Log("breath" + breath);

                    //ここからプロトタイプ
                    //TissueObj.GetComponent<CubeController>().BreathAddForce(TissueObj.transform.position - head.transform.position, breath);
                    TissueObj.GetComponent<TissueManager>().BreathAddForce(TissueObj.transform.position - head.transform.position, breath);
                }
            }

            //もしどちらもおされていたら
            if(PlayerCon.GetBreathChargeFlg() && PlayerCon.GetBreathFlg())
            {
                breathMeter = breathMeter - breathPower;

                //ティッシュ間との距離を取得
                dest = Vector3.Distance(head.transform.position, TissueObj.transform.position);

                //距離を見てパワー再調整-------------------------------------------

                //距離が息の届く距離であれば
                if (dest <= PLAYER_BREATH_DEST_MAX)
                {

                    HitbreathFlg = true;
                    //もともとの息
                    breath = breathPower;
                    //距離を換算したときの息
                    breath = breath - (breath * (dest * PLAYER_BREATH_DEST_ONE_PERCENT));
                    // Debug.Log("breath" + breath);

                    //ここからプロトタイプ
                    //TissueObj.GetComponent<CubeController>().BreathAddForce(TissueObj.transform.position - head.transform.position, breath);
                    TissueObj.GetComponent<TissueManager>().BreathAddForce(TissueObj.transform.position - head.transform.position, breath);
                }
            }

            else HitbreathFlg = false;

            breathPower = 0.0f;

            if(breathMeter < PLAYER_BREATH_MAX)
            {
                //switch (TissueObj.GetComponent<TissueManager>().area)
                //{
                //    case TissueManager.E_Area.Player1:
                //        if(PlayerCon.mode == PlayerController.E_MODE.PLAYER_2) breathMeter += breathRecoverySpeed;
                //        break;
                //    case TissueManager.E_Area.Player2:
                //        if (PlayerCon.mode == PlayerController.E_MODE.PLAYER_1) breathMeter += breathRecoverySpeed;
                //        break;
                //    case TissueManager.E_Area.Center:
                //        break;
                //    default:
                //        break;
                //}

                //Chargeボタンが押されている尚且つ息吐きボタンが押されていなかったら
                if(PlayerCon.GetBreathChargeFlg() && !PlayerCon.GetBreathFlg())
                {

         
                    //仮値
                    breathMeter += breathRecoverySpeed;
                }
                
            }
        }

        //もし値がきれいに割り切れてない場合
        if (breathPower > breathMeter)
        {
            //MAX値に整える
            breathPower = breathMeter;
        }

        //もし値がきれいに割り切れてない場合
        if (breathMeter > PLAYER_BREATH_MAX)
        {
            //MAX値に整える
            breathMeter = PLAYER_BREATH_MAX;
        }
        if (breathMeter < 0.0f)
        {
            //最低値に整える
            breathMeter = 0.0f;
        }

        //Debug.Log("breathMeter" + breathMeter);
        //Debug.Log("breathPower" + breathPower);
        //Debug.Log("HitbreathFlg" + HitbreathFlg);

    }

    //=============================================================================
    //  アクセサ
    //=============================================================================

    //息パワー
    public float GetBreathPower() { return breathPower; }
    //息メーター
    public float GetBreathMeter() { return breathMeter; }
    //息(距離とパワー)
    public float GetBreath() { return breath; }
    //息あたり判定
    public bool GetHitBreathFlg() { return HitbreathFlg; }


}
