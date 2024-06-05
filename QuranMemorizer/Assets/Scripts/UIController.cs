using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void PlaySFX(string name)
    {
        AudioManager.instance.PlaySFX(name);
    }

    // exit the game
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
