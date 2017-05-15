using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty{
     EASY
    ,MEDIUM
    ,HARD
}

public class GameControl : MonoBehaviour {
    public static GameControl instance;

    public Color rightColor;
    public Color wrongColor;

    public AudioClip rightSound;
    public AudioClip wrongSound;

    public Difficulty difficulty = Difficulty.EASY;

    public float speed = 1f;
    public float slowMotionFactor;

    public bool tutorial = false;

    public bool rightSelected = false;

    public Text scoreText;

    AudioSource sound;

    // Use this for initialization
    void OnEnable () {
		if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayRight()
    {
        sound.Stop();
        sound.clip = rightSound;
        sound.Play();
    }

    public void PlayWrong()
    {
        sound.Stop();
        sound.clip = wrongSound;
        sound.Play();
    }

    public void IncreaseScore()
    {
        int score = int.Parse(scoreText.text);
        if(score < 99)
            ++score;
        scoreText.text = score.ToString("00");
    }

    public void DecreaseScore()
    {
        int score = int.Parse(scoreText.text);
        if(score > 0)
            --score;
        scoreText.text = score.ToString("00");
    }
}
