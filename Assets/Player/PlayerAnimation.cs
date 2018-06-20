using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    Animator anim;


    public enum E_status
    {
        idle,//待機
        walk,//前進
        walkback,//後退
        breath,//息を吐く
        breath1,
        squrt,//しゃがみ
        squrt1,
        squrtwalk,//しゃがみ前
        squrtbackwalk,//しゃがみ後ろ
        squrtbreath,//しゃがみ息
        jump,//ジャンプ
        breathcharge

    }

    [SerializeField]private PlayerController playerController;

    


    void Start()
    {
        playerController = this.gameObject.GetComponent<PlayerController>();
        anim = this.GetComponent<Animator>();
    }
   
    void Update()
    {

        if (playerController.GetBreathUpFlg() == true)
        {
            anim.SetBool("Isbreathflg", true);
        }
        else
        {
            anim.SetBool("Isbreathflg", false);
        }

        if (playerController.GetBreathChargeFlg() == true)
        {
            anim.SetBool("Isbreathcharge", true);
        }
        else
        {
            anim.SetBool("Isbreathcharge", false);
        }


        if (playerController.GetMove().x > 0)
            {
                anim.SetBool("Iswalk", true);
            }
            else
            {
                anim.SetBool("Iswalk", false);
            }


            if (playerController.GetMove().y < 0)
            {
                anim.SetBool("Issqurt", true);
            }
            else
            {
                anim.SetBool("Issqurt", false);
            }


            if (playerController.GetMove().x < 0)
            {
                anim.SetBool("Iswalkback", true);
            }
            else
            {
                anim.SetBool("Iswalkback", false);
            }


            if (playerController.GetMove().y > 0)
            {
                anim.SetBool("Isjump", true);
            }
            else
            {
                anim.SetBool("Isjump", false);
            }

            

        


        /*if(Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Iswalk", true);
        }
        else
        {
            anim.SetBool("Iswalk", false);
        }
       


        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Iswalkback", true);
        }
        else
        {
            anim.SetBool("Iswalkback", false);
        }



        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Isjump", true);
        }
        else
        {
            anim.SetBool("Isjump", false);
        }
       


        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("Isbreathcharge", true);
        }
        else
        {
            anim.SetBool("Isbreathcharge", false);
        }
    


        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Issqurt", true);
        }
        else
        {
            anim.SetBool("Issqurt", false);
        }



        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Isbreathflg", true);
        }
        else
        {
            anim.SetBool("Isbreathflg", false);
        }


       
        if (playerController.GetBreathUpFlg() == true)
        {
            AnimChange(E_status.breath);
        }
        //攻撃アニメーション中ではない
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("BreathFlg") == false)
        {
            //移動キーが入力されているか
            if (playerController.GetMove().x != 0) //零じゃないなら
            {
                if (playerController.GetMove().y != 0)//零じゃないなら
                {
                    if (playerController.GetMove().y < 0)//0よりy小さい
                    {
                        if (playerController.GetMove().x < 0) //0よりx小さい
                        {
                            AnimChange(E_status.squrtbackwalk);
                        }
                        else
                        {
                            AnimChange(E_status.squrtwalk);
                        }
                    }
                    else
                    {
                        if (playerController.GetMove().x > 0)
                        {
                            AnimChange(E_status.walk);
                        }
                        else
                        {
                            AnimChange(E_status.walkback);
                        }
                    }

                }
                else
                {
                    if (playerController.GetMove().y != 0)
                    {
                        if (playerController.GetMove().y < 0)
                        {
                            AnimChange(E_status.squrt);
                        }
                        else
                        {
                            AnimChange(E_status.jump);
                        }
                    }
                    else
                    {
                        AnimChange(E_status.idle);
                    }

                }    
                


            }
           
        }*/
    }


    void AnimChange(E_status _statusRef)
    {
        switch (_statusRef)
        {
            case E_status .idle:
                anim.CrossFade("Idle", 0);
                break;
            case E_status.walk:
                anim.CrossFade("Walk", 0);
                break;
            case E_status.walkback:
                anim.CrossFade("Walkback", 0);
                break;
                /*case E_status.squrt:
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Squrtwalk") == true|| anim.GetCurrentAnimatorStateInfo(0).IsName("Squrtwalkback") == true|| anim.GetCurrentAnimatorStateInfo(0).IsName("Squrtbreath") == true || anim.GetCurrentAnimatorStateInfo(0).IsName("Squrt1") == true)
                    {
                        Debug.Log("座りから");
                        anim.CrossFade("Squrt1", 0);
                    }
                    else
                    {
                        Debug.Log("立ちから");
                        anim.CrossFade("Squrt", 0);
                    }*/
               
            case E_status.squrt:
                anim.CrossFade("Squrt", 0);

                break;
            case E_status.squrt1:
                anim.CrossFade("Squrt1", 0);
                break;
            case E_status.breath:
                anim.CrossFade("BreathFlg", 0);
                break;
            case E_status.breath1:
                anim.CrossFade("BreathFlg1", 0);
                break;
            case E_status.breathcharge:
                anim.CrossFade("Breathcharge", 0);
                break;
            case E_status.squrtwalk:
                anim.CrossFade("Squrtwalk", 0);
                break;
            case E_status.squrtbackwalk:
                anim.CrossFade("Squrtwalkback", 0);
                break;
            case E_status.squrtbreath:
                anim.CrossFade("Squrtbreath", 0);
                break;
            case E_status.jump:
                anim.CrossFade("Jump", 0);
                break;
            default:
                break;
        }
    }
}
