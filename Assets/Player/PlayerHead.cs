using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{

    //テッシュ情報
    private GameObject TissueObj;
    //基準ベクトル
    Vector3 baseVec = new Vector3(1, 0, 0);

    //=============================================================================
    //  初期化
    //=============================================================================
    void Start ()
    {
        //ティッシュ情報
        TissueObj = GameObject.Find("Cube");
        baseVec = new Vector3(1, 0, 0);
    }

    //=============================================================================
    //  更新
    //=============================================================================
    void Update ()
    {
        //transform.rotation = Quaternion.Euler(x, 0, 0);
        //Vector3 rotVec = (TissueObj.transform.position - transform.position).normalized;
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, rotVec);


        Vector3 euler = this.transform.eulerAngles;//Playerの角度

        //Playerの位置
        Vector3 pos = this.transform.position;
        //対象の位置
        Vector3 tissuePos = TissueObj.transform.position;

        //Playerから見た対象の方向ベクトル
        Vector3 vec = (tissuePos - pos).normalized;

        //内積
        //角度が求めれる
        //Mathf.Acos(Vec1.x * Vec2.x + Vec1.y * Vec2.y)
        // float r = dot(vec, baseMove2);
        float r = Mathf.Acos(vec.x * baseVec.x + vec.y * baseVec.y);

         //外積
         //+/-で左右判定
         //Vec1.x * Vec2.y - Vec1.y * Vec2.x
         //float _cross = cross(baseMove2, vec);
        float cross = vec.x * baseVec.y - vec.y * baseVec.x;

        //左右判定
        if (cross < 0)
        {
            //右
            r *= -1;
        }

        //ラジアン→度
        euler.z = r * 180 / Mathf.PI;


        //Debug.Log(euler);
        //代入
        this.transform.eulerAngles = euler;
    }
}
