using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    public class AchievementData
    {
        public string icon;
        public List<string> title;
        public string description;
        public int progress;
        public List<int> goal;
        public List<int> rewards;
        public List<bool> isClaimed;

        public AchievementData(string icon, List<string> title, string description, int progress, List<int> goal, List<int> rewards, List<bool> isClaimed)
        {
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
    private List<AchievementData> achievements = new List<AchievementData>();

    // references
    public GameObject badgePrefab;
    public GameObject horizontalLayoutPrefab;
    public GameObject emptyObjectPrefab;
    public Transform badgetParent;

    private void Awake() {
        // Populate the achievement database with sample achievements
        achievements.Add(new AchievementData(
            "Sprites/TargetIcon",
            new List<string> {"Misi Selesai", "Ahli Tugas", "Pekerja Keras", "Pahlawan Harian"},
            "Selesaikan misi harian",
            0,
            new List<int> {1, 5, 15, 30},
            new List<int> {20, 100, 300, 500},
            new List<bool> {false, false, false, false}
        ));

        achievements.Add(new AchievementData(
            "Sprites/CoinIcon",
            new List<string> {"Aku Punya Koin", "Kolektor Koin", "Kaya Raya", "Bankir"},
            "Kumpulkan jumlah koin",
            0,
            new List<int> {100, 1000, 5000, 10000},
            new List<int> {20, 150, 300, 500},
            new List<bool> {false, false, false, false}
        ));

        achievements.Add(new AchievementData(
            "Sprites/BookIcon",
            new List<string> {"Pembaca", "Pencari Surah", "Penggemar Al-Quran", "Penikmat Al-Quran"},
            "Jumlah surah yang dipunyai",
            0,
            new List<int> {2, 5, 10, 20},
            new List<int> {50, 100, 500, 750},
            new List<bool> {false, false, false, false}
        ));

        achievements.Add(new AchievementData(
            "Sprites/QuranIcon",
            new List<string> {"Penghafal", "Ahli Tafsir", "Hafizh", "Ulama"},
            "Jumlah surah dengan skor S",
            0,
            new List<int> {1, 4, 10, 20},
            new List<int> {50, 150, 500, 1250},
            new List<bool> {false, false, false, false}
        ));

        achievements.Add(new AchievementData(
            "Sprites/StreakIcon",
            new List<string> {"Pemula Sempurna", "Penampil Presisi", "Pengeksekusi Ahli", "Perfeksionis"},
            "Selesai dengan skor S berturut-turut",
            0,
            new List<int> {3, 6, 10, 20},
            new List<int> {50, 100, 350, 600},
            new List<bool> {false, false, false, false}
        ));

        achievements.Add(new AchievementData(
            "Sprites/LoginIcon",
            new List<string> {"Pengguna", "Pengunjung Harian", "Peserta Konsisten", "Penghuni Setia"},
            "Login setiap hari berturut-turut",
            0,
            new List<int> {3, 7, 14, 30},
            new List<int> {50, 100, 350, 600},
            new List<bool> {false, false, false, false}
        ));
    }

    private void OnEnable() {
        // Clear the badge parent
        foreach (Transform child in badgetParent) {
            Destroy(child.gameObject);
        }

        // Populate the badge parent with badges
        GameObject horizontalLayout = Instantiate(horizontalLayoutPrefab, badgetParent);
        for (int i = 0; i < achievements.Count; i++)
        {
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
                achievements[i].icon,
                iconColor,
                achievements[i].title[badgeStageIndex],
                achievements[i].description,
                achievements[i].rewards[badgeStageIndex].ToString(),
                achievements[i].progress,
                achievements[i].goal[badgeStageIndex],
                achievements[i].isClaimed[badgeStageIndex]
            );
        }

        // Create empty game object to fill the last row
        Instantiate(emptyObjectPrefab, badgetParent);
    }   
}
