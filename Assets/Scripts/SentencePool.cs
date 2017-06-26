using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentencePool : MonoBehaviour {

    public GameObject objectPrefab;
    public int objectCount;
    GameObject[] objectList;

    public static SentencePool instance;

    // Use this for initialization
    void OnEnable ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Criando a lista com o tamanho pré definido
        objectList = new GameObject[objectCount];


        //Criar X numero de objetos
        for(int i = 0; i < objectCount; ++i)
        {
            objectList[i] = Instantiate(objectPrefab, transform) as GameObject;
            objectList[i].transform.localScale = new Vector3(1f, 1f, 1f);

            //Vector3 objectPosition = ((RectTransform)objectList[i].transform).localPosition;
            ((RectTransform)objectList[i].transform).localPosition = new Vector3(0f, 0f, 0f);

            objectList[i].SetActive(false);
        }
    }

    public GameObject ActivateObject()
    {
        //Faz um loop por toda a pool
        int i = 0;
        while (i < objectList.Length)
        {
            //Ativa o primeiro objeto da pool que estiver desativado
            if (objectList[i].activeSelf == false)
            {
                objectList[i].SetActive(true);
                return objectList[i]; //Retorna este objeto
            }
            i += 1;
        }


        //Caso tenha rodado por todos os objetos da pool e todos ja estiverem ativados, avisa.
        Debug.LogError("Excedeu o numero de objetos da pool");
        objectList[0].SetActive(true); //Retorna o primeiro objeto da lista só pra evitar um retorno nulo 
        return objectList[0];
    }

    public void HideText(string show)
    {
        Text[] texts = GetComponentsInChildren<Text>();

        foreach (Text t in texts)
        {
            Color cor = t.color;

            if (t.text != show)
                cor.a = 0f;

            t.color = cor;
        }
    }

    public void ShowText()
    {
        Text[] texts = GetComponentsInChildren<Text>();

        foreach (Text t in texts)
        {
            Color cor = t.color;
            
            cor.a = 0xFF;

            t.color = cor;
        }
    }
}
