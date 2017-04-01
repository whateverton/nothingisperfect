using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGenerator : MonoBehaviour {
    public static ButtonGenerator instance;

    public float timeBetweenSentence;
    public GameObject sentencePrefab;

    GameObject canvas;

    // Use this for initialization
    void Start()
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
            yield return new WaitForSeconds(timeBetweenSentence);

            GameObject currentSentence = Instantiate(sentencePrefab,canvas.transform) as GameObject;
            
            float posX = Random.Range(0, Screen.width);
            currentSentence.transform.localScale = new Vector3(1f, 1f, 1f);
            Vector3 newPosition = new Vector3(posX, 300f, 0f);

            ((RectTransform)currentSentence.transform).position = Camera.main.ScreenToWorldPoint(newPosition);
            currentSentence.SetActive(true);
        }
    }
}
