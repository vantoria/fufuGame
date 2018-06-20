using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : SceneController {

	public GameObject c1, c2;
	public GameObject cursor1, cursor2;
    public resultAnim player1, player2;

    public ResultPlayer p1, p2;

	[SerializeField]private GameObject[] cursor = new GameObject[4];
	[SerializeField]private XboxControllerCustom manager;

	private bool isKeyClick = false;

    protected override void Start()
    {
        base.Start();

        musicManager.PlayBGM(MusicManager.BGM_SELECT.title);

		cursor[0].gameObject.SetActive(true);
		cursor[1].gameObject.SetActive(true);
		cursor[2].gameObject.SetActive(false);
		cursor[3].gameObject.SetActive(false);

        StartCoroutine("Delay");
    }

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();

		//シーン遷移はこの関数を呼ぶ
		//引数で遷移先指定
		// base.SceneTransition_FadeIn(E_SceneMode.Main);


		if (Input.GetAxis("Vertical") < 0.0f && Input.GetAxis("Vertical2") < 0.0f)
		{
			if (!isKeyClick)
			{
				cursor[0].gameObject.SetActive(true);
				cursor[1].gameObject.SetActive(true);
				cursor[2].gameObject.SetActive(false);
				cursor[3].gameObject.SetActive(false);

				isKeyClick = true;
			}
			else
			{
				cursor[0].gameObject.SetActive(false);
				cursor[1].gameObject.SetActive(false);
				cursor[2].gameObject.SetActive(true);
				cursor[3].gameObject.SetActive(true);

				isKeyClick = false;
			}
		}
		if (Input.GetAxis("Vertical") > 0.0f && Input.GetAxis("Vertical2") > 0.0f)
		{
			if (!isKeyClick)
			{
				cursor[0].gameObject.SetActive(true);
				cursor[1].gameObject.SetActive(true);
				cursor[2].gameObject.SetActive(false);
				cursor[3].gameObject.SetActive(false);

				isKeyClick = true;
			}
			else
			{
				cursor[0].gameObject.SetActive(false);
				cursor[1].gameObject.SetActive(false);
				cursor[2].gameObject.SetActive(true);
				cursor[3].gameObject.SetActive(true);

				isKeyClick = false;
			}
		}


		if (manager.Enter())
		{
			if (!isKeyClick)
			{
				ToGame();
			}
			else
			{
				ToTitle();
			}
		}

		KeyCommand();
	}



	void KeyCommand()
	{
		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
		{
			if (!isKeyClick)
			{
				cursor[0].gameObject.SetActive(true);
				cursor[1].gameObject.SetActive(true);
				cursor[2].gameObject.SetActive(false);
				cursor[3].gameObject.SetActive(false);

				isKeyClick = true;
			}
			else
			{
				cursor[0].gameObject.SetActive(false);
				cursor[1].gameObject.SetActive(false);
				cursor[2].gameObject.SetActive(true);
				cursor[3].gameObject.SetActive(true);

				isKeyClick = false;
			}
		}
		if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
		{
			if (!isKeyClick)
			{
				cursor[0].gameObject.SetActive(true);
				cursor[1].gameObject.SetActive(true);
				cursor[2].gameObject.SetActive(false);
				cursor[3].gameObject.SetActive(false);

				isKeyClick = true;
			}
			else
			{
				cursor[0].gameObject.SetActive(false);
				cursor[1].gameObject.SetActive(false);
				cursor[2].gameObject.SetActive(true);
				cursor[3].gameObject.SetActive(true);

				isKeyClick = false;
			}
		}

		if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space))
		{
			if (!isKeyClick)
			{
				ToGame();
			}
			else
			{
				ToTitle();
			}
		}
	}

	public void ToGame()
	{
		base.SceneTransition_FadeIn(E_SceneMode.Main);
	}

	public void ToTitle()
	{
		base.SceneTransition_FadeIn(E_SceneMode.Title);
	}

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.20f);
        string player1JudgeName = p1.GetComponentInChildren<Image>().sprite.name;
        string player2JudgeName = p2.GetComponentInChildren<Image>().sprite.name;
        print(player1JudgeName);
        if (player1JudgeName == "win")
        {
            player1.animPlay(resultAnim.E_mode.win);
            player2.animPlay(resultAnim.E_mode.lose);
        }
        else
        {
            player1.animPlay(resultAnim.E_mode.lose);
            player2.animPlay(resultAnim.E_mode.win);
        }
    }
}
