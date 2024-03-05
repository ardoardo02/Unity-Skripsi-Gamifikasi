using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int currentSurah;
    int currentAyah;
    int totalQuestions;

    int score = 0;
    int maxStreak = 0;
    int totalRightAnswers = 0;
    int currentQuestionNumber = 1;
    int selectedQuestionType;

    string multipleChoiceAnswer;
    string clickedMultipleChoiceAnswer;

    [SerializeField] QuestionDatabase questionDatabase;
    [SerializeField] Button submitButton;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject finishPanel;

    [Header("Progress Bar")]
    [SerializeField] TMP_Text progressText;
    [SerializeField] Image streakProgressBar;
    [SerializeField] TMP_Text streakText;

    [Header("Finish Panel")]
    [SerializeField] TMP_Text finishScoreText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text maxComboText;
    [SerializeField] TMP_Text accuracyText;
    [SerializeField] TMP_Text coinsEarnedText;
    [SerializeField] TMP_Text xpEarnedText;

    [Header("Finish Panel Grades and Characters")]
    [SerializeField] GameObject grade_S;
    [SerializeField] GameObject grade_A;
    [SerializeField] GameObject grade_B;
    [SerializeField] GameObject grade_C;
    [SerializeField] GameObject ustadz;
    [SerializeField] GameObject ustadzah;

    [Header("Question Type 1")]
    [SerializeField] GameObject questionType1;
    [SerializeField] TMP_Text mainQuestionText;
    [SerializeField] Text secondaryQuestionText;
    [SerializeField] Text answerText1_1;
    [SerializeField] Text answerText1_2;
    [SerializeField] Text answerText1_3;
    [SerializeField] Text answerText1_4;
    List<Text> answerTexts1 = new List<Text>();

    [Header("Question Type 2")]
    [SerializeField] GameObject questionType2;
    [SerializeField] TMP_Text mainQuestionText2;
    [SerializeField] Text answerText2_1;
    [SerializeField] Text answerText2_2;
    [SerializeField] Text answerText2_3;
    [SerializeField] Text answerText2_4;
    List<Text> answerTexts2 = new List<Text>();

    private void Awake() {
        // Get the surah and ayah data from the player prefs
        currentSurah = PlayerPrefs.GetInt("SurahNumber", 999);
        currentAyah = PlayerPrefs.GetInt("AyahNumber", 999);
        totalQuestions = PlayerPrefs.GetInt("TotalQuestions", 10);

        // Set the initial score and progress
        scoreText.text = score.ToString();
        progressText.text = "1/" + totalQuestions;
        streakProgressBar.fillAmount = 0;
        streakText.text = "0";

        // Add the answer texts to the list
        answerTexts1.Add(answerText1_1);
        answerTexts1.Add(answerText1_2);
        answerTexts1.Add(answerText1_3);
        answerTexts1.Add(answerText1_4);

        answerTexts2.Add(answerText2_1);
        answerTexts2.Add(answerText2_2);
        answerTexts2.Add(answerText2_3);
        answerTexts2.Add(answerText2_4);

        // Add listener to the answer buttons
        for (int i = 0; i < answerTexts1.Count; i++) {
            int index = i;
            answerTexts1[i].GetComponentInParent<Button>().onClick.AddListener(() => OnAnswerButtonClicked(index + 1));
        }

        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (currentSurah == 999 || currentAyah == 999) {
            Debug.Log("No surah and ayah data found");
        }
        
        Debug.Log("Current Surah: " + currentSurah);
        Debug.Log("Current Ayah: " + currentAyah);
        Debug.Log("Total Questions: " + totalQuestions);

        GetQuestionType1();
    }

    void GetQuestionType1() {
        selectedQuestionType = 1;

        int questionBasedOnAyah = currentAyah * 2;
        if (questionBasedOnAyah > questionDatabase.questions.Count) {
            questionBasedOnAyah = questionDatabase.questions.Count;
        }
        // Randomly select a question from the database
        var currentLevel = questionDatabase.questions[currentSurah];
        var currentQuestion = currentLevel[Random.Range(0, questionBasedOnAyah)];
        // var currentQuestion = questionDatabase.questions[currentSurah, Random.Range(0, questionBasedOnAyah)];

        // Display question text
        mainQuestionText.text = currentQuestion.mainQuestionText;
        secondaryQuestionText.GetComponent<ArabicFixer>().fixedText = currentQuestion.secondaryQuestionText;
        
        // Get the correct answer
        var currentOptions = questionDatabase.options[currentSurah];
        multipleChoiceAnswer = currentOptions[currentQuestion.correctAnswerIndex];

        // Randomize the options
        List<string> randomizedOptions = new List<string>(currentOptions);
        randomizedOptions = randomizedOptions.OrderBy(x => UnityEngine.Random.value).ToList(); // Use OrderBy with a random value to shuffle the list
        
        // Display options
        bool isAnswerSet = false;
        for (int i = 0; i < answerTexts1.Count; i++) {
            answerTexts1[i].GetComponent<ArabicFixer>().fixedText = randomizedOptions[i];
            if (!isAnswerSet && randomizedOptions[i] == multipleChoiceAnswer) {
                isAnswerSet = true;
            }
        };

        // Randomly place the correct 
        if (!isAnswerSet) {
            int randomAnswerIndex = Random.Range(0, 4);
            answerTexts1[randomAnswerIndex].GetComponent<ArabicFixer>().fixedText = multipleChoiceAnswer;
        }

        questionType1.SetActive(true);
        Debug.Log("Correct answer: " + multipleChoiceAnswer);
    }

    public void OnAnswerButtonClicked(int answerIndex) {
        // Store the clicked answer
        clickedMultipleChoiceAnswer = answerTexts1[answerIndex - 1].text;

        // Reset the color of all answers
        for (int i = 0; i < answerTexts1.Count; i++) {
            answerTexts1[i].GetComponentInParent<Image>().color = new Color32(255, 255, 255, 255);
            answerTexts1[i].color = new Color32(51, 51, 51, 255);
        }

        // Change the color of the clicked answer
        answerTexts1[answerIndex - 1].GetComponentInParent<Image>().color = new Color32(0, 169, 171, 255);
        answerTexts1[answerIndex - 1].color = new Color32(255, 255, 255, 255);

        submitButton.interactable = true;
    }

    public void OnSubmitButtonClicked() {
        StartCoroutine(OnSubmitButtonClicked_Coroutine());
    }

    IEnumerator OnSubmitButtonClicked_Coroutine() {
        submitButton.interactable = false;
        ColorBlock colors = submitButton.colors;

        // Check if the clicked answer is the correct answer
        if (clickedMultipleChoiceAnswer == multipleChoiceAnswer) {
            colors.disabledColor = new Color32(100, 193, 126, 255); // Correct answer color
            totalRightAnswers++;

            // Add score
            score += 100;
            scoreText.text = score.ToString();

            // Add streak
            streakText.text = (int.Parse(streakText.text) + 1).ToString();
            streakProgressBar.fillAmount = float.Parse(streakText.text) / totalQuestions;
            if (int.Parse(streakText.text) > maxStreak) maxStreak = int.Parse(streakText.text);
            
        } else {
            colors.disabledColor = new Color32(171, 0, 0, 255); // Wrong answer color

            // Reset streak
            streakText.text = "0";
            streakProgressBar.fillAmount = 0;
        }

        submitButton.colors = colors;

        // Wait for 2 seconds before changing the color back to normal
        yield return new WaitForSeconds(2);
        colors.disabledColor = new Color32(185, 185, 185, 255);
        submitButton.colors = colors;

        // Reset the color of all answers
        for (int i = 0; i < answerTexts1.Count; i++) {
            answerTexts1[i].GetComponentInParent<Image>().color = new Color32(255, 255, 255, 255);
            answerTexts1[i].color = new Color32(51, 51, 51, 255);
        }

        // Reset state
        clickedMultipleChoiceAnswer = "";
        multipleChoiceAnswer = "";
        selectedQuestionType = 0;
        questionType1.SetActive(false);
        
        // Update the question number
        currentQuestionNumber++;
        progressText.text = currentQuestionNumber + "/" + totalQuestions;
        
        // Get the next question
        if (currentQuestionNumber > totalQuestions) {
            SetUpFinishPanel();
        } else {
            GetQuestionType1();
        }
    }

    void SetUpFinishPanel() {
        // Getting keys for high score and grade
        string highScoreKey = "HighScore_" + currentSurah + "_" + currentAyah;
        string gradeKey = "Grade_" + currentSurah + "_" + currentAyah;

        // Calculate the accuracy
        int accuracy = (int)((float)totalRightAnswers / totalQuestions * 100);

        // Set the finish panel texts
        finishScoreText.text = score.ToString();
        highScoreText.text = PlayerPrefs.GetInt(highScoreKey, 0).ToString();
        maxComboText.text = maxStreak.ToString();
        accuracyText.text = accuracy + "%";
        coinsEarnedText.text = "0";
        xpEarnedText.text = "0";

        // Set the grade
        grade_S.SetActive(false);
        if (accuracy > 99) {
            grade_S.SetActive(true);
        } else if (accuracy > 80) {
            grade_A.SetActive(true);
        } else if (accuracy > 60) {
            grade_B.SetActive(true);
        } else {
            grade_C.SetActive(true);
        }

        // Randomly select a character
        if (Random.Range(0, 2) == 0) { // if 0, display ustadzah : else, display ustadz
            ustadz.SetActive(false);
            ustadzah.SetActive(true);
        }

        // Display the finish panel
        finishPanel.SetActive(true);

        // Save the high score and grade
        if (score > PlayerPrefs.GetInt(highScoreKey, 0)) {
            PlayerPrefs.SetInt(highScoreKey, score);
        }
        if (accuracy > PlayerPrefs.GetInt(gradeKey, 0)) {
            PlayerPrefs.SetInt(gradeKey, accuracy);
        }
    }
}
