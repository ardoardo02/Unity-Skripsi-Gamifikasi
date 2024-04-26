using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    [SerializeField] TopBar topBar;
    [SerializeField] AchievementDatabase achievementDatabase;
    [SerializeField] GameObject badgePrefab;
    [SerializeField] GameObject horizontalLayoutPrefab;
    [SerializeField] GameObject emptyObjectPrefab;
    [SerializeField] Transform badgetParent;

    List<AchievementDatabase.AchievementData> achievements;

    const string KEY_CLAIMED = "_CLAIMED";

    
    private void OnEnable() {
        achievements = achievementDatabase.achievements;
        
        // Clear the badge parent
        foreach (Transform child in badgetParent) {
            Destroy(child.gameObject);
        }

        // Populate the badge parent with badges
        GameObject horizontalLayout = Instantiate(horizontalLayoutPrefab, badgetParent);
        for (int i = 0; i < achievements.Count; i++)
        {
            int index = i;

            if (i % 2 == 0 && i != 0) {
                horizontalLayout = Instantiate(horizontalLayoutPrefab, badgetParent);
            }

            int badgeStageIndex = 0;
            for (int j = 0; j < achievements[i].isClaimed.Count; j++) {
                if (achievements[i].isClaimed[j] == true) continue;

                badgeStageIndex = j;
                break;
            }

            Color32 iconColor;
            switch (badgeStageIndex)
            {
                case 0:
                    iconColor = new Color32(255, 135, 73, 255);
                    break;
                case 1:
                    iconColor = new Color32(238, 232, 169, 255);
                    break;
                case 2:
                    iconColor = new Color32(100, 193, 126, 255);
                    break;
                case 3:
                    iconColor = new Color32(146, 255, 255, 255);
                    break;
                default:
                    iconColor = new Color32(255, 255, 255, 255);
                    break;
            }

            GameObject badge = Instantiate(badgePrefab, horizontalLayout.transform);
            badge.GetComponent<Badge>().SetBadgeData(
                achievements[i].key,
                achievements[i].icon,
                iconColor,
                achievements[i].title[badgeStageIndex],
                achievements[i].description,
                achievements[i].rewards[badgeStageIndex].ToString(),
                achievements[i].progress,
                achievements[i].goal[badgeStageIndex],
                achievements[i].isClaimed[badgeStageIndex]
            );

            badge.GetComponent<Badge>().ClaimButton.onClick.AddListener(() => ClaimAchievement(achievements[index].key));
        }

        // Create empty game object to fill the last row
        Instantiate(emptyObjectPrefab, badgetParent);
    }

    public void ClaimAchievement(string key)
    {
        for (int i = 0; i < achievements.Count; i++)
        {
            if (achievements[i].key == key)
            {
                for (int j = 0; j < achievements[i].isClaimed.Count; j++)
                {
                    if (achievements[i].isClaimed[j] == true) continue;

                    if (achievements[i].progress >= achievements[i].goal[j])
                    {
                        achievements[i].isClaimed[j] = true;
                        AudioManager.instance.PlaySFX("ClickConfirm");

                        string[] tempData = PlayerPrefs.GetString(achievements[i].key + KEY_CLAIMED, "0:0:0:0").Split(':');
                        tempData[j] = "1";
                        PlayerPrefs.SetString(achievements[i].key + KEY_CLAIMED, string.Join(":", tempData));

                        topBar.GainCoins(achievements[i].rewards[j]);
                        PlayerPrefs.Save();
                        break;
                    }
                }
            }
        }

        OnEnable();
    }
}
