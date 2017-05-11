using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SentenceTutorial : SentenceObject {

    public override void ActivateSentence()
    {
        SentenceStruct sentence = TutorialControl.instance.GetNextSentence();
        TutorialControl.instance.currentSentence = gameObject;
        InformationControl.instance.SetHelpText(TutorialControl.instance.GetInfoByType(sentence.type).text);

        gameObject.GetComponentInChildren<Text>().text = sentence.text;
        right = (sentence.type == 0) ? false : true;

        localSpeed = GameControl.instance.speed * (sizeFactor / sentence.text.Length);
    }

    public override void Update()
    {
        if (TutorialControl.instance.tutorialPause) return;

        base.Update();
    }
}
