using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    [SerializeField] TopBar topBar;
    [Header("Top Panel")]
    [SerializeField] TMP_Text usernameText;
    [SerializeField] Transform editUsernamePanel;
    [SerializeField] TMP_InputField editUsernameInput;
    [SerializeField] Button editUsernameButton;
    [SerializeField] Image avatar;
    [SerializeField] Sprite boySprite;
    [SerializeField] Sprite girlSprite;
    [SerializeField] TMP_Text levelText;
    [SerializeField] Image levelBar;
    Transform usernamePanel;

    [Header("Middle Panel")]
    [SerializeField] TMP_Text streakText;
    [SerializeField] TMP_Text completedSurahText;

    [Header("Bottom Panel")]
    [SerializeField] AchievementDatabase achievementDatabase;
    [SerializeField] GameObject badgePrefab;
    [SerializeField] Transform badgeParent;

    private void Start() {
        usernamePanel = usernameText.transform.parent;
    }

    // Keys for PlayerPrefs
    const string KEY_CHARACTER = "CHARACTER";
    const string KEY_USERNAME = "USERNAME";
    const string KEY_LOGIN_STREAK = "ACH_LOGIN_STREAK";
    const string KEY_COMPLETED_SURAH = "COMPLETED_SURAH";

    private void OnEnable() {
        // ~~~~~~~~~~~~~~~~~~~~ Set top panel ~~~~~~~~~~~~~~~~~~~~
        // Set avatar
        if (PlayerPrefs.HasKey(KEY_CHARACTER)) {
            if (PlayerPrefs.GetString(KEY_CHARACTER) == "girl")
                avatar.sprite = girlSprite;
        }

        // Set username
        if (PlayerPrefs.HasKey(KEY_USERNAME)) {
            string username = PlayerPrefs.GetString(KEY_USERNAME);
            usernameText.text = username;
            editUsernameInput.text = username;
        }

        // Set level
        levelText.text = topBar.CurrentLevel.ToString();
        levelBar.fillAmount = (float)topBar.CurrentXP / topBar.XPForNextLevel(topBar.CurrentLevel);

        // ~~~~~~~~~~~~~~~~~~~~ Set middle panel ~~~~~~~~~~~~~~~~~~~~
        // Set streak
        streakText.text = PlayerPrefs.GetInt(KEY_LOGIN_STREAK, 0).ToString();

        // Set completed surah
        completedSurahText.text = PlayerPrefs.GetInt(KEY_COMPLETED_SURAH, 0).ToString();

        // ~~~~~~~~~~~~~~~~~~~~ Set bottom panel ~~~~~~~~~~~~~~~~~~~~
        // Clear the badge parent
        foreach (Transform child in badgeParent) {
            Destroy(child.gameObject);
        }

        // Populate the badge parent with badges
        List<AchievementDatabase.AchievementData> achievements = achievementDatabase.achievements;
        
        foreach (AchievementDatabase.AchievementData achievement in achievements) {
            for(int i = 0; i < achievement.isClaimed.Count; i++) {
                if (i == 0 && achievement.isClaimed[i] == false) break; //if stage 0 is not claimed, break

                GameObject badge; 
                Image badgeImg;

                if (achievement.isClaimed[i] == true && i == achievement.isClaimed.Count - 1) { //if all stages are claimed (diamond)
                    badge = Instantiate(badgePrefab, badgeParent);
                
                    badgeImg = badge.transform.GetChild(0).GetComponent<Image>();
                    badgeImg.sprite = Resources.Load<Sprite>(achievement.icon);
                    badgeImg.color = new Color32(146, 255, 255, 255);
                    break;
                }
                
                if (achievement.isClaimed[i] == true) continue;

                badge = Instantiate(badgePrefab, badgeParent);
                
                badgeImg = badge.transform.GetChild(0).GetComponent<Image>();
                badgeImg.sprite = Resources.Load<Sprite>(achievement.icon);

                switch (i) {
                    case 1: // if stage 1 is claimed (bronze)
                        badgeImg.color = new Color32(255, 135, 73, 255);
                        break;
                    case 2: // if stage 2 is claimed (gold)
                        badgeImg.color = new Color32(238, 232, 169, 255);
                        break;
                    case 3: // if stage 3 is claimed (green)
                        badgeImg.color = new Color32(100, 193, 126, 255);
                        break;
                    default:
                        badgeImg.color = new Color32(255, 255, 255, 255);
                        break;
                } 

                break;
            }
        }
    }

    public void SaveUsername() {
        string newUsername = editUsernameInput.text;

        if (PlayerPrefs.GetString(KEY_USERNAME) != newUsername) {
            usernameText.text = newUsername;
            topBar.Username.text = newUsername;

            PlayerPrefs.SetString(KEY_USERNAME, newUsername);
        }

        editUsernamePanel.gameObject.SetActive(false);
        usernamePanel.gameObject.SetActive(true);
    }

    public void OnUsernameValueEdit() {
        string newUsername = editUsernameInput.text;
        editUsernameButton.interactable = newUsername.Length >= 4 && newUsername.Length <= 12;
    }

    public void ChangeAvatar() {
        string currentCharacter = PlayerPrefs.GetString(KEY_CHARACTER);

        if (currentCharacter == "girl") {
            avatar.sprite = boySprite;
            topBar.Avatar.sprite = boySprite;
            PlayerPrefs.SetString(KEY_CHARACTER, "boy");
        } else {
            avatar.sprite = girlSprite;
            topBar.Avatar.sprite = girlSprite;
            PlayerPrefs.SetString(KEY_CHARACTER, "girl");
        }
    }
}
