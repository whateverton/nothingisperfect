using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    AudioSource sound;

    // Use this for initialization
    void Start () {
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
}
