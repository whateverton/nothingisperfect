using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public enum SentenceType
{
    WRONG,      // Well, not correct
    CORRECT_1,  // Started in the past and are still going on
    CORRECT_2,  // Happened in an undefined time in the past
    CORRECT_3,  // Things that just happened
}

public class SentenceStruct
{
    public string text;
    public SentenceType type;
    public bool used;
    public string info;
}

public class SentenceManager : MonoBehaviour {

    public static SentenceManager instance;

    XmlDocument currentSentenceDocument;

    SentenceStruct[] loadedSentences;

    // Use this for initialization
    void OnEnable () {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        LoadSentencesDocument();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadSentencesDocument()
    {
        TextAsset documentText = (TextAsset)Resources.Load("Sentences");

        //Transforma o texto em objeto XML 
        currentSentenceDocument = new XmlDocument();
        currentSentenceDocument.LoadXml(documentText.ToString());

        XmlNodeList nodeList = currentSentenceDocument["SentencesData"].ChildNodes;
        loadedSentences = new SentenceStruct[nodeList.Count];

        int index = 0;
        foreach (XmlNode node in currentSentenceDocument["SentencesData"])
        {
            loadedSentences[index] = new SentenceStruct();

            loadedSentences[index].text = node.InnerText;
            loadedSentences[index].type = (SentenceType)int.Parse(node.Attributes["type"].InnerText);
            loadedSentences[index].used = false;
            ++index;
        }
    }

    public SentenceStruct GetRandomSentence()
    {
        int index;
        int totalChecked = 0;
        do
        {
            index = Random.Range(0, loadedSentences.Length);
            ++totalChecked;
            if (totalChecked == loadedSentences.Length) ResetSentences();
        } while (loadedSentences[index].used);

        if (loadedSentences[index] != null)
        {
            loadedSentences[index].used = true;
            return loadedSentences[index];
        }
        else
            return null;
    }

    public SentenceStruct GetSentenceByType(SentenceType type)
    {
        int index = -1;
        int startIndex = -1;

        do
        {
            ++index;

            if (loadedSentences[index].type == type && startIndex == -1)
                startIndex = index;

            if (index == loadedSentences.Length)
            {
                ResetSentences();

                index = startIndex - 1;
            }
        } while (loadedSentences[index].used || (loadedSentences[index].type != type));

        if (loadedSentences[index] != null)
        {
            loadedSentences[index].used = true;
            return loadedSentences[index];
        }
        else
            return null;
    }

    void ResetSentences()
    {
        foreach(SentenceStruct sentence in loadedSentences)
        {
            sentence.used = false;
        }
    }
}
