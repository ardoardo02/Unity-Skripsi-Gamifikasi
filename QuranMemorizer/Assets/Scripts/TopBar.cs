using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour
{
    [SerializeField] AchievementDatabase achievementDatabase;
    [Header("Profile")]
    [SerializeField] Transform profilePanel;
    [SerializeField] Button profileButton;
    [SerializeField] Image avatar;
    [SerializeField] Sprite girlSprite;
    [SerializeField] TMP_Text username;

    [Header("Coins")]
    [SerializeField] TMP_Text coins;

    [Header("Experience")]
    [SerializeField] TMP_Text level;
    [SerializeField] Image experienceBar;
    [SerializeField] int currentLevel = 1;
    [SerializeField] int currentXP = 0;
    [SerializeField] int baseXP = 50; // Base XP required for reaching level 2
    [SerializeField] float levelMultiplier = 1.5f; // Multiplier to increase XP for each level

    public Image Avatar { get => avatar; set => avatar = value; }
    public TMP_Text Username { get => username; set => username = value; }
    public int CurrentLevel { get => currentLevel; }
    public int CurrentXP { get => currentXP; }

    // Keys for PlayerPrefs
    const string KEY_CHARACTER = "CHARACTER";
    const string KEY_USERNAME = "USERNAME";
    const string KEY_COINS = "COINS";
    const string KEY_LEVEL = "LEVEL";
    const string KEY_EXPERIENCE = "EXPERIENCE";
    const string KEY_TOTAL_COIN = "ACH_TOTAL_COIN";


    private void Awake() {
        profileButton.onClick.AddListener(() => {
            profilePanel.gameObject.SetActive(true);
        });
    }

    private void Start() {
        // set avatar
        if (PlayerPrefs.HasKey(KEY_CHARACTER)) {
            if (PlayerPrefs.GetString(KEY_CHARACTER) == "girl") {
                avatar.sprite = girlSprite;
            }
        }

        // set username
        if (PlayerPrefs.HasKey(KEY_USERNAME)) {
            username.text = PlayerPrefs.GetString(KEY_USERNAME);
        }

        // set coins
        coins.text = PlayerPrefs.GetInt(KEY_COINS).ToString();

        // set level
        currentLevel = PlayerPrefs.GetInt(KEY_LEVEL, 1);
        level.text = currentLevel.ToString();

        // set experience
        currentXP = PlayerPrefs.GetInt(KEY_EXPERIENCE);
        CheckLevelUp(); 
    }

    public void GainCoins(int amount)
    {
        int totalCoins = PlayerPrefs.GetInt(KEY_COINS, 0) + amount;
        PlayerPrefs.SetInt(KEY_COINS, totalCoins);
        coins.text = totalCoins.ToString();

        // Update total coin achievement
        int totalCoinAchievement = PlayerPrefs.GetInt(KEY_TOTAL_COIN, 0);
        totalCoinAchievement += amount;
        PlayerPrefs.SetInt(KEY_TOTAL_COIN, totalCoinAchievement);
        
        achievementDatabase.achievements.Find(ach => ach.key == KEY_TOTAL_COIN).progress += amount;
    }

    public void UseCoins(int amount)
    {
        int totalCoins = PlayerPrefs.GetInt(KEY_COINS, 0) - amount;
        PlayerPrefs.SetInt(KEY_COINS, totalCoins);
        coins.text = totalCoins.ToString();
    }

    public void GainExperience(int amount)
    {
        currentXP += amount;
        CheckLevelUp();

        PlayerPrefs.SetInt(KEY_EXPERIENCE, currentXP);
    }

    void CheckLevelUp()
    {
        while (currentXP >= XPForNextLevel(currentLevel))
        {
            currentXP -= XPForNextLevel(currentLevel); // Remove XP for next level
            currentLevel++; // Increase level
            level.text = currentLevel.ToString(); // Update UI
            PlayerPrefs.SetInt(KEY_LEVEL, currentLevel);

            // Handle level-up logic (e.g., increase player stats, unlock abilities)
            Debug.Log("Leveled Up! Current Level: " + currentLevel);
        }

        // Update experience bar
        experienceBar.fillAmount = (float)currentXP / XPForNextLevel(currentLevel);
    }

    public int XPForNextLevel(int level)
    {
        return Mathf.RoundToInt(baseXP * Mathf.Pow(levelMultiplier, level - 1));
    }
}
