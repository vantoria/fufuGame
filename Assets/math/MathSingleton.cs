using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathSingleton : MonoBehaviour
{

   　public static MathSingleton instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Vector2 nomalize(Vector2 move)//正規化
    {
        float z = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        move = new Vector2(move.x / z, move.y / z);
        return move;
    }
    public float magnitude(Vector2 vec)//斜辺の長さ
    {
        float z = Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
        return z;
    }
    public float distance(Vector2 vec1, Vector2 vec2)//距離（B,A）AからBまでの距離
    {
        float z = magnitude(vec1 - vec2);
        return z;
    }
    public Vector2 addition_radian(Vector2 Move, float rad)//加法定理 (ラジアン)
    {
        Vector2 thisVec;
        rad = rad * 180 / Mathf.PI;
        thisVec.x = Move.x * Mathf.Cos(rad) - Move.y * Mathf.Sin(rad);
        thisVec.y = Move.x * Mathf.Sin(rad) + Move.y * Mathf.Cos(rad);

        return thisVec;
    }
    public Vector2 addition_degree(Vector2 Move, float deg)//加法定理 (デグリー)
    {
        Vector2 thisVec;
        thisVec.x = Move.x * Mathf.Cos(deg) - Move.y * Mathf.Sin(deg);
        thisVec.y = Move.x * Mathf.Sin(deg) + Move.y * Mathf.Cos(deg);

        return thisVec;
    }
    public float dot(Vector2 Vec1, Vector2 Vec2)//内積(角度)
    {
        float f = Vec1.x * Vec2.x + Vec1.y * Vec2.y;
        if (f >= 1) f = 1;
        else if (f <= -1) f = -1;
        float i = Mathf.Acos(f);
        return i;
    }
    public float cross(Vector2 Vec1, Vector2 Vec2)//外積(左右判定　＋→左、－→右)
    {
        if (Vec1 == Vec2) return 0;
        return (Vec1.x * Vec2.y - Vec1.y * Vec2.x);
    }
    public float radian(float deg)//度→ラジアン
    {
        deg = deg * Mathf.PI / 180;
        return deg;
    }
    public float degree(float rad)//ラジアン→度
    {
        rad = rad * 180 / Mathf.PI;
        return rad;
    }



}
