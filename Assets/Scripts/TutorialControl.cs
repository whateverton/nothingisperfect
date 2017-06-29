using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class TutorialControl : MonoBehaviour
{
    public static TutorialControl instance;

    XmlDocument currentSentenceDocument;
    SentenceStruct[] loadedInfo;

    [HideInInspector]
    public bool tutorialPause = false;

    [HideInInspector]
    public bool alreadyPaused = false;

    [HideInInspector]
    public GameObject currentSentence;

    public int minErrorThreshold;
    public int maxErrorThreshold;

    private bool growingRight = true;
    private bool downingRight = false;

    private bool showInfo = true;

    public int rightAmount = 0;

    private int globalIndex = 0;

    void OnEnable()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        currentSentence = null;
        tutorialPause = false;
        alreadyPaused = false;
        globalIndex = 0;

        LoadInfo();
    }

    public void Update()
    {
        CheckErrors();

        if(currentSentence != null)
        {
            if((currentSentence.transform.position.y < 0) && !alreadyPaused && showInfo)
            {
                tutorialPause = true;
                alreadyPaused = true;
                InformationControl.instance.gameObject.SetActive(true);
            }
        }
    }

    private void LoadInfo()
    {
        /*sentenceList = new SentenceStruct[sentenceText.Length];

        for(int index = 0; index < sentenceList.Length; ++index)
        {
            sentenceList[index] = new SentenceStruct();

            sentenceList[index].text = sentenceText[index];
            sentenceList[index].type = sentenceType[index];
            sentenceList[index].used = false;
            sentenceList[index].info = infoList[index];
        }*/
        TextAsset documentText = (TextAsset)Resources.Load("Information");

        //Transforma o texto em objeto XML 
        currentSentenceDocument = new XmlDocument();
        currentSentenceDocument.LoadXml(documentText.ToString());

        XmlNodeList nodeList = currentSentenceDocument["InformationData"].ChildNodes;
        loadedInfo = new SentenceStruct[nodeList.Count];

        int index = 0;
        foreach (XmlNode node in currentSentenceDocument["InformationData"])
        {
            loadedInfo[index] = new SentenceStruct();

            loadedInfo[index].text = node.InnerText;
            loadedInfo[index].type = (SentenceType)int.Parse(node.Attributes["type"].InnerText);
            loadedInfo[index].used = false;
            ++index;
        }
    }

    public SentenceStruct GetInfoByType(SentenceType type)
    {
        int index = -1;
        int startIndex = -1;

        do
        {
            ++index;

            if (index == loadedInfo.Length)
            {
                ResetSentences();

                index = startIndex - 1;
            }

            if (loadedInfo[index].type == type && startIndex == -1)
                startIndex = index;

        } while (loadedInfo[index].used || (loadedInfo[index].type != type));

        if (loadedInfo[index] != null)
        {
            loadedInfo[index].used = true;
            return loadedInfo[index];
        }
        else
            return null;
    }

    void ResetSentences()
    {
        foreach (SentenceStruct sentence in loadedInfo)
        {
            sentence.used = false;
        }
    }

    public SentenceStruct GetNextSentence()
    {
        alreadyPaused = false;

        return SentenceManager.instance.GetRandomSentence();
    }

    private void CheckErrors()
    {
        if(showInfo && growingRight && rightAmount > maxErrorThreshold)
        {
            showInfo = false;
        }
        else if(!showInfo && growingRight && rightAmount < maxErrorThreshold)
        {
            growingRight = false;
            downingRight = true;
        }
        else if(!showInfo && downingRight && rightAmount < minErrorThreshold)
        {
            showInfo = true;
        }
        else if(showInfo && downingRight && rightAmount > minErrorThreshold)
        {
            growingRight = true;
            downingRight = false;
        }
    }
}
