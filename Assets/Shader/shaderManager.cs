using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderManager : MonoBehaviour {

    public Material material;

    public float fade = 0;
    public float shack = 0;

    public enum E_FadeMode { FadeIn,FadeOut, Default};
    public E_FadeMode fadeMode = E_FadeMode.FadeIn;//今のフェードモード


    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, material);
    }


    void Start ()
    {
        material.SetFloat("_fade", fade);

        //material.SetFloat("_shake", shack);
        //material.SetFloat("_haba", 0.1f);


        FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
            //shack ++;
            //material.SetFloat("_shake", shack);

        
    }

    public void FadeOut()//明るくなる
    {
        fadeMode = E_FadeMode.FadeOut;
        StartCoroutine(fadeOutCor());
    }
    IEnumerator fadeOutCor()
    {

        yield return new WaitForSeconds(0.01f);


        if (fadeMode == E_FadeMode.FadeOut)
        {
            fade += 0.01f;
            material.SetFloat("_fade", fade);

            //1より小さければ再帰処理
            if (fade < 1) StartCoroutine(fadeOutCor());
            else fadeMode = E_FadeMode.Default;
        }
    }


    public void FadeIn()//暗くなる
    {
        fadeMode = E_FadeMode.FadeIn;
        StartCoroutine(fadeInCor());
    }
    IEnumerator fadeInCor()
    {

        yield return new WaitForSeconds(0.01f);

        if (fadeMode == E_FadeMode.FadeIn)
        {
            fade -= 0.01f;
            material.SetFloat("_fade", fade);

            //0より大きければ再帰処理
            if (fade > 0) StartCoroutine(fadeInCor());
            else fadeMode = E_FadeMode.Default;
        }
    }
}
