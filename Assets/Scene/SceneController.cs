using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*シーンクラスの親クラス*/

public class SceneController : MonoBehaviour {

    public enum E_SceneMode
    {
        Title,
        Name,
        Main,
        Result
    }
    public string sceneName;

    public GameInstance gameInstance;
    public shaderManager shaderManager;
    public MusicManager musicManager;

    public bool transitionFlg = false;

    protected virtual void Start()
    {
        gameInstance = GameInstance.instance;
        shaderManager = GameObject.Find("Main Camera").GetComponent<shaderManager>();
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (transitionFlg == true)
        {
            if (shaderManager.fadeMode == shaderManager.E_FadeMode.Default) SceneManager.LoadScene(sceneName);
        }
    }

    public void SetUp()
    {
        gameInstance = GameInstance.instance;
        shaderManager = GameObject.Find("Main Camera").GetComponent<shaderManager>();
    }


    //シーン遷移フェードインモードで実行　列挙型modeで遷移先指定
    public void SceneTransition_FadeIn(E_SceneMode mode)
    {
        shaderManager.FadeIn();
        transitionFlg = true;
        switch (mode)
        {
            case E_SceneMode.Title:
                sceneName = "TitleScene";
                break;
            case E_SceneMode.Name:
                sceneName = "PlayerName";
                break;
            case E_SceneMode.Main:
                sceneName = "MainScene";
                break;
            case E_SceneMode.Result:
                sceneName = "ResultScene";
                break;
        }
    }
}
