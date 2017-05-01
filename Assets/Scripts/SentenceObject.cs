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
    public void ActivateSentence () {
        SentenceStruct sentence;
        if (GameControl.instance.tutorial)
            sentence = TutorialControl.instance.GetNextSentence();
        else
            sentence = SentenceManager.instance.GetRandomSentence();

        gameObject.GetComponentInChildren<Text>().text = sentence.text;
        right = (sentence.type == 0) ? false : true;

        localSpeed = GameControl.instance.speed * (sizeFactor/sentence.text.Length);
    }
	
	// Update is called once per frame
	void Update () {
        if(!GameControl.instance.rightSelected)
            transform.position -= new Vector3(0f, localSpeed, 0f);
        else
            transform.position -= new Vector3(0f, localSpeed*GameControl.instance.slowMotionFactor, 0f);

        if (!isVisible())
        {
            gameObject.SetActive(false);
        }
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

            GameControl.instance.rightSelected = true;
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

    private bool isVisible()
    {
        Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height); // Screen space bounds (assumes camera renders across the entire screen)
        Vector3[] objectCorners = new Vector3[4];
        ((RectTransform)transform).GetWorldCorners(objectCorners);

        int visibleCorners = 0;
        Vector3 tempScreenSpaceCorner; // Cached
        for (var i = 0; i < objectCorners.Length; i++) // For each corner in rectTransform
        {
            tempScreenSpaceCorner = Camera.main.WorldToScreenPoint(objectCorners[i]); // Transform world space position of corner to screen space
            if (screenBounds.Contains(tempScreenSpaceCorner)) // If the corner is inside the screen
            {
                visibleCorners++;
            }
        }

        return (visibleCorners > 0);
    }
}
