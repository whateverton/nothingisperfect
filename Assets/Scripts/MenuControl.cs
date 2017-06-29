using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {
    Animator animator;
    public AudioSource audioSource;
    public AudioClip closing;

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
        audioSource.clip = closing;
        animator.SetBool("Opened", false);

        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        audioSource.Play();
        StartCoroutine("LoadSceneCoroutine", name);
    }

    public void QuitGame()
    {
        audioSource.clip = closing;
        animator.SetBool("Opened", false);

        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        audioSource.Play();
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
        yield return new WaitForSeconds(1f);

        audioSource.Play();

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
