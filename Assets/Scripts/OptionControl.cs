using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OptionControl : MonoBehaviour {
    public float interval;

    public SentenceType type;

    private GameObject[] options;
    private float[] finalPosistion;
    private float initialPos;

    // Use this for initialization
    void OnEnable ()
    {
        initialPos = transform.GetChild(0).localPosition.y;

        options = new GameObject[3];
        finalPosistion = new float[3];

        for (int i = 0; i < transform.childCount; ++i)
        {
            Vector3 pos;

            options[i] = transform.GetChild(i).gameObject;

            pos = options[i].transform.localPosition;
            finalPosistion[i] = pos.y;
            pos.y = initialPos;
            options[i].transform.localPosition = pos;
        }

        options[0].transform.DOLocalMoveY(finalPosistion[0], interval).SetEase(Ease.OutBounce);
        options[1].transform.DOLocalMoveY(finalPosistion[1], interval).SetEase(Ease.OutBounce);
        options[2].transform.DOLocalMoveY(finalPosistion[2], interval).SetEase(Ease.OutBounce);

    }

	public void OptionSelected(int type)
    {
        if((SentenceType)type == this.type)
        {
            GameControl.instance.IncreaseScore();
            ++GameControl.instance.fullRight;
        }
        else
        {
            ++GameControl.instance.wrongCategory;
        }

        options[0].transform.DOLocalMoveY(initialPos, interval).SetEase(Ease.InBounce);
        options[1].transform.DOLocalMoveY(initialPos, interval).SetEase(Ease.InBounce);
        options[2].transform.DOLocalMoveY(initialPos, interval).SetEase(Ease.InBounce);

        StartCoroutine("RemoveOptions");
    }

    IEnumerator RemoveOptions()
    {
        yield return new WaitForSeconds(interval+0.25f);

        Destroy(gameObject);

        GameControl.instance.rightSelected = false;

        SentencePool.instance.ShowText();
    }
}
