using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGenerator : MonoBehaviour {
    public static ButtonGenerator instance;
    
    public float timeBetweenSentence;
    public bool onceAtATime;

    GameObject canvas;

    // Use this for initialization
    void OnEnable()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        canvas = GameObject.Find("Canvas");

        StartCoroutine("CreateSentence");
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator CreateSentence()
    {
        while (true)
        {
            GameObject currentSentence = SentencePool.instance.ActivateObject();
            Rect sentenceRect = ((RectTransform)currentSentence.transform).rect;

            float posX;
            if (onceAtATime)
                posX = 0; 
            else
                posX = Random.Range(-((RectTransform)canvas.transform).rect.width / 2 + sentenceRect.width, ((RectTransform)canvas.transform).rect.width / 2 - sentenceRect.width);

            Vector3 objectPosition = ((RectTransform)currentSentence.transform).position;//Camera.main.ScreenToWorldPoint(newPosition);
            ((RectTransform)currentSentence.transform).position = new Vector3(posX * canvas.transform.localScale.x, 300 * canvas.transform.localScale.y, objectPosition.z * canvas.transform.localScale.z);
            currentSentence.SetActive(true);

            if (!onceAtATime)
            {
                if (GameControl.instance.rightSelected)
                    yield return new WaitForSeconds(timeBetweenSentence / GameControl.instance.slowMotionFactor);
                else
                    yield return new WaitForSeconds(timeBetweenSentence);
            }
            else
            {
                while (currentSentence.activeSelf)
                {
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}
