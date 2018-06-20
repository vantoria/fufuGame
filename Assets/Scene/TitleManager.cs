using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : SceneController {

    public GameObject cursor1, cursor2;//カーソル
    public GameObject startObj, setumei, exitObj;//選択肢

    public enum E_GameMode
    {
        gameStart,
        setumei,
        exit
    }
    public E_GameMode mode;

    public PlayerController player1, player2;

    private bool doonce1 = false, doonce2 = false;
    private bool okFlg = false;

    TutorialScene TS;

    public enum E_tutorialMode
    {
        upEnd,
        downEnd,
        downNow,
        upNow
    }
    public E_tutorialMode tutorialMode = E_tutorialMode.upEnd;

    protected override void Start ()
    {
        base.Start();

        modeSelect(E_GameMode.gameStart);

        TS = this.gameObject.GetComponent<TutorialScene>();

        musicManager.PlayBGM(MusicManager.BGM_SELECT.title);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (player1.GetMove().y != 0 || player2.GetMove().y != 0)
        {
            if (player1.GetMove().y < 0 && doonce1 == false)
            {
                musicManager.PlaySE(MusicManager.SE_SELECT.select);
                modeChange(-1);
                doonce1 = true;
            }
            else if (player1.GetMove().y > 0 && doonce1 == false)
            {
                musicManager.PlaySE(MusicManager.SE_SELECT.select);
                modeChange(1);
                doonce1 = true;
            }
            else if(player1.GetMove().y == 0)
            {
                doonce1 = false;
            }

            if (player2.GetMove().y < 0 && doonce2 == false)
            {
                musicManager.PlaySE(MusicManager.SE_SELECT.select);
                modeChange(-1);
                doonce2 = true;
            }
            else if (player2.GetMove().y > 0 && doonce2 == false)
            {
                musicManager.PlaySE(MusicManager.SE_SELECT.select);
                modeChange(1);
                doonce2 = true;
            }
            else if (player2.GetMove().y == 0)
            {
                doonce2 = false;
            }
        }
        else
        {
            doonce1 = false;
            doonce2 = false;
        }

        if ((player1.GetBreathFlg() == true || player2.GetBreathFlg() == true) && okFlg == false)
        {
            musicManager.PlaySE(MusicManager.SE_SELECT.ok);
            switch (mode)
            {
                case E_GameMode.gameStart:
                    base.SceneTransition_FadeIn(E_SceneMode.Name);
                    okFlg = true;
                    break;
                case E_GameMode.setumei:
                    switch (tutorialMode)
                    {
                        case E_tutorialMode.upEnd:
                            tutorialMode = E_tutorialMode.downNow;
                            TS.spriteDown();
                            break;
                        case E_tutorialMode.downEnd:
                            tutorialMode = E_tutorialMode.upNow;
                            TS.spriteUp();
                            break;
                        default:
                            break;
                    }
                    break;
                case E_GameMode.exit:
                    Application.Quit();
                    okFlg = true;
                    break;
            }

        }
        switch (tutorialMode)
        {
            case E_tutorialMode.downNow:
                if(TS.isArriveDown == true)
                {
                    tutorialMode = E_tutorialMode.downEnd;
                }
                break;
            case E_tutorialMode.upNow:
                if (TS.isArriveUp == true)
                {
                    tutorialMode = E_tutorialMode.upEnd;
                }
                break;
            default:
                break;
        }
    }

    void modeChange(int i)
    {
        switch (mode)
        {
            case E_GameMode.gameStart:
                if(i==1)
                {
                    modeSelect(E_GameMode.exit);
                }
                if(i==-1)
                {
                    modeSelect(E_GameMode.setumei);
                }

                break;
            case E_GameMode.setumei:
                if (i == 1)
                {
                    modeSelect(E_GameMode.gameStart);
                }
                if (i == -1)
                {
                    modeSelect(E_GameMode.exit);
                }
                break;

            case E_GameMode.exit:
                if (i == 1)
                {
                    modeSelect(E_GameMode.setumei);
                }
                if (i == -1)
                {
                    modeSelect(E_GameMode.gameStart);
                }
                break;
            default:
                break;
        }
    }
    void modeSelect(E_GameMode _mode)
    {
        startObj.GetComponent<SpriteRenderer>().color = Color.white;
        setumei.GetComponent<SpriteRenderer>().color = Color.white;
        exitObj.GetComponent<SpriteRenderer>().color = Color.white;

        mode = _mode;
        Vector3 vec1, vec2, vec3 = Vector3.zero;
        vec1 = cursor1.transform.position;
        vec2 = cursor2.transform.position;

        switch (mode)
        {
            case E_GameMode.gameStart:
                vec3 = startObj.transform.position;
                startObj.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case E_GameMode.setumei:
                vec3 = setumei.transform.position;
                setumei.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case E_GameMode.exit:
                vec3 = exitObj.transform.position;
                exitObj.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            default:
                break;
        }

        vec1.y = vec3.y;
        vec2.y = vec3.y;

        cursor1.transform.position = vec1;
        cursor2.transform.position = vec2;
    }
}
