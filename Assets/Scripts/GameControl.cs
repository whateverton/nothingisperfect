using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public int maxTime;

    public float speed = 1f;
    public float slowMotionFactor;

    public bool tutorial = false;

    public bool rightSelected = false;

    public Text scoreText;
    public Text timeText;

    AudioSource sound;

    public int fullRight = 0;
    public int wrongCategory = 0;
    public int fullWrong = 0;
    public int rightPassed = 0;
    public int wrongPassed = 0;

    public GameObject finalReport;

    public bool gameOver = false;

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
        if(!tutorial)
            StartCoroutine("Counter");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
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

        if (tutorial)
            ++TutorialControl.instance.rightAmount;
    }

    public void DecreaseScore()
    {
        int score = int.Parse(scoreText.text);
        if(score > 0)
            --score;
        scoreText.text = score.ToString("00");
    }

    IEnumerator Counter()
    {
        while (!gameOver)
        {
            int time;

            yield return new WaitForSeconds(1f);

            time = int.Parse(timeText.text);
            if (time < maxTime)
                ++time;
            else
                EndGame();

            timeText.text = time.ToString("00");
        }
    }

    void EndGame()
    {
        GameObject currentReport = Instantiate(finalReport, FindObjectOfType<Canvas>().transform);
        currentReport.transform.localScale = new Vector3(1f, 1f, 1f);
        currentReport.transform.localPosition = new Vector3(100f, 0f, 0f);
        Text[] scores = currentReport.GetComponentsInChildren<Text>();

        scores[2].text = fullRight.ToString("00");
        scores[4].text = wrongCategory.ToString("00");
        scores[6].text = fullWrong.ToString("00");
        scores[8].text = rightPassed.ToString("00");
        scores[10].text = wrongPassed.ToString("00");

        gameOver = true;
    }
}
