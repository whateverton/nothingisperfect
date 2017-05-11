using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CounterControl : MonoBehaviour {

    public int startTime;
    public int endTime;
    public int pace;

    private int counter;
    private bool runBackwards;

    private Text counterText;

    void OnEnable()
    {
        if (startTime < endTime)
            runBackwards = false;
        else
            runBackwards = true;

        counter = startTime;

        counterText = GetComponent<Text>();

        counterText.text = counter.ToString();

        StartCoroutine("updateTimer");
    }

    IEnumerator updateTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(pace);

            if(runBackwards)
            {
                --counter;
                if(counter < endTime)
                {
                    closeTimer();
                }
            }
            else
            {
                ++counter;
                if(counter > endTime)
                {
                    closeTimer();
                }
            }

            counterText.text = counter.ToString();
        }
    }

    void closeTimer()
    {
        // Rechear de codigo.. ou nao
    }
}
