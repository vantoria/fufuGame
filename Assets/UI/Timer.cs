using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Sprite[] number;
    [SerializeField] private GameObject[] time_;
    public float timeLeft = 31.0f;
    public bool gameOverFlg = false;

    // private
    private string timeName;

    void Start()
    {
        StartCoroutine("TimeCountDown");
        timeLeft += 1;
    }

    IEnumerator TimeCountDown()
    {
        while (true)
        {
            int count = 0;
            string tName = Mathf.FloorToInt(timeLeft).ToString();
            while (count < 2)
            {
                if (tName.Length <= 1)
                {
                    if (count < 1)
                    {
                        timeName = "0";
                    }
                    else
                    {
                        timeName = tName.Substring(0, 1);
                    }
                }
                else
                {
                    timeName = tName.Substring(count, 1);
                }

                time_[count].GetComponent<Image>().sprite = number[Mathf.FloorToInt(float.Parse(timeName))];
                count++;
            }
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                //game over
                timeLeft = 0;
                gameOverFlg = true;
            }
            yield return null;
        }
    }
}
