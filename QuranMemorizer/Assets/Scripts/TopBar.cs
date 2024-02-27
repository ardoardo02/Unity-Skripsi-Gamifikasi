using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] Image Avatar;
    [SerializeField] Sprite GirlSprite;
    [SerializeField] TMP_Text Username;

    [Header("Coins")]
    [SerializeField] TMP_Text Coins;

    [Header("Experience")]
    [SerializeField] TMP_Text Level;
    [SerializeField] Image ExperienceBar;
    [SerializeField] int currentLevel = 1;
    [SerializeField] int currentXP = 0;
    [SerializeField] int baseXP = 50; // Base XP required for reaching level 2
    [SerializeField] float levelMultiplier = 1.5f; // Multiplier to increase XP for each level

    private void Start() {
        // set avatar
        if (PlayerPrefs.HasKey("Character")) {
            if (PlayerPrefs.GetString("Character") == "girl") {
                Avatar.sprite = GirlSprite;
            }
        }

        // set username
        if (PlayerPrefs.HasKey("Username")) {
            Username.text = PlayerPrefs.GetString("Username");
        }

        // set coins
        Coins.text = PlayerPrefs.GetInt("Coins").ToString();

        // set level
        currentLevel = PlayerPrefs.GetInt("Level", 1);
        Level.text = currentLevel.ToString();

        // set experience
        currentXP = PlayerPrefs.GetInt("Experience");
        CheckLevelUp(); 
    }

    public void GainExperience(int amount)
    {
        currentXP += amount;
        CheckLevelUp();

        PlayerPrefs.SetInt("Experience", currentXP);
    }

    void CheckLevelUp()
    {
        while (currentXP >= XPForNextLevel(currentLevel))
        {
            currentXP -= XPForNextLevel(currentLevel); // Remove XP for next level
            currentLevel++; // Increase level
            Level.text = currentLevel.ToString(); // Update UI
            PlayerPrefs.SetInt("Level", currentLevel);

            // Handle level-up logic (e.g., increase player stats, unlock abilities)
            Debug.Log("Leveled Up! Current Level: " + currentLevel);
        }

        // Update experience bar
        ExperienceBar.fillAmount = (float)currentXP / XPForNextLevel(currentLevel);
    }

    int XPForNextLevel(int level)
    {
        return Mathf.RoundToInt(baseXP * Mathf.Pow(levelMultiplier, level - 1));
    }
}
