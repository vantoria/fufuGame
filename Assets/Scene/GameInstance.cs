using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour {

    public static GameInstance instance;

    string[] playerName = new string[2];

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum E_Player
    {
        Player1,
        Player2,
        num
    }

    public E_Player winPlayer = E_Player.num;//勝利プレイヤー
    public E_Player previousPlayer = E_Player.num;//前の勝利プレイヤー
    public int winNum = 0;//勝利数

    public E_Player GetWinPlayer()
    {
        return winPlayer;
    }
    public void SetWinPlayer(E_Player player)
    {
        winPlayer = player;
    }
    public E_Player GetPreviousPlayer()
    {
        return previousPlayer;
    }
    public void SetPreviousPlayer(E_Player player)
    {
        previousPlayer = player;
    }
    public int GetWinNum()
    {
		return winNum;
	}
    public void SetWinNum(int i)
    {
		winNum += i;
    }

    public void EraseResultData()
    {
        winPlayer = E_Player.num;
        previousPlayer = E_Player.num;
        winNum = 0;
    }

    void Start () {
	}


	void Update () {
		
	}

    public void SetName(string[] str )
    {
        playerName = str;
    }
    public string[] GetName()
    {
        return playerName;
    }
}
