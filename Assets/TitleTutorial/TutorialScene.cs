using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScene : MonoBehaviour {

    public GameObject title;
    public float tutMoveSpeed = 5;

    private GameObject tutoCalled;
    private Vector3 initialPos = new Vector3(0, 13.4f, -0.1f);
    private Vector3 Target = new Vector3(0, 1, -0.1f);
    private Vector3 titlePos;
    
    bool isPressed;//ボタン入力受付制限
    bool isMoveUp = true;//
    bool isMoveDown = true;//
    bool isArriveDown;//下降完了
    bool isArriveUp;//上昇完了

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M) && !isPressed && isMoveUp)
        //{
        //    isPressed = true;
        //    isMoveDown = true;
        //    // call out tutorial image
        //    // ボタン押すとチュートリアル画像を呼び出し    
        //    StartCoroutine("TutorialDown");
        //}
        //if (Input.GetKeyDown(KeyCode.N) && isPressed && isMoveDown && isArriveDown)
        //{
        //    isPressed = false;
        //    isMoveUp = false;
        //    //ボタン押すとチュートリアル画像を上がる。
        //    StartCoroutine("TutorialUp");
        //}
    }

    public void spriteDown()
    {
        if (!isPressed && isMoveUp)
        {
            isPressed = true;
            isMoveDown = true;
            // call out tutorial image
            // ボタン押すとチュートリアル画像を呼び出し    
            StartCoroutine("TutorialDown");
        }
    }
    public void spriteUp()
    {
        if (isPressed && isMoveDown && isArriveDown)
        {
            isPressed = false;
            isMoveUp = false;
            //ボタン押すとチュートリアル画像を上がる。
            StartCoroutine("TutorialUp");
        }
    }

    IEnumerator TutorialDown()
    {
        // instantiate thet tutorial gameobject and move downward
        // チュートリアルが画像を呼び出す、画面の真ん中に移動します。
        tutoCalled = Instantiate(title, initialPos, Quaternion.identity) as GameObject;

        while (!isArriveDown)
        {
            titlePos = tutoCalled.gameObject.transform.position = Vector3.MoveTowards(tutoCalled.gameObject.transform.position, Target, tutMoveSpeed * Time.deltaTime);
            if (Vector3.Distance(titlePos, Target) == 0)
            {
                isArriveDown = true;
                isArriveUp = false;
            }
            yield return null;
        }
    }

    IEnumerator TutorialUp()
    {
        // move the tutorial game object up
        //チュートリアル画像を上がって、消します。
        while (!isArriveUp)
        {
            titlePos = tutoCalled.gameObject.transform.position;
            tutoCalled.gameObject.transform.position = Vector3.MoveTowards(tutoCalled.gameObject.transform.position, initialPos, tutMoveSpeed * Time.deltaTime);
            if (Vector3.Distance(titlePos, initialPos) == 0)
            {
                Debug.Log("reachtop");
                isArriveUp = true;
                isMoveUp = true;
                isArriveDown = false;
                Destroy(tutoCalled);
            }
            yield return null;

        }
    }

}
