using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] GameObject UsernamePanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Welcome");
    }

    IEnumerator Welcome()
    {
        yield return new WaitForSeconds(3);
        UsernamePanel.SetActive(true);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
