using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameSceneManager : SceneController
{

	//名前入れ
	private string[] playerName_ = new string[2];

	private int writeCount_ = 0;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

        musicManager.PlayBGM(MusicManager.BGM_SELECT.title);
	}

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();

        //シーン遷移はこの関数を呼ぶ
        //引数で遷移先指定
        // base.SceneTransition_FadeIn(E_SceneMode.Main);
        if (writeCount_ >= 2)
        {
            base.gameInstance.SetName(playerName_);

            base.SceneTransition_FadeIn(E_SceneMode.Main);
        }

	}

	public void PlayerNameWrite(int playerNumber, string playerName)
	{
		playerName_[playerNumber] = playerName;
		writeCount_++;
	}

	public string[] PlayerNameLoad()
	{
		return playerName_;
	}
}
