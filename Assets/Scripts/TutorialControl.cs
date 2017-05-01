using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    public static TutorialControl instance;

    public SentenceStruct[] sentenceList;

    public string[] sentenceText;
    public SentenceType[] sentenceType;
    public string[] infoList;

    private int globalIndex = 0;

    void OnEnable()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        globalIndex = 0;

        LoadSentences();
    }

    private void LoadSentences()
    {
        sentenceList = new SentenceStruct[sentenceText.Length];

        for(int index = 0; index < sentenceList.Length; ++index)
        {
            sentenceList[index] = new SentenceStruct();

            sentenceList[index].text = sentenceText[index];
            sentenceList[index].type = sentenceType[index];
            sentenceList[index].used = false;
            sentenceList[index].info = infoList[index];
        }
    }

    public SentenceStruct GetNextSentence()
    {
        if (globalIndex < sentenceList.Length)
            return sentenceList[globalIndex++];
        else
            return null;
    }
}
