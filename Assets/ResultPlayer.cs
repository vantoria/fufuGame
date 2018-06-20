using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPlayer : SceneController 
{
	[SerializeField]private Sprite[] image_ = new Sprite[2];

	//private const int ;

	private int playerNum_ = 0;

	public GameObject imageAnimation_;

	private float min = 0, max = 30;
	private float count = 0;
	bool isRotation = false;

	[SerializeField]private float delaySpeed = 0.0f;

	// Use this for initialization
	protected override void Start()
	{
		base.Start();



		GetComponentInChildren<Text>().text = "";

		WinnerName();
		Winner();
		count = imageAnimation_.transform.rotation.z;
	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
		if (count < max && !isRotation)
		{
			imageAnimation_.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 10);
			count++;
		}
		else if (count < max && isRotation)
		{
			imageAnimation_.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 10);
			count++;
		}
		else
		{
			count = 0;
			isRotation = !isRotation;
		}


	}

	private void WinnerName()
	{
		string name = base.gameInstance.GetName()[int.Parse(gameObject.name.Replace("Player", "").Replace("Name", "")) - 1];
		GetComponent<Text>().text = name;
	}


	private void Winner()
	{
		playerNum_ = int.Parse(base.gameInstance.GetWinPlayer().ToString().Replace("Player", ""));

		if (playerNum_ == int.Parse(gameObject.name.Replace("Player", "").Replace("Name", "")))
		{
			gameObject.GetComponentInChildren<Image>().sprite = image_[0];
		}
		else
		{
			gameObject.GetComponentInChildren<Image>().sprite = image_[1];
		}
		imageAnimation_ = gameObject.GetComponentInChildren<Image>().gameObject;
	}
}
