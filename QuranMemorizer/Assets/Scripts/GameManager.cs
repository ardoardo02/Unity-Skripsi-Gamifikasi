using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int currentSurah;
    int currentAyah;

    private void Awake() {
        currentSurah = PlayerPrefs.GetInt("SurahNumber", 999);
        currentAyah = PlayerPrefs.GetInt("AyahNumber", 999);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (currentSurah == 999 || currentAyah == 999) {
            Debug.Log("No surah and ayah data found");
        }
        
        Debug.Log("Current Surah: " + currentSurah);
        Debug.Log("Current Ayah: " + currentAyah);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
