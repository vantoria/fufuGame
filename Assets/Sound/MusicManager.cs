using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager instance;

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

    public AudioSource _audioSource;

    public enum SE_SELECT
    {
        select,
        ok,
        allOkSE,
        rocker,
        breath

    }
    public enum BGM_SELECT
    {
        title,
        game
    }

    public AudioClip rockerSE;
    public AudioClip okSE;
    public AudioClip allOkSE;

    public AudioClip titleBGM;
    public AudioClip gameBGM;

    void Start ()
    {
        _audioSource = this.GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySE(SE_SELECT mode)
    {
        switch (mode)
        {
            case SE_SELECT.select:
                break;
            case SE_SELECT.ok:
                _audioSource.PlayOneShot(okSE);
                break;
            case SE_SELECT.allOkSE:
                _audioSource.PlayOneShot(allOkSE);
                break;
            case SE_SELECT.rocker:
                _audioSource.PlayOneShot(rockerSE);
                break;
            case SE_SELECT.breath:
                break;
            default:
                break;
        }
    }

    public void PlayBGM(BGM_SELECT mode)
    {
        StopBGM();
        switch (mode)
        {
            case BGM_SELECT.title:
                _audioSource.clip = titleBGM;
                _audioSource.Play();
                break;
            case BGM_SELECT.game:
                _audioSource.clip = gameBGM;
                _audioSource.Play();
                break;
            default:
                break;
        }
    }

    public void StopBGM()
    {
        _audioSource.Stop();
    }
}
