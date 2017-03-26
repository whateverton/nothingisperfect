using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentenceObject : MonoBehaviour {
    public bool right;
    public string sentence;
    public float sizeFactor;

    private float localSpeed;

    // Use this for initialization
    void Start () {
        SentenceStruct sentence = SentenceManager.instance.GetRandomSentence();
        gameObject.GetComponentInChildren<Text>().text = sentence.text;
        right = (sentence.type == 0) ? false : true;

        localSpeed = GameControl.instance.speed * (sizeFactor/sentence.text.Length);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(0f, localSpeed, 0f);
	}

    public void Clicked()
    {
        var colors = GetComponent<Button>().colors;

        if (right)
        {
            GameControl.instance.PlayRight();
            colors.highlightedColor = GameControl.instance.rightColor;
            colors.normalColor = GameControl.instance.rightColor;
            colors.pressedColor = GameControl.instance.rightColor;
        }
        else
        {
            GameControl.instance.PlayWrong();
            colors.highlightedColor = GameControl.instance.wrongColor;
            colors.normalColor = GameControl.instance.wrongColor;
            colors.pressedColor = GameControl.instance.wrongColor;
        }
        
        GetComponent<Button>().colors = colors;
    }
}
