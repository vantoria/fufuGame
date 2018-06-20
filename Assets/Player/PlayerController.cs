using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //=============================================================================
    //  ※ 変数にアクセスしたい場合は一番下のアクセサ関数から呼び出してください※
    //=============================================================================

    //移動判断値(今のところx値のみ使用)
    private Vector2 move;
    //息はく押下判断
    private bool    breathFlg;
    private bool    breathUpFlg;
    //息チャージ押下判断
    private bool breathChargeFlg;


    public enum E_MODE
    {
        PLAYER_1,
        PLAYER_2
    }

    public enum CONTROLLER_MODE
    {
        KEYBOARD,
        JOYPAD
    }

    //何Pか
    public E_MODE mode;
    //コントローラー
    private CONTROLLER_MODE controller;

    //=============================================================================
    //  初期化
    //=============================================================================
    IEnumerator Start ()
    {
        enabled = false;
        yield return new WaitForSeconds(2);
        enabled = true;

        //初期化
        move        = new Vector2( 0.0f, 0.0f);
        breathFlg   = false;
        breathUpFlg = false;
        

        breathChargeFlg = false;

        ControllerSearch();

    }

    //=============================================================================
    //  更新
    //=============================================================================
    void Update ()
    {
        switch (mode)
        {
            case E_MODE.PLAYER_1:
                {
                    if(controller == CONTROLLER_MODE.KEYBOARD)
                    {
                        ConKeyboard1P();
                    }
                    else
                    {
                        ConJoypad1P();
                    }
                } 
            break;

            case E_MODE.PLAYER_2:
                {
                    if (controller == CONTROLLER_MODE.KEYBOARD)
                    {
                        ConKeyboard2P();
                    }
                    else
                    {
                        ConJoypad2P();
                    }
                }
            break;

            default:
            break;

        }
       
    }
    //=============================================================================
    //  操作 : キーボード 1P
    //=============================================================================
    void ConKeyboard1P()
    {
        //移動
        
        //【 Dキー 】
        if (Input.GetKey(KeyCode.D))
        {
            //右に移動
            move.x = 1.0f;
        }
        //【 Aキー 】
        else if (Input.GetKey(KeyCode.A))
        {
            //左に移動
            move.x = -1.0f;
        }
        else
        {
            //0で初期化
            move.x = 0.0f;
        }

        //ジャンプ
        //【 Wキー 】
        if (Input.GetKeyDown(KeyCode.W))
        {
            move.y = 1.0f;
        }
        //しゃがみ
        //【 Sキー 】
        else if (Input.GetKey(KeyCode.S))
        {
            move.y = -1.0f;
        }
        else
        {
            move.y = 0.0f;
        }



        //息

        //【 Eキー 】
        if (Input.GetKey(KeyCode.E))
        {
            

            breathChargeFlg = true;
        }
        else
        {
            breathChargeFlg = false;
        }



        //【 スペースキー 】
        if (Input.GetKey(KeyCode.Space))
        {
            breathFlg = true;
        }
        else breathFlg = false;

        
    

        //【 スペースキー 】離した瞬間
        if (Input.GetKeyUp(KeyCode.Space))
        {
            breathUpFlg = true;
        }
        else breathUpFlg = false;
    }
    //=============================================================================
    //  操作 : キーボード 2P
    //=============================================================================
    void ConKeyboard2P()
    {
        //移動
        //【 →キー 】
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //右に移動
            move.x = 1.0f;
        }
        //【 ←キー 】
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //左に移動
            move.x = -1.0f;
        }
        else
        {
            //0で初期化
            move.x = 0.0f;
        }

        //ジャンプ
        //【 ↑キー 】
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            move.y = 1.0f;
        }
        //しゃがみ
        //【 ↓キー 】
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move.y = -1.0f;
        }
        else
        {
            move.y = 0.0f;
        }


        //チャージ
        //【 テンキー1】
        if (Input.GetKey(KeyCode.Keypad1))
        {
            breathChargeFlg = true;
        }
        else
        {
            breathChargeFlg = false;
        }

        //息
        //【 右シフトキー 】
        if (Input.GetKey(KeyCode.RightShift))
        {
            breathFlg = true;
        }
        else breathFlg = false;

        //【 右シフトキー 】離した瞬間
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            breathUpFlg = true;
        }
        else breathUpFlg = false;
    }

    //=============================================================================
    //  操作 : ジョイパッド 1P
    //=============================================================================
    void ConJoypad1P()
    {
        //移動
        //【 スティック右倒し 】
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            //右に移動
            move.x = 1.0f;
        }
        //【 スティック左倒し 】
        else if (Input.GetAxis("Horizontal") < 0.0f)
        {
            //右に移動
            move.x = -1.0f;
        }
        else
        {
            //0で初期化
            move.x = 0.0f;
        }

        //しゃがみ
         //【 スティック下倒し 】
        if (Input.GetAxis("Vertical") > 0.0f)
        {
            //しゃがみ
            move.y = -1.0f;
        }
        //ジャンプ
        //【 A 】押されていたら
        else if (Input.GetButton("Jump"))
        {
            move.y = 1.0f;
        }
        else
        {
            move.y = 0.0f;
        }

        //息

        //ﾁｬｰｼﾞ
        if (Input.GetButton("Charge"))
        {
            breathChargeFlg = true;
        }
        else
        {
            breathChargeFlg = false;
        }

        //【 B 】押されていたら
        if (Input.GetButton("1P_Breath"))
        {
            breathFlg = true;
        }
        else breathFlg = false;

        //【 B 】離した瞬間
        if (Input.GetButtonUp("1P_Breath"))
        {
            breathUpFlg = true;
        }
        else breathUpFlg = false;
    }

    //=============================================================================
    //  操作 : ジョイパッド 2P
    //=============================================================================
    void ConJoypad2P()
    {
        //移動
        //【 スティック右倒し 】
        if (Input.GetAxis("Horizontal2") > 0.0f)
        {
            //右に移動
            move.x = 1.0f;
        }
        //【 スティック左倒し 】
        else if (Input.GetAxis("Horizontal2") < 0.0f)
        {
            //左に移動
            move.x = -1.0f;
        }
        else
        {
            //0で初期化
            move.x = 0.0f;
        }

        //しゃがみ
        //【 スティック下倒し 】
        if (Input.GetAxis("Vertical2") > 0.0f)
        {
            //しゃがみ
            move.y = -1.0f;
        }
        //ジャンプ
        //【 A 】押されていたら
        else if (Input.GetButton("Jump2"))
        {
            move.y = 1.0f;
        }
        else
        {
            move.y = 0.0f;
        }

        //息

        //ﾁｬｰｼﾞ
        if (Input.GetButton("Charge2"))
        {
            breathChargeFlg = true;
        }
        else
        {
            breathChargeFlg = false;
        }

        //【 B 】押されていたら
        if (Input.GetButton("2P_Breath"))
        {
            breathFlg = true;
        }
        else breathFlg = false;

        //【 B 】離した瞬間
        if (Input.GetButtonUp("2P_Breath"))
        {
            breathUpFlg = true;
        }
        else breathUpFlg = false;
    }

    //=============================================================================
    //  コントローラー接続確認
    //=============================================================================
    void ControllerSearch()
    {
        var controllerNames = Input.GetJoystickNames();

        int cunCount = 0;

        for (int i = 0; i < controllerNames.Length; i++)
        {
            if (controllerNames[i] != "")
            {
                cunCount++;
            }

        }

        if (cunCount > 0)
        {
            if (cunCount == 1)
            {
                // 1p はパッド
                if (mode == E_MODE.PLAYER_1)
                {
                    controller = CONTROLLER_MODE.JOYPAD;
                }
                // 2p はキーボード
                else controller = CONTROLLER_MODE.KEYBOARD;

               
            }
            else if (cunCount == 2)
            {
                controller = CONTROLLER_MODE.JOYPAD;
                
            }
            else
            {
                //何もしない
            }
        }
        else
        {
            controller = CONTROLLER_MODE.KEYBOARD;
            
            
        }


        
       
    }

    //=============================================================================
    //  アクセサ
    //=============================================================================

    //移動判断値
    public Vector2 GetMove() { return move; }
    //息ボタン押下判断(押されていたら)
    public bool GetBreathFlg() { return breathFlg; }
    //息ボタン押下判断(離した瞬間)
    public bool GetBreathUpFlg() { return breathUpFlg; }
    //息ボタンチャージ
    public bool GetBreathChargeFlg() { return breathChargeFlg; }

    

}
