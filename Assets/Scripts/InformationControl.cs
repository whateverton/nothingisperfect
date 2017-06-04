﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InformationControl : MonoBehaviour {

    Text helpText;

    static public InformationControl instance;

    Animator infoAnimator;

	public void OnEnable()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;

        infoAnimator = GetComponent<Animator>();

        helpText = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void SetHelpText(string infoText)
    {
        helpText.text = infoText;
    }

    public void AckInfo()
    {
        TutorialControl.instance.tutorialPause = false;
        gameObject.SetActive(false);
    }
}
