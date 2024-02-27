using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
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

    [Header("Button")]
    [SerializeField] Button startButton;

    ArabicFixer arabicFixer;
    LevelButton selectedLevelButton;
    LevelButtonChild selectedLevelButtonChild;


    //  level database
    private List<LevelData> levels = new List<LevelData>();

    void Start()
    {
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
                0,
                0,
                0
            },
            new List<int> { // grade score
                0,
                0,
                0
            }
        ));

        levels.Add(new LevelData(
            112,
            "Al-Ikhlas",
            "Ikhlas",
            "الإخلاص",
            new List<int> { // score
                0,
                0,
                0,
                0
            },
            new List<int> { // grade score
                0,
                0,
                0,
                0
            }
        ));

        levels.Add(new LevelData(
            113,
            "Al-Falaq",
            "Waktu Subuh",
            "الفلق",
            new List<int> { // score
                0
            },
            new List<int> { // grade score
                0
            }
        ));

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

            levelButton.GetComponent<LevelButton>().SetLevelData(level.surahNumber, level.surahName, level.idnName, CheckGradeScore(tempGradeScore/level.score.Count));
            levelButton.GetComponent<Button>().onClick.AddListener(() => ShowLevelData(level, levelButton.GetComponent<LevelButton>(), LevelContainer.GetComponent<RectTransform>()));
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
        // Create the child buttons for the level
        for (int i = 0; i < level.score.Count; i++)
        {
            tempScore += level.score[i];
            tempGradeScore += level.gradeScore[i];

            if (selectedLevelButtonChild) continue;
            // Increase the height of the level container
            levelContainerRectTransform.sizeDelta = new Vector2(levelContainerRectTransform.sizeDelta.x, levelContainerRectTransform.sizeDelta.y + 110);

            // Create a child button for each level
            GameObject levelButtonChild = Instantiate(LevelButtonChildPrefab, levelButton.transform.parent);
            levelButtonChild.GetComponent<LevelButtonChild>().SetLevelData(i + 1, level.score[i], CheckGradeScore(level.gradeScore[i]));
            levelButtonChild.GetComponent<Button>().onClick.AddListener(() => ShowChildLevelData(levelButtonChild.GetComponent<LevelButtonChild>()));
        }

        // Show the level data
        surahNumberText.text = level.surahNumber.ToString();
        surahNameText.text = level.surahName;
        idnNameText.text = level.idnName;
        arabicNameText.text = level.arabicName;
        arabicFixer.fixedText = level.arabicName;
        ayahText.text = level.score.Count + " Ayat";
        scoreText.text = tempScore.ToString();
        gradeScoreText.text = CheckGradeScore(tempGradeScore/level.score.Count);

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
        PlayerPrefs.SetInt("SurahNumber", int.Parse(surahNumberText.text));
        PlayerPrefs.SetInt("AyahNumber", int.Parse(selectedLevelButtonChild.TitleText.text.Split(' ')[1]));
        // load the game scene
        SceneManager.LoadScene("Gameplay");
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
