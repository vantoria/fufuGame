using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathGauge : MonoBehaviour {
    
    //Playerのブレス    
    public BreathManager breathManager;

    //ゲージのSprite
    public SpriteRenderer greenSR;
    public SpriteRenderer redSR;

    //ゲージの最大値の時の長さ
    const float MAX_GAUGE = 5;

    float useBreath;//これから使用するブレス
    float haveBreath;//持っているブレス

    void start()
    {
    }
    
    void Update()
    {
        //値受け取り
        useBreath = breathManager.GetBreathPower();
        haveBreath = breathManager.GetBreathMeter();

        //長さ調整

        if (useBreath == 0)
        {
            Vector2 vec = new Vector2(1, haveBreath * MAX_GAUGE);
            greenSR.size = vec;
            redSR.size = vec;
        }
        else
        {
            Vector2 vec = new Vector2(1, (haveBreath - useBreath) * MAX_GAUGE);
            greenSR.size = vec;
            vec = new Vector2(1, haveBreath * MAX_GAUGE);
            redSR.size = vec;
        }
        
    }

}
