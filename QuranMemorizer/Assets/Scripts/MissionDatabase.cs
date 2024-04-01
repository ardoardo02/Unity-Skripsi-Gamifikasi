using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDatabase : MonoBehaviour
{
    public class MissionData
    {
        public string key;
        public string missionText;
        public int progress;
        public int goal;
        public int reward;
        public bool isClaimed;

        public MissionData(string key, string missionText, int progress, int goal, int reward, bool isClaimed)
        {
            this.key = key;
            this.missionText = missionText;
            this.progress = progress;
            this.goal = goal;
            this.reward = reward;
            this.isClaimed = isClaimed;
        }
    }

    //  mission database
    public List<MissionData> missions = new List<MissionData>();

    // Keys for PlayerPrefs
    const string KEY_COIN = "MISSION_COIN";
    const string KEY_SCORE_S = "MISSION_SCORE_S";
    const string KEY_LEVEL = "MISSION_LEVEL";
    const string KEY_COMBO = "MISSION_COMBO";
    const string KEY_XP = "MISSION_XP";
    const string KEY_CLAIMED = "_CLAIMED";

    private void Awake() {
        // Populate the mission database with sample missions
        missions.Add(new MissionData(
            KEY_COIN,
            "Kumpulkan 100 koin",
            PlayerPrefs.GetInt(KEY_COIN, 0),
            100,
            35,
            PlayerPrefs.GetInt(KEY_COIN + KEY_CLAIMED, 0) == 1 
        ));

        missions.Add(new MissionData(
            KEY_SCORE_S,
            "Selesai dengan skor S di 2 level",
            PlayerPrefs.GetInt(KEY_SCORE_S, 0),
            2,
            50,
            PlayerPrefs.GetInt(KEY_SCORE_S + KEY_CLAIMED, 0) == 1 
        ));

        missions.Add(new MissionData(
            KEY_LEVEL,
            "Selesaikan 5 level",
            PlayerPrefs.GetInt(KEY_LEVEL, 0),
            5,
            30,
            PlayerPrefs.GetInt(KEY_LEVEL + KEY_CLAIMED, 0) == 1 
        ));

        missions.Add(new MissionData(
            KEY_COMBO,
            "Dapatkan 5x combo dalam 4 level",
            PlayerPrefs.GetInt(KEY_COMBO, 0),
            4,
            45,
            PlayerPrefs.GetInt(KEY_COMBO + KEY_CLAIMED, 0) == 1 
        ));

        missions.Add(new MissionData(
            KEY_XP,
            "Dapatkan 100 XP",
            PlayerPrefs.GetInt(KEY_XP, 0),
            100,
            25,
            PlayerPrefs.GetInt(KEY_XP + KEY_CLAIMED, 0) == 1 
        ));
    }
}
