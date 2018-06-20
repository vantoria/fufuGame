using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultAnim : MonoBehaviour {

    Animator anim;

    public enum E_mode
    {
        win,
        lose,
        num
    }


	void Start () {
        anim = this.GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void animPlay(E_mode mode)
    {
        switch (mode)
        {
            case E_mode.win:
                anim.CrossFade("win", 0);
                break;
            case E_mode.lose:
                anim.CrossFade("lose", 0);
                break;
            case E_mode.num:
                break;
            default:
                break;
        }
    }
}
