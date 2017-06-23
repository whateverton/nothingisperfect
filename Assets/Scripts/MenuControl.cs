using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {
    Animator animator;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Opened", true);

        for(int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        StartCoroutine("ActivateButtons");
    }

    public void LoadScene(string name)
    {
        animator.SetBool("Opened", false);

        StartCoroutine("LoadSceneCoroutine", name);
    }

    public void QuitGame()
    {
        animator.SetBool("Opened", false);
        StartCoroutine("QuitGameCoroutine");
    }

    IEnumerator LoadSceneCoroutine(string name)
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(name);

    }

    IEnumerator QuitGameCoroutine()
    {
        yield return new WaitForSeconds(2f);

        Application.Quit();
    }

    IEnumerator ActivateButtons()
    {
        yield return new WaitForSeconds(2f);


        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
