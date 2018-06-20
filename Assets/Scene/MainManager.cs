using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : SceneController {

    public GameObject player1;
    public GameObject player2;

    public struct playerComponent
    {
        public BreathManager _breathManager;
        public PlayerController _playerController;
        public PlayerAnimation _playerAnimation;
        public PlayerMove _playerMove;
    }

    public playerComponent player1Component;
    public playerComponent player2Component;

    public Text p1Name;
    public Text p2Name;

    public Timer timer;
    public TissueManager tissue;

    private bool finishFlg = false;
    

    protected override void Start()
    {
        base.Start();

        SetComponent(player1);
        SetComponent(player2);

        if(gameInstance == null|| gameInstance.GetName()[0] == "")
        {

        }
        else
        {
            p1Name.text = gameInstance.GetName()[0];
        }
        if (gameInstance == null || gameInstance.GetName()[1] == "")
        {

        }
        else
        {
            p2Name.text = gameInstance.GetName()[1];
        }

        //tissue = GameObject.Find("Tissue").GetComponent<TissueManager>();

        musicManager.PlayBGM(MusicManager.BGM_SELECT.game);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //時間終了
        if (timer.gameOverFlg == true && finishFlg == false)
        {
            resultConvey();
        }
        //落ちた
        if(tissue.gameOverFlg == true && finishFlg == false)
        {
            resultConvey();
        }

    }
    void resultConvey()
    {
        switch (tissue.area)
        {
            case TissueManager.E_Area.Player1:
                gameInstance.SetWinPlayer(GameInstance.E_Player.Player2);
                break;
            case TissueManager.E_Area.Player2:
                gameInstance.SetWinPlayer(GameInstance.E_Player.Player1);
                break;
            case TissueManager.E_Area.Center:
                gameInstance.SetWinPlayer(GameInstance.E_Player.num);
                break;
            default:
                gameInstance.SetWinPlayer(GameInstance.E_Player.num);
                break;
        }

        finishFlg = true;
        base.SceneTransition_FadeIn(E_SceneMode.Result);

    }

    playerComponent SetComponent(GameObject obj)
    {
        playerComponent pc;

        pc._breathManager = obj.gameObject.GetComponent<BreathManager>();
        pc._playerController = obj.gameObject.GetComponent<PlayerController>();
        pc._playerAnimation = obj.gameObject.GetComponent<PlayerAnimation>();
        pc._playerMove = obj.gameObject.GetComponent<PlayerMove>();

        return pc;
    }
}
