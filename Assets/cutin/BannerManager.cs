using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerManager : MonoBehaviour {

	public enum E_mode
    {
        Player1,
        Player2,
        num
    }
    public E_mode mode;


    private Vector3 pos;
    private Vector3 basePos;
    private Vector3 scale;
    private Vector3 baseSize;
    private Vector2 size;

    private float speed = 0.4f;

	void Start ()
    {
        pos = this.transform.position;
        scale = this.transform.localScale;
        basePos = this.transform.position;
        baseSize = this.transform.localScale;
        size = this.GetComponent<SpriteRenderer>().size;
        size *= 0.7f;

        switch (mode)
        {
            case E_mode.Player1:
                break;
            case E_mode.Player2:
                speed *= -1;
                break;
            case E_mode.num:
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void startCutIn()
    {
        StartCoroutine(CutInMove());
    }
    IEnumerator CutInMove()
    {
        yield return new WaitForSeconds(0.001f);
        pos.x += speed;
        transform.position = pos;
        if(Mathf.Abs( basePos.x-pos.x)>=size.x)
        {
            StartCoroutine(Delay());
        }
        else
        {
            StartCoroutine(CutInMove());
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ScaleDown());
    }
    IEnumerator ScaleDown()
    {
        yield return new WaitForSeconds(0.01f);
        scale.y -= 0.05f;
        if(scale.y<=0)
        {
            pos = basePos;
            scale = baseSize;

            this.transform.position = basePos;
            this.transform.localScale = baseSize;
        }
        else
        {
            transform.localScale = scale;
            StartCoroutine(ScaleDown());
        }

    }
}
