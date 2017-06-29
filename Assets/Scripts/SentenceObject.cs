using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SentenceObject : MonoBehaviour {
    public bool right;
    public string sentence;
    public float sizeFactor;

    public GameObject optionButtons;
    private GameObject options;

    protected float localSpeed;

    private bool clicked = false;

    public void OnEnable()
    {
        clicked = false;
    }

    // Use this for initialization
    public virtual void ActivateSentence () {
        SentenceStruct sentence = SentenceManager.instance.GetRandomSentence();

        gameObject.GetComponentInChildren<Text>().text = sentence.text;
        this.sentence = sentence.text;
        right = (sentence.type == 0) ? false : true;

        localSpeed = GameControl.instance.speed * (sizeFactor/sentence.text.Length);

        // Hide if needed
        if (GameControl.instance.rightSelected)
        {
            Color cor = gameObject.GetComponentInChildren<Text>().color;
            cor.a = 0f;
            gameObject.GetComponentInChildren<Text>().color = cor;
        }
    }

    // Update is called once per frame
    public virtual void Update () {
        if(!GameControl.instance.rightSelected)
            transform.position -= new Vector3(0f, localSpeed, 0f);
        else
            transform.position -= new Vector3(0f, localSpeed*GameControl.instance.slowMotionFactor, 0f);

        if (!isVisible())
        {
            if (GameControl.instance.tutorial)
                TutorialControl.instance.currentSentence = null;

            gameObject.SetActive(false);
        }
	}

    public void Clicked()
    {
        if (!clicked && !GameControl.instance.rightSelected)
        {
            var colors = GetComponent<Button>().colors;

            clicked = true;

            if (right)
            {
                SentencePool.instance.HideText(sentence);

                GameControl.instance.PlayRight();
                colors.highlightedColor = GameControl.instance.rightColor;
                colors.normalColor = GameControl.instance.rightColor;
                colors.pressedColor = GameControl.instance.rightColor;

                GameControl.instance.rightSelected = true;
                GameControl.instance.IncreaseScore();

                options = Instantiate(optionButtons, transform);
                options.transform.localScale = new Vector3(1f, 1f, 1f);
                ((RectTransform)options.transform).localPosition = new Vector3(0f, 0f, 0f);
                
                //options.transform.localPosition = transform.localPosition;
            }
            else
            {
                GameControl.instance.PlayWrong();
                colors.highlightedColor = GameControl.instance.wrongColor;
                colors.normalColor = GameControl.instance.wrongColor;
                colors.pressedColor = GameControl.instance.wrongColor;

                ++GameControl.instance.fullWrong;

                GameControl.instance.DecreaseScore();
            }

            //GetComponent<Button>().colors = colors;
        }
    }

    private bool isVisible()
    {
        bool visible = true;

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

        if(visibleCorners == 0)
        {
            visible = false;

            if (right && !clicked)
            {
                ++GameControl.instance.rightPassed;
                GameControl.instance.DecreaseScore();
            }
            else if(!right && !clicked)
            {
                GameControl.instance.IncreaseScore();
                ++GameControl.instance.wrongPassed;
            }

            if (options != null)
            {
                Destroy(options);
                options = null;
            }

            clicked = false;
        }
        return visible;
    }
}
