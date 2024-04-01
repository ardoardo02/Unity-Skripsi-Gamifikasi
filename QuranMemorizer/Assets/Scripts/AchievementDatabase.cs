using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementDatabase : MonoBehaviour
{
    public class AchievementData
    {
        public string key;
        public string icon;
        public List<string> title;
        public string description;
        public int progress;
        public List<int> goal;
        public List<int> rewards;
        public List<bool> isClaimed;

        public AchievementData(string key, string icon, List<string> title, string description, int progress, List<int> goal, List<int> rewards, List<bool> isClaimed)
        {
            this.key = key;
            this.icon = icon;
            this.title = title;
            this.description = description;
            this.progress = progress;
            this.goal = goal;
            this.rewards = rewards;
            this.isClaimed = isClaimed;
        }
    }

    //  achivement database
    public List<AchievementData> achievements = new List<AchievementData>();

    // Keys for PlayerPrefs
    const string KEY_DAILY_MISSION = "ACH_DAILY_MISSION";
    const string KEY_TOTAL_COIN = "ACH_TOTAL_COIN";
    const string KEY_TOTAL_SURAH = "ACH_TOTAL_SURAH";
    const string KEY_TOTAL_SURAH_S = "ACH_TOTAL_SURAH_S";
    const string KEY_S_STREAK = "ACH_S_STREAK";
    const string KEY_LOGIN_STREAK = "ACH_LOGIN_STREAK";
    const string KEY_CLAIMED = "_CLAIMED";

    private void Awake() {
        // Populate the achievement database with sample achievements
        bool[] isClaimedData = DeserializeClaimedData(KEY_DAILY_MISSION);
        achievements.Add(new AchievementData(
            KEY_DAILY_MISSION,
            "Sprites/TargetIcon",
            new List<string> {"Misi Selesai", "Ahli Tugas", "Pekerja Keras", "Pahlawan Harian"},
            "Selesaikan misi harian",
            PlayerPrefs.GetInt(KEY_DAILY_MISSION, 0),
            new List<int> {1, 5, 15, 30},
            new List<int> {50, 250, 500, 750},
            new List<bool> {isClaimedData[0], isClaimedData[1], isClaimedData[2], isClaimedData[3]}
        ));

        isClaimedData = DeserializeClaimedData(KEY_TOTAL_COIN);
        achievements.Add(new AchievementData(
            KEY_TOTAL_COIN,
            "Sprites/CoinIcon",
            new List<string> {"Aku Punya Koin", "Kolektor Koin", "Kaya Raya", "Bankir"},
            "Kumpulkan jumlah koin",
            PlayerPrefs.GetInt(KEY_TOTAL_COIN, 0),
            new List<int> {100, 1000, 5000, 10000},
            new List<int> {50, 150, 300, 500},
            new List<bool> {isClaimedData[0], isClaimedData[1], isClaimedData[2], isClaimedData[3]}
        ));

        isClaimedData = DeserializeClaimedData(KEY_TOTAL_SURAH);
        achievements.Add(new AchievementData(
            KEY_TOTAL_SURAH,
            "Sprites/BookIcon",
            new List<string> {"Pembaca", "Pencari Surah", "Penggemar Al-Quran", "Penikmat Al-Quran"},
            "Jumlah surah yang dipunyai",
            PlayerPrefs.GetInt(KEY_TOTAL_SURAH, 0),
            new List<int> {2, 5, 10, 20},
            new List<int> {100, 300, 700, 1250},
            new List<bool> {isClaimedData[0], isClaimedData[1], isClaimedData[2], isClaimedData[3]}
        ));

        isClaimedData = DeserializeClaimedData(KEY_TOTAL_SURAH_S);
        achievements.Add(new AchievementData(
            KEY_TOTAL_SURAH_S,
            "Sprites/QuranIcon",
            new List<string> {"Penghafal", "Ahli Tafsir", "Hafizh", "Ulama"},
            "Jumlah surah dengan skor S",
            PlayerPrefs.GetInt(KEY_TOTAL_SURAH_S, 0),
            new List<int> {1, 4, 10, 20},
            new List<int> {100, 350, 800, 1500},
            new List<bool> {isClaimedData[0], isClaimedData[1], isClaimedData[2], isClaimedData[3]}
        ));

        isClaimedData = DeserializeClaimedData(KEY_S_STREAK);
        achievements.Add(new AchievementData(
            KEY_S_STREAK,
            "Sprites/StreakIcon",
            new List<string> {"Pemula Sempurna", "Penampil Presisi", "Pengeksekusi Ahli", "Perfeksionis"},
            "Selesai dengan skor S berturut-turut",
            PlayerPrefs.GetInt(KEY_S_STREAK, 0),
            new List<int> {3, 6, 10, 20},
            new List<int> {75, 150, 450, 800},
            new List<bool> {isClaimedData[0], isClaimedData[1], isClaimedData[2], isClaimedData[3]}
        ));

        isClaimedData = DeserializeClaimedData(KEY_LOGIN_STREAK);
        achievements.Add(new AchievementData(
            KEY_LOGIN_STREAK,
            "Sprites/LoginIcon",
            new List<string> {"Pengguna", "Pengunjung Harian", "Peserta Konsisten", "Penghuni Setia"},
            "Login setiap hari berturut-turut",
            PlayerPrefs.GetInt(KEY_LOGIN_STREAK, 0),
            new List<int> {3, 7, 14, 30},
            new List<int> {75, 150, 500, 1000},
            new List<bool> {isClaimedData[0], isClaimedData[1], isClaimedData[2], isClaimedData[3]}
        ));
    }

    bool[] DeserializeClaimedData(string key){
        Debug.Log("DeserializeClaimedData: " + key + "| Data: " + PlayerPrefs.GetString(key + KEY_CLAIMED, "0:0:0:0"));
        string[] tempData = PlayerPrefs.GetString(key + KEY_CLAIMED, "0:0:0:0").Split(':');
        bool[] data = new bool[tempData.Length];

        for (int i = 0; i < tempData.Length; i++) {
            data[i] = tempData[i] == "1";
        }
        return data;
    }

    // private void Start() {
    //     Debug.Log("Today is: " + DateTime.Now.ToString("dd/MM/yyyy"));
    //     Debug.Log("Yesterday is: " + DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
    //     Debug.Log("Tomorrow is: " + DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
    // }
}
