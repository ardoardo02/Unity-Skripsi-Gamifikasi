using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] AchievementDatabase achievementDatabase;
    [SerializeField] TopBar topBar;
    [Header("Level Menu")]
    [SerializeField] GameObject ContentContainer;
    [SerializeField] GameObject LevelContainerPrefab;
    [SerializeField] GameObject LevelButtonPrefab;
    [SerializeField] GameObject LevelButtonChildPrefab;

    [Header("Level Data")]
    [SerializeField] TMP_Text surahNumberText;
    [SerializeField] TMP_Text surahNameText;
    [SerializeField] TMP_Text idnNameText;
    [SerializeField] Text arabicNameText;
    [SerializeField] TMP_Text ayahText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text gradeScoreText;
    [SerializeField] GameObject lockIcon;

    [Header("Button")]
    [SerializeField] Button startButton;
    [SerializeField] Button buyButton;
    [SerializeField] TMP_Text buyButtonPriceText;

    ArabicFixer arabicFixer;
    LevelButton selectedLevelButton;
    LevelButtonChild selectedLevelButtonChild;
    int ach_total_surah = 0;
    int ach_total_surah_s = 0;

    //  level database
    private List<LevelData> levels = new List<LevelData>();

    // Keys for PlayerPrefs
    const string KEY_SURAH_NUMBER = "SURAH_NUMBER";
    const string KEY_AYAH_NUMBER = "AYAH_NUMBER";
    const string KEY_TOTAL_QUESTIONS = "TOTAL_QUESTIONS";
    const string KEY_HIGHSCORE = "HIGHSCORE_";
    const string KEY_GRADE = "GRADE_";
    const string KEY_PRICE = "PRICE_";
    const string KEY_COINS = "COINS";

    const string KEY_TOTAL_SURAH = "ACH_TOTAL_SURAH";
    const string KEY_TOTAL_SURAH_S = "ACH_TOTAL_SURAH_S";

    private void Awake() {
        arabicFixer = arabicNameText.GetComponent<ArabicFixer>();

        // string scoresString = PlayerPrefs.GetString("LevelScores");
        // string gradeScoresString = PlayerPrefs.GetString("LevelGradeScores");

        // // Deserialize the string to get back the level scores
        // List<List<int>> levelScores = DeserializeScores(scoresString);
        // List<List<int>> levelGradeScores = DeserializeScores(gradeScoresString);

        // Initialize some levels with example data
        levels.Add(new LevelData(
            1,
            "Al-Fatihah",
            "Pembukaan",
            "الفاتحة",
            new List<int> { // score
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "1_1", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "1_2", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "1_3", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "1_4", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "1_5", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "1_6", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "1_7", 0)
            },
            new List<int> { // grade score
                PlayerPrefs.GetInt(KEY_GRADE + "1_1", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "1_2", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "1_3", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "1_4", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "1_5", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "1_6", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "1_7", 0)
            },
            new List<int> { // total questions
                5,
                8,
                12,
                15,
                18,
                23,
                25
            },
            PlayerPrefs.GetInt(KEY_PRICE + "1", 1500)
        ));

        levels.Add(new LevelData(
            112,
            "Al-Ikhlas",
            "Ikhlas",
            "الإخلاص",
            new List<int> { // score
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "112_1", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "112_2", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "112_3", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "112_4", 0),
            },
            new List<int> { // grade score
                PlayerPrefs.GetInt(KEY_GRADE + "112_1", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "112_2", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "112_3", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "112_4", 0),
            },
            new List<int> { // total questions
                6,
                10,
                13,
                18
            },
            0
        ));

        levels.Add(new LevelData(
            113,
            "Al-Falaq",
            "Waktu Subuh",
            "الفلق",
            new List<int> { // score
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "113_1", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "113_2", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "113_3", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "113_4", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "113_5", 0)
            },
            new List<int> { // grade score
                PlayerPrefs.GetInt(KEY_GRADE + "113_1", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "113_2", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "113_3", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "113_4", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "113_5", 0)
            },
            new List<int> { // total questions
                6,
                10,
                15,
                17,
                20
            },
            PlayerPrefs.GetInt(KEY_PRICE + "113", 500)
        ));

        levels.Add(new LevelData(
            114,
            "An-Nas",
            "Manusia",
            "الناس",
            new List<int> { // score
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "114_1", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "114_2", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "114_3", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "114_4", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "114_5", 0),
                PlayerPrefs.GetInt(KEY_HIGHSCORE + "114_6", 0)
            },
            new List<int> { // grade score
                PlayerPrefs.GetInt(KEY_GRADE + "114_1", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "114_2", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "114_3", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "114_4", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "114_5", 0),
                PlayerPrefs.GetInt(KEY_GRADE + "114_6", 0)
            },
            new List<int> { // total questions
                6,
                9,
                12,
                16,
                18,
                22
            },
            PlayerPrefs.GetInt(KEY_PRICE + "114", 1000)
        ));
    }

    void Start()
    {
        GetLevels();
    }

    void GetLevels()
    {
        // remove all the level buttons
        foreach (Transform child in ContentContainer.transform) {
            // remove listeners
            if (child.GetComponent<Button>())
                child.GetComponent<Button>().onClick.RemoveAllListeners();

            Destroy(child.gameObject);
        }

        // reset the total surah and total surah with S achievements
        ach_total_surah = 0;
        ach_total_surah_s = 0;

        levels.Sort((x, y) => {
            // return x.surahNumber.CompareTo(y.surahNumber); // sort by surah number
            return x.price.CompareTo(y.price); // sort by price
        });

        // Create level buttons
        foreach (LevelData level in levels)
        {
            GameObject LevelContainer = Instantiate(LevelContainerPrefab, ContentContainer.transform);
            LevelContainer.name = "Level " + level.surahNumber;

            GameObject levelButton = Instantiate(LevelButtonPrefab, LevelContainer.transform);
            
            int tempGradeScore = 0;
            for (int i = 0; i < level.score.Count; i++){
                tempGradeScore += level.gradeScore[i];
            }

            levelButton.GetComponent<LevelButton>().SetLevelData(level.surahNumber, level.surahName, level.idnName, CheckGradeScore(tempGradeScore/level.score.Count), level.price);
            levelButton.GetComponent<Button>().onClick.AddListener(() => ShowLevelData(level, levelButton.GetComponent<LevelButton>(), LevelContainer.GetComponent<RectTransform>()));

            // increment achievement
            if (level.price == 0) ach_total_surah++;
            if (CheckGradeScore(tempGradeScore/level.score.Count) == "S") ach_total_surah_s++;
        }

        // Save the achievements
        if (ach_total_surah > PlayerPrefs.GetInt(KEY_TOTAL_SURAH, 0)) { // save the total surah achievement
            PlayerPrefs.SetInt(KEY_TOTAL_SURAH, ach_total_surah); 
            achievementDatabase.achievements.Find(ach => ach.key == KEY_TOTAL_SURAH).progress = ach_total_surah;
        }
        if (ach_total_surah_s > PlayerPrefs.GetInt(KEY_TOTAL_SURAH_S, 0)) { // save the total surah with S achievement
            PlayerPrefs.SetInt(KEY_TOTAL_SURAH_S, ach_total_surah_s);
            achievementDatabase.achievements.Find(ach => ach.key == KEY_TOTAL_SURAH_S).progress = ach_total_surah_s;
        } 
        
        // if(levelScores[0][0])
        // Debug.Log("score database 1: " + levelScores[0][0]);
        // Debug.Log("grade score database 3: " + levelGradeScores[1][2]);
    }

    // Show the level data when a level button is clicked
    void ShowLevelData(LevelData level, LevelButton levelButton, RectTransform levelContainerRectTransform)
    {
        if (selectedLevelButton == levelButton && selectedLevelButtonChild == null){ // if the same level is clicked
            Debug.Log("Level " + level.surahNumber + " is already selected");
            return;
        }

        // Change the color of the selected level button
        if (selectedLevelButton != null && selectedLevelButton != levelButton){
            if (selectedLevelButtonChild){
                selectedLevelButtonChild = null;
                startButton.interactable = false;
                startButton.GetComponentInChildren<TMP_Text>().text = "PILIHLAH LEVEL...";
            } else selectedLevelButton.ChangeColor();

            // Delete the child buttons of the previous level
            foreach (Transform child in selectedLevelButton.transform.parent) {
                if (child != selectedLevelButton.transform) {
                    child.GetComponent<Button>().onClick.RemoveAllListeners();
                    Destroy(child.gameObject);
                }
            }

            // Reset the height of the level container
            selectedLevelButton.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(selectedLevelButton.transform.parent.GetComponent<RectTransform>().sizeDelta.x, 150);
        }
        selectedLevelButton = levelButton;
        selectedLevelButton.ChangeColor();
        
        int tempScore = 0;
        int tempGradeScore = 0;

        if (level.price == 0){
            for (int i = 0; i < level.score.Count; i++) {
                tempScore += level.score[i];
                tempGradeScore += level.gradeScore[i];

                if (selectedLevelButtonChild) continue;
                // Increase the height of the level container
                levelContainerRectTransform.sizeDelta = new Vector2(levelContainerRectTransform.sizeDelta.x, levelContainerRectTransform.sizeDelta.y + 110);
                
                // Create a child button for each level
                GameObject levelButtonChild = Instantiate(LevelButtonChildPrefab, levelButton.transform.parent);
                levelButtonChild.GetComponent<LevelButtonChild>().SetLevelData(i + 1, level.score[i], CheckGradeScore(level.gradeScore[i]), i == 0 ? "unlock" : CheckGradeScore(level.gradeScore[i-1]), level.totalQuestions[i]);
                levelButtonChild.GetComponent<Button>().onClick.AddListener(() => ShowChildLevelData(levelButtonChild.GetComponent<LevelButtonChild>()));
            }
        }

        // Show the level data
        surahNumberText.text = level.surahNumber.ToString();
        surahNameText.text = level.surahName;
        idnNameText.text = level.idnName;
        // arabicNameText.text = level.arabicName;
        arabicFixer.fixedText = level.arabicName;
        ayahText.text = level.score.Count + " Ayat";
        if (level.price > 0){ // if the level is locked
            scoreText.text = "";
            gradeScoreText.text = "";
            buyButtonPriceText.text = level.price.ToString();
            
            lockIcon.SetActive(true);
            startButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
        } else { // if the level is unlocked
            lockIcon.SetActive(false);
            buyButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);

            scoreText.text = tempScore.ToString();
            gradeScoreText.text = CheckGradeScore(tempGradeScore/level.score.Count);
        }

        if (selectedLevelButtonChild){
            selectedLevelButtonChild.ChangeColor();
            selectedLevelButtonChild = null;
            startButton.interactable = false;
            startButton.GetComponentInChildren<TMP_Text>().text = "PILIHLAH LEVEL...";
        }
    }

    void ShowChildLevelData(LevelButtonChild levelButtonChild)
    {
        if (selectedLevelButtonChild == levelButtonChild){
            Debug.Log("Level " + levelButtonChild.TitleText.text + " is already selected");
            return;
        }

        if (selectedLevelButtonChild != null){
            selectedLevelButtonChild.ChangeColor();
        } else {
            selectedLevelButton.ChangeColor();
        }
        selectedLevelButtonChild = levelButtonChild;
        selectedLevelButtonChild.ChangeColor();
        startButton.interactable = true;
        startButton.GetComponentInChildren<TMP_Text>().text = "MULAI";

        scoreText.text = levelButtonChild.ScoreText.text == "" ? "0" : levelButtonChild.ScoreText.text;
        gradeScoreText.text = levelButtonChild.GradeScoreText.text;
    }

    private string CheckGradeScore(int gradeScore)
    {
        return gradeScore > 99 ? "S" : gradeScore > 80 ? "A" : gradeScore > 60 ? "B" : gradeScore > 0 ? "C" : "";
    }

    public void OnStartButtonClicked()
    {
        if (selectedLevelButtonChild == null){
            Debug.Log("Please select a level");
            return;
        }
        PlayerPrefs.SetInt(KEY_SURAH_NUMBER, int.Parse(surahNumberText.text));
        PlayerPrefs.SetInt(KEY_AYAH_NUMBER, int.Parse(selectedLevelButtonChild.TitleText.text.Split(' ')[1]));
        PlayerPrefs.SetInt(KEY_TOTAL_QUESTIONS, selectedLevelButtonChild.TotalQuestions);
        // load the game scene
        SceneManager.LoadScene("Gameplay");
    }

    public void OnBuyButtonClicked()
    {
        int price = int.Parse(buyButtonPriceText.text);

        if (PlayerPrefs.GetInt(KEY_COINS, 0) >= price){ // if the player has enough coins
            topBar.UseCoins(price);
            PlayerPrefs.SetInt(KEY_PRICE + surahNumberText.text, 0); // unlock the level

            levels.Find(level => level.surahNumber == int.Parse(surahNumberText.text)).price = 0; // unlock the level

            GetLevels();
        } else {
            StartCoroutine(NotEnoughCoins());
        }
    }

    IEnumerator NotEnoughCoins()
    {
        buyButton.interactable = false;
        buyButton.GetComponentInChildren<TMP_Text>().text = "TIDAK CUKUP KOIN";
        yield return new WaitForSeconds(2);
        buyButton.GetComponentInChildren<TMP_Text>().text = "BELI";
        buyButton.interactable = true;
    }

    // Deserialize string to get back the level scores
    // List<List<int>> DeserializeScores(string scoresString)
    // {
    //     List<List<int>> levelScores = new List<List<int>>();
    //     string[] levelScoreStrings = scoresString.Split(';');
    //     foreach (string scoreString in levelScoreStrings)
    //     {
    //         List<int> scores = new List<int>();
    //         string[] scoreValues = scoreString.Split(',');
    //         foreach (string scoreValue in scoreValues)
    //         {
    //             int score;
    //             if (int.TryParse(scoreValue, out score)) // Try parsing the string to integer
    //             {
    //                 scores.Add(score);
    //             }
    //             else
    //             {
    //                 Debug.LogError("Failed to parse score: " + scoreValue);
    //             }
    //         }
    //         levelScores.Add(scores);
    //     }
    //     return levelScores;
    // }
}
