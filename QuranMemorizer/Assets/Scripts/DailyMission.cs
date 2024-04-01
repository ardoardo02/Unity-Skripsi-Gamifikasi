using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyMission : MonoBehaviour
{
    [SerializeField] TopBar topBar;
    [SerializeField] AchievementDatabase achievementDatabase;
    [SerializeField] MissionDatabase missionDatabase;
    [Header("Daily Mission")]
    [SerializeField] GameObject missionPrefab;
    [SerializeField] Transform missionParent;
    [Header("Login Streak")]
    [SerializeField] TMP_Text streakText;
    [SerializeField] Image streakBarImage;

    // PlayerPrefs keys
    private const string KEY_LAST_LOGIN = "LAST_LOGIN_DATE";
    private const string KEY_STREAK = "LOGIN_STREAK";
    private const string KEY_LAST_MISSION = "LAST_MISSION";
    private const string KEY_CLAIMED = "_CLAIMED";

    const string KEY_DAILY_MISSION = "ACH_DAILY_MISSION";
    const string KEY_LOGIN_STREAK = "ACH_LOGIN_STREAK";

    void Start()
    {
        // Check if the player has logged in before
        if (PlayerPrefs.HasKey(KEY_LAST_LOGIN))
        {
            // Get the last login date from PlayerPrefs
            string lastLoginDate = PlayerPrefs.GetString(KEY_LAST_LOGIN);

            // Check if the last login date is yesterday
            if (DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy") == lastLoginDate)
            {
                // Player has logged in yesterday
                IncrementStreak();
                GetNewMission();
            }
            // Check if the last login date is today
            else if (DateTime.Now.ToString("dd/MM/yyyy") == lastLoginDate)
            {
                // Player has already logged in today
                Debug.Log("Player has already logged in today");
                GetTodaysMission();
            }
            else
            {
                // Player has not logged in for a day or more
                ResetStreak();
                GetNewMission();
            }
        }
        else
        {
            // First time logging in, set the last login date to today
            PlayerPrefs.SetString(KEY_LAST_LOGIN, DateTime.Now.ToString("dd/MM/yyyy"));
            PlayerPrefs.SetInt(KEY_STREAK, 1);

            GetNewMission();
        }

        DisplayLoginStreak();
    }

    private void OnDestroy() {
        // Disconnect the claim button listeners
        foreach (Transform child in missionParent) {
            child.GetComponent<Mission>().ClaimButton.onClick.RemoveAllListeners();
        }
    }

    void GetNewMission()
    {
        // Clear the mission parent
        foreach (Transform child in missionParent) {
            Destroy(child.gameObject);
        }

        // Get 3 random new set of missions
        List<MissionDatabase.MissionData> newMissions = new List<MissionDatabase.MissionData>();

        while (newMissions.Count < 3) {
            int randomIndex = UnityEngine.Random.Range(0, missionDatabase.missions.Count);

            if (!newMissions.Contains(missionDatabase.missions[randomIndex])) {
                // Create a new mission object
                GameObject mission = Instantiate(missionPrefab, missionParent);
                
                mission.GetComponent<Mission>().SetMissionData(
                    missionDatabase.missions[randomIndex].key,
                    missionDatabase.missions[randomIndex].missionText,
                    missionDatabase.missions[randomIndex].progress,
                    missionDatabase.missions[randomIndex].goal,
                    missionDatabase.missions[randomIndex].reward,
                    missionDatabase.missions[randomIndex].isClaimed
                );

                // Add button listener to claim button
                mission.GetComponent<Mission>().ClaimButton.onClick.AddListener(() => ClaimMissionReward(missionDatabase.missions[randomIndex].key));

                // Reset the mission progress and claimed status
                PlayerPrefs.SetInt(missionDatabase.missions[randomIndex].key, 0);
                PlayerPrefs.SetInt(missionDatabase.missions[randomIndex].key + KEY_CLAIMED, 0);
                mission.GetComponent<Mission>().ReSetMission(0, missionDatabase.missions[randomIndex].goal);

                // Save the mission to PlayerPrefs
                string lastMission = PlayerPrefs.GetString(KEY_LAST_MISSION, "0:0:0");
                string[] splitLastMission = lastMission.Split(':');
                splitLastMission[newMissions.Count] = randomIndex.ToString();
                lastMission = string.Join(":", splitLastMission);
                PlayerPrefs.SetString(KEY_LAST_MISSION, lastMission);

                // Add the mission to the new missions list
                newMissions.Add(missionDatabase.missions[randomIndex]);
            }

            Debug.Log("New mission Index: " + randomIndex);
        }
    }

    void GetTodaysMission()
    {
        // Clear the mission parent
        foreach (Transform child in missionParent) {
            Destroy(child.gameObject);
        }

        // Get the missions from PlayerPrefs
        string lastMission = PlayerPrefs.GetString(KEY_LAST_MISSION, "0:1:2");
        string[] splitLastMission = lastMission.Split(':');

        for (int i = 0; i < splitLastMission.Length; i++) {
            int index = i;

            // Create a new mission object
            GameObject mission = Instantiate(missionPrefab, missionParent);
            
            mission.GetComponent<Mission>().SetMissionData(
                missionDatabase.missions[int.Parse(splitLastMission[i])].key,
                missionDatabase.missions[int.Parse(splitLastMission[i])].missionText,
                missionDatabase.missions[int.Parse(splitLastMission[i])].progress,
                missionDatabase.missions[int.Parse(splitLastMission[i])].goal,
                missionDatabase.missions[int.Parse(splitLastMission[i])].reward,
                missionDatabase.missions[int.Parse(splitLastMission[i])].isClaimed
            );

            // Add button listener to claim button
            mission.GetComponent<Mission>().ClaimButton.onClick.AddListener(() => ClaimMissionReward(missionDatabase.missions[int.Parse(splitLastMission[index])].key));
        }
    }

    void IncrementStreak()
    {
        // Increment the login streak
        int currentStreak = PlayerPrefs.GetInt(KEY_STREAK, 0);
        PlayerPrefs.SetInt(KEY_STREAK, currentStreak + 1);
        if (currentStreak + 1 > PlayerPrefs.GetInt(KEY_LOGIN_STREAK, 0)) {
            PlayerPrefs.SetInt(KEY_LOGIN_STREAK, currentStreak + 1);
        }

        // Update the last login date to today
        PlayerPrefs.SetString(KEY_LAST_LOGIN, DateTime.Now.ToString("dd/MM/yyyy"));
    }

    void ResetStreak()
    {
        // Reset the login streak to 1
        PlayerPrefs.SetInt(KEY_STREAK, 1);

        // Update the last login date to today
        PlayerPrefs.SetString(KEY_LAST_LOGIN, DateTime.Now.ToString("dd/MM/yyyy"));
    }

    void DisplayLoginStreak()
    {
        // Get the current streak
        int currentStreak = PlayerPrefs.GetInt(KEY_STREAK);

        // Get the streak achievement from the achievement database
        AchievementDatabase.AchievementData streakAchievement = achievementDatabase.achievements.Find(achievement => achievement.key == KEY_LOGIN_STREAK);
        int achievementIndex = 0;
        
        for (int i = 0; i < streakAchievement.isClaimed.Count; i++) {
            if (streakAchievement.isClaimed[i] == false) {
                achievementIndex = i;
                break;
            } else if (i == streakAchievement.isClaimed.Count - 1) {
                achievementIndex = i;
            }
        }

        // Display the login streak
        streakText.text = currentStreak + "/" + streakAchievement.goal[achievementIndex];
        streakBarImage.fillAmount = (float)currentStreak / streakAchievement.goal[achievementIndex];
    }

    void ClaimMissionReward(string key)
    {
        // Find the mission in the mission database
        MissionDatabase.MissionData mission = missionDatabase.missions.Find(mission => mission.key == key);

        // Claim the mission reward
        mission.isClaimed = true;
        PlayerPrefs.SetInt(key + KEY_CLAIMED, 1); // Save the claimed status to PlayerPrefs
        PlayerPrefs.SetInt(KEY_DAILY_MISSION, PlayerPrefs.GetInt(KEY_DAILY_MISSION, 0) + 1); // Increment the daily mission achievement progress
        topBar.GainCoins(mission.reward); // Gain the mission reward


        // // Update the mission in the mission database
        // int index = missionDatabase.missions.FindIndex(m => m.key == key);
        // missionDatabase.missions[index] = mission;

        // Update the mission in the mission parent
        foreach (Transform child in missionParent) {
            if (child.GetComponent<Mission>().Key == key) {
                child.GetComponent<Mission>().ClaimReward();
            }
        }
    }
}
