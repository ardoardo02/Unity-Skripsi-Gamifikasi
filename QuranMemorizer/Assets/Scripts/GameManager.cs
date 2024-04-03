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
    
    float timer = 0;
    [SerializeField] float timerDuration = 61;

    int score = 0;
    int maxStreak = 0;
    int totalRightAnswers = 0;
    int currentQuestionNumber = 1;
    int selectedQuestionType;

    string multipleChoiceAnswer;
    string clickedMultipleChoiceAnswer;

    string topMatchingAnswer;
    string bottomMatchingAnswer;
    int matchCounter = 0;
    bool isWrongAnswer = false;

    [SerializeField] QuestionDatabase questionDatabase;
    [SerializeField] Button submitButton;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Image timerBar;
    [SerializeField] GameObject loadingPanel;
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
    [SerializeField] AudioSource audioQuestion2;
    [SerializeField] Text answerText2_1;
    [SerializeField] Text answerText2_2;
    [SerializeField] Text answerText2_3;
    [SerializeField] Text answerText2_4;
    [SerializeField] Sprite notPlayingAudioIcon;
    [SerializeField] Sprite playingAudioIcon;
    List<Text> answerTexts2 = new List<Text>();
    Button audioButton2;

    [Header("Question Type 3")]
    [SerializeField] GameObject questionType3;
    [SerializeField] Text secondaryQuestionText3;
    [SerializeField] List<Text> topAnswerTexts;
    [SerializeField] List<Text> bottomAnswerTexts;

    // Keys for PlayerPrefs
    const string KEY_SURAH_NUMBER = "SURAH_NUMBER";
    const string KEY_AYAH_NUMBER = "AYAH_NUMBER";
    const string KEY_TOTAL_QUESTIONS = "TOTAL_QUESTIONS";
    const string KEY_HIGHSCORE = "HIGHSCORE_";
    const string KEY_GRADE = "GRADE_";
    const string KEY_COINS = "COINS";
    const string KEY_EXPERIENCE = "EXPERIENCE";
    const string KEY_S_STREAK = "S_STREAK";
    // achievement
    const string KEY_ACH_TOTAL_COINS = "ACH_TOTAL_COIN";
    const string KEY_ACH_S_STREAK = "ACH_S_STREAK";
    // mission
    const string KEY_MISSION_COIN = "MISSION_COIN";
    const string KEY_MISSION_SCORE_S = "MISSION_SCORE_S";
    const string KEY_MISSION_LEVEL = "MISSION_LEVEL";
    const string KEY_MISSION_COMBO = "MISSION_COMBO";
    const string KEY_MISSION_XP = "MISSION_XP";


    private void Awake() {
        // Get the surah and ayah data from the player prefs
        currentSurah = PlayerPrefs.GetInt(KEY_SURAH_NUMBER, 999);
        currentAyah = PlayerPrefs.GetInt(KEY_AYAH_NUMBER, 999);
        totalQuestions = PlayerPrefs.GetInt(KEY_TOTAL_QUESTIONS, 10);

        // Set the initial score and progress
        scoreText.text = score.ToString();
        progressText.text = "1/" + totalQuestions;
        streakProgressBar.fillAmount = 0;
        streakText.text = "0";
        
        audioButton2 = audioQuestion2.GetComponent<Button>();

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
        for (int i = 0; i < answerTexts1.Count; i++) { // type 1 and 2
            int index = i;
            answerTexts1[i].GetComponentInParent<Button>().onClick.AddListener(() => OnAnswerButtonClicked(index + 1));
            answerTexts2[i].GetComponentInParent<Button>().onClick.AddListener(() => OnAnswerButtonClicked(index + 1));
        }

        for (int i = 0; i < topAnswerTexts.Count; i++) { // type 3
            int index = i;
            topAnswerTexts[i].GetComponentInParent<Button>().onClick.AddListener(() => OnType3_AnswerButtonClicked(index, true));
            bottomAnswerTexts[i].GetComponentInParent<Button>().onClick.AddListener(() => OnType3_AnswerButtonClicked(index, false));
        }
        
        audioButton2.onClick.AddListener(PlayAudioQuestion2); // audio button for type 2
        submitButton.onClick.AddListener(OnSubmitButtonClicked); // submit button
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

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame() {
        yield return new WaitForSeconds(3);

        questionType1.SetActive(false);
        questionType2.SetActive(false);
        questionType3.SetActive(false);

        yield return new WaitForSeconds(1);

        GetQuestion();

        yield return new WaitForSeconds(.5f);

        timer = timerDuration;
        timerBar.fillAmount = 1;
        loadingPanel.SetActive(false);
    }

    void GetQuestion() {
        // Randomly select a question type
        int tempType = 0;
        while (tempType == selectedQuestionType || tempType == 0) {
            tempType = Random.Range(1, 4);
        }
        selectedQuestionType = tempType;

        // selectedQuestionType = 3;

        switch (selectedQuestionType) {
            case 1:
                GetQuestionType1();
                break;
            case 2:
                GetQuestionType2();
                break;
            case 3:
                GetQuestionType3();
                break;
        }

        timer = timerDuration;
        timerBar.fillAmount = 1;
    }

    void GetQuestionType1() {
        selectedQuestionType = 1;

        int questionBasedOnAyah = currentAyah * 3;

        if (questionBasedOnAyah > questionDatabase.questionsType_1[currentSurah].Count - 1) {
            questionBasedOnAyah = questionDatabase.questionsType_1[currentSurah].Count - 1;
        }
        // Randomly select a question from the database
        var currentLevel = questionDatabase.questionsType_1[currentSurah];
        var currentQuestion = currentLevel[Random.Range(0, questionBasedOnAyah)];
        // var currentQuestion = questionDatabase.questions[currentSurah, Random.Range(0, questionBasedOnAyah)];

        // Display question text
        mainQuestionText.text = currentQuestion.mainQuestionText;
        secondaryQuestionText.GetComponent<ArabicFixer>().fixedText = currentQuestion.secondaryQuestionText;
        
        // Get the correct answer
        var currentOptions = questionDatabase.optionsType_1[currentSurah];
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

    void GetQuestionType2() {
        selectedQuestionType = 2;

        int questionBasedOnAyah = currentAyah * 2;
        if (questionBasedOnAyah > questionDatabase.questionsType_2[currentSurah].Count - 1) {
            questionBasedOnAyah = questionDatabase.questionsType_2[currentSurah].Count - 1;
        }
        // Randomly select a question from the database
        var currentLevel = questionDatabase.questionsType_2[currentSurah];
        var currentQuestion = currentLevel[Random.Range(0, questionBasedOnAyah)];
        // var currentQuestion = questionDatabase.questions[currentSurah, Random.Range(0, questionBasedOnAyah)];

        // Display question text
        mainQuestionText2.text = currentQuestion.mainQuestionText;
        audioQuestion2.clip = Resources.Load<AudioClip>(currentQuestion.secondaryQuestionText);
        
        // Get the correct answer
        var currentOptions = questionDatabase.optionsType_2[currentSurah];
        multipleChoiceAnswer = currentOptions[currentQuestion.correctAnswerIndex];

        // Randomize the options
        List<string> randomizedOptions = new List<string>(currentOptions);
        randomizedOptions = randomizedOptions.OrderBy(x => UnityEngine.Random.value).ToList(); // Use OrderBy with a random value to shuffle the list
        
        // Display options
        bool isAnswerSet = false;
        for (int i = 0; i < answerTexts2.Count; i++) {
            answerTexts2[i].GetComponent<ArabicFixer>().fixedText = randomizedOptions[i];
            if (!isAnswerSet && randomizedOptions[i] == multipleChoiceAnswer) {
                isAnswerSet = true;
            }
        };

        // Randomly place the correct 
        if (!isAnswerSet) {
            int randomAnswerIndex = Random.Range(0, 4);
            answerTexts2[randomAnswerIndex].GetComponent<ArabicFixer>().fixedText = multipleChoiceAnswer;
        }

        questionType2.SetActive(true);
        Debug.Log("Correct answer: " + multipleChoiceAnswer);
    }

    void GetQuestionType3() {
        selectedQuestionType = 3;

        var currentLevel = questionDatabase.questionsType_3[currentSurah];

        // Display question text
        secondaryQuestionText3.text = questionDatabase.surahName[currentSurah];

        // Randomize and take 4 top options
        Dictionary<int, string> currentQuestion = new Dictionary<int, string>();
        currentQuestion = currentLevel.OrderBy(x => UnityEngine.Random.value).Take(4).ToDictionary(pair => pair.Key, pair => pair.Value);

        // Display top options
        for (int i = 0; i < topAnswerTexts.Count; i++) {
            topAnswerTexts[i].GetComponent<ArabicFixer>().fixedText = currentQuestion.ElementAt(i).Value;
            topAnswerTexts[i].GetComponent<ArabicFixer>().tempSaveData = currentQuestion.ElementAt(i).Key.ToString();
        };

        // Get 4 bottom options based on the top options
        Dictionary<int, string> currentOptions = new Dictionary<int, string>();
        foreach (var item in currentQuestion) {
            currentOptions.Add(item.Key, questionDatabase.optionsType_3[currentSurah].ElementAt(item.Key));
        }

        // Randomize bottom options
        currentOptions = currentOptions.OrderBy(x => UnityEngine.Random.value).ToDictionary(pair => pair.Key, pair => pair.Value);

        // Display bottom options
        for (int i = 0; i < bottomAnswerTexts.Count; i++) {
            bottomAnswerTexts[i].GetComponent<ArabicFixer>().fixedText = currentOptions.ElementAt(i).Value;
            bottomAnswerTexts[i].GetComponent<ArabicFixer>().tempSaveData = currentOptions.ElementAt(i).Key.ToString();
        };

        questionType3.SetActive(true);
    }

    public void OnAnswerButtonClicked(int answerIndex) {
        switch (selectedQuestionType) {
            case 1:
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
                break;
            case 2:
                // Store the clicked answer
                clickedMultipleChoiceAnswer = answerTexts2[answerIndex - 1].text;

                // Reset the color of all answers
                for (int i = 0; i < answerTexts2.Count; i++) {
                    answerTexts2[i].GetComponentInParent<Image>().color = new Color32(255, 255, 255, 255);
                    answerTexts2[i].color = new Color32(51, 51, 51, 255);
                }

                // Change the color of the clicked answer
                answerTexts2[answerIndex - 1].GetComponentInParent<Image>().color = new Color32(0, 169, 171, 255);
                answerTexts2[answerIndex - 1].color = new Color32(255, 255, 255, 255);
                break;
        }

        submitButton.interactable = true;
    }

    public void OnType3_AnswerButtonClicked(int answerIndex, bool isTop)
    {
        if (isTop) {
            if (topMatchingAnswer != null) { // Reset the color of the previous top 
                foreach (var text in topAnswerTexts) {
                    if (text.GetComponent<ArabicFixer>().tempSaveData == topMatchingAnswer) {
                        ResetMatchingAnswerColor(text, true);
                        break;
                    }
                }
            }

            topMatchingAnswer = topAnswerTexts[answerIndex].GetComponent<ArabicFixer>().tempSaveData;
            ChangeMatchingAnswerColor(answerIndex, true);
        } else {
            if (bottomMatchingAnswer != null) { // Reset the color of the previous bottom answer
                foreach (var text in bottomAnswerTexts) {
                    if (text.GetComponent<ArabicFixer>().tempSaveData == bottomMatchingAnswer) {
                        ResetMatchingAnswerColor(text, false);
                        break;
                    }
                }
            }

            bottomMatchingAnswer = bottomAnswerTexts[answerIndex].GetComponent<ArabicFixer>().tempSaveData;
            ChangeMatchingAnswerColor(answerIndex, false);
        }

        if (topMatchingAnswer != null && bottomMatchingAnswer != null) {
            if (topMatchingAnswer == bottomMatchingAnswer) {
                // Change the color of the correct answers
                StartCoroutine(CorrectMatchingAnswerColor());

                matchCounter++;

                if (matchCounter >= 4) {
                    submitButton.interactable = true;
                }
            } else {
                // Reset the color of the previous answers
                StartCoroutine(WrongMatchingAnswerColor());

                if (!isWrongAnswer) isWrongAnswer = true;
            }

            topMatchingAnswer = null;
            bottomMatchingAnswer = null;
        }
    }

    void ChangeMatchingAnswerColor(int answerIndex, bool isTop) {
        if (isTop) {
            topAnswerTexts[answerIndex].GetComponentInParent<Image>().color = new Color32(0, 169, 171, 255);
            topAnswerTexts[answerIndex].color = new Color32(0, 169, 171, 255);
        } else {
            bottomAnswerTexts[answerIndex].GetComponentInParent<Image>().color = new Color32(0, 169, 171, 255);
            bottomAnswerTexts[answerIndex].color = new Color32(255, 255, 255, 255);
        }
    }

    void ResetMatchingAnswerColor(Text thatText, bool isTop) {
        if (isTop) {
            thatText.GetComponentInParent<Image>().color = new Color32(255, 255, 255, 255);
            thatText.color = new Color32(255, 255, 255, 255);
                   
        } else {
            thatText.GetComponentInParent<Image>().color = new Color32(255, 255, 255, 255);
            thatText.color = new Color32(51, 51, 51, 255);
                    
        }
    }

    IEnumerator CorrectMatchingAnswerColor() {
        Text topText = null;
        Text bottomText = null;

        // Find the correct answers
        foreach (var text in topAnswerTexts) {
            if (text.GetComponent<ArabicFixer>().tempSaveData == topMatchingAnswer) {
                topText = text;
                break;
            }
        }

        foreach (var text in bottomAnswerTexts) {
            if (text.GetComponent<ArabicFixer>().tempSaveData == bottomMatchingAnswer) {
                bottomText = text;
                break;
            }
        }

        // Disable the buttons
        topText.GetComponentInParent<Button>().interactable = false;
        bottomText.GetComponentInParent<Button>().interactable = false;

        // Change the color of the correct answers
        topText.GetComponentInParent<Image>().color = new Color32(100, 193, 126, 255);
        topText.color = new Color32(100, 193, 126, 255);
        bottomText.GetComponentInParent<Image>().color = new Color32(100, 193, 126, 255);
        bottomText.color = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        // Change the color to the color of disabled button
        topText.GetComponentInParent<Image>().color = new Color32(185, 185, 185, 255);
        topText.color = new Color32(185, 185, 185, 255);
        bottomText.GetComponentInParent<Image>().color = new Color32(185, 185, 185, 255);
        bottomText.color = new Color32(255, 255, 255, 255);
    }

    IEnumerator WrongMatchingAnswerColor() {
        Text topText = null;
        Text bottomText = null;

        // Find the previous answers
        foreach (var text in topAnswerTexts) {
            if (text.GetComponent<ArabicFixer>().tempSaveData == topMatchingAnswer) {
                topText = text;
                break;
            }
        }
        foreach (var text in bottomAnswerTexts) {
            if (text.GetComponent<ArabicFixer>().tempSaveData == bottomMatchingAnswer) {
                bottomText = text;
                break;
            }
        }

        // Disable the buttons
        topText.GetComponentInParent<Button>().interactable = false;
        bottomText.GetComponentInParent<Button>().interactable = false;

        // Change the color of the wrong answers
        topText.GetComponentInParent<Image>().color = new Color32(171, 0, 0, 255);
        topText.color = new Color32(171, 0, 0, 255);
        bottomText.GetComponentInParent<Image>().color = new Color32(171, 0, 0, 255);
        bottomText.color = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        // Reset the color of the previous answers
        ResetMatchingAnswerColor(topText, true);
        ResetMatchingAnswerColor(bottomText, false);

        // Enable the buttons
        topText.GetComponentInParent<Button>().interactable = true;
        bottomText.GetComponentInParent<Button>().interactable = true;
    }

    public void OnSubmitButtonClicked() {
        StartCoroutine(OnSubmitButtonClicked_Coroutine());
    }

    IEnumerator OnSubmitButtonClicked_Coroutine() {
        submitButton.interactable = false;
        ColorBlock colors = submitButton.colors;

        // Check if the clicked answer is the correct answer
        if (
            ((selectedQuestionType == 1 || selectedQuestionType == 2) && clickedMultipleChoiceAnswer == multipleChoiceAnswer) // type 1 and 2
            || (selectedQuestionType == 3 && !isWrongAnswer) // type 3
            ) {
            colors.disabledColor = new Color32(100, 193, 126, 255); // Correct answer color
            totalRightAnswers++;

            // Add streak
            streakText.text = (int.Parse(streakText.text) + 1).ToString();
            streakProgressBar.fillAmount = float.Parse(streakText.text) / totalQuestions;
            if (int.Parse(streakText.text) > maxStreak) maxStreak = int.Parse(streakText.text);

            // Add score
            score += (100 + (int)Mathf.Round(timer)) * int.Parse(streakText.text);
            scoreText.text = score.ToString();
         
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
        switch (selectedQuestionType) {
            case 1:
                for (int i = 0; i < answerTexts1.Count; i++) {
                    answerTexts1[i].GetComponentInParent<Image>().color = new Color32(255, 255, 255, 255);
                    answerTexts1[i].color = new Color32(51, 51, 51, 255);
                }
                questionType1.SetActive(false);
                break;
            case 2:
                for (int i = 0; i < answerTexts2.Count; i++) {
                    answerTexts2[i].GetComponentInParent<Image>().color = new Color32(255, 255, 255, 255);
                    answerTexts2[i].color = new Color32(51, 51, 51, 255);
                }
                questionType2.SetActive(false);
                break;
            case 3:
                for (int i = 0; i < topAnswerTexts.Count; i++) {
                    ResetMatchingAnswerColor(topAnswerTexts[i], true);
                    ResetMatchingAnswerColor(bottomAnswerTexts[i], false);

                    topAnswerTexts[i].GetComponentInParent<Button>().interactable = true;
                    bottomAnswerTexts[i].GetComponentInParent<Button>().interactable = true;
                }
                questionType3.SetActive(false);
                break;
        }

        // Reset state
        clickedMultipleChoiceAnswer = "";
        multipleChoiceAnswer = "";

        topMatchingAnswer = null;
        bottomMatchingAnswer = null;
        matchCounter = 0;
        isWrongAnswer = false;
                
        // Update the question number
        currentQuestionNumber++;
        progressText.text = currentQuestionNumber + "/" + totalQuestions;
        
        // Get the next question
        if (currentQuestionNumber > totalQuestions) {
            SetUpFinishPanel();
        } else {
            GetQuestion();
        }
    }

    void SetUpFinishPanel() {
        // Getting keys for high score and grade
        string highScoreKey = KEY_HIGHSCORE + currentSurah + "_" + currentAyah;
        string gradeKey = KEY_GRADE + currentSurah + "_" + currentAyah;

        // Calculate the accuracy
        int accuracy = (int)((float)totalRightAnswers / totalQuestions * 100);
        
        // temporary variables
        int tempCoins = 0;
        int tempXP = 0;

        // Set the grade
        grade_S.SetActive(false);
        if (accuracy > 99) {
            grade_S.SetActive(true);
            tempCoins = 30;
            tempXP = 50;

            PlayerPrefs.SetInt(KEY_MISSION_SCORE_S, PlayerPrefs.GetInt(KEY_MISSION_SCORE_S, 0) + 1); // daily mission score S
            PlayerPrefs.SetInt(KEY_S_STREAK, PlayerPrefs.GetInt(KEY_S_STREAK, 0) + 1); // s streak
            if (PlayerPrefs.GetInt(KEY_S_STREAK, 0) > PlayerPrefs.GetInt(KEY_ACH_S_STREAK, 0)) { // achievement s streak
                PlayerPrefs.SetInt(KEY_ACH_S_STREAK, PlayerPrefs.GetInt(KEY_S_STREAK));
            }
        } else if (accuracy > 80) {
            grade_A.SetActive(true);
            tempCoins = 20;
            tempXP = 40;
        } else if (accuracy > 60) {
            grade_B.SetActive(true);
            tempCoins = 15;
            tempXP = 30;
        } else {
            grade_C.SetActive(true);
            tempCoins = 10;
            tempXP = 20;
        }

        // Set the finish panel texts
        finishScoreText.text = score.ToString();
        highScoreText.text = PlayerPrefs.GetInt(highScoreKey, 0).ToString();
        maxComboText.text = maxStreak.ToString();
        accuracyText.text = accuracy + "%";
        coinsEarnedText.text = tempCoins.ToString();
        xpEarnedText.text = tempXP.ToString();

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

        // Add the variables to the player prefs
        PlayerPrefs.SetInt(KEY_COINS, PlayerPrefs.GetInt(KEY_COINS, 0) + tempCoins); // coins
        PlayerPrefs.SetInt(KEY_ACH_TOTAL_COINS, PlayerPrefs.GetInt(KEY_ACH_TOTAL_COINS, 0) + tempCoins); // achievement total coins
        PlayerPrefs.SetInt(KEY_MISSION_COIN, PlayerPrefs.GetInt(KEY_MISSION_COIN, 0) + tempCoins); // daily mission coin

        PlayerPrefs.SetInt(KEY_EXPERIENCE, PlayerPrefs.GetInt(KEY_EXPERIENCE, 0) + tempXP); // experience
        PlayerPrefs.SetInt(KEY_MISSION_XP, PlayerPrefs.GetInt(KEY_MISSION_XP, 0) + tempXP); // daily mission xp

        PlayerPrefs.SetInt(KEY_MISSION_LEVEL, PlayerPrefs.GetInt(KEY_MISSION_LEVEL, 0) + 1); // daily mission level
        if (maxStreak >= 5) PlayerPrefs.SetInt(KEY_MISSION_COMBO, PlayerPrefs.GetInt(KEY_MISSION_COMBO, 0) + 1); // daily mission combo
    }

    public void PlayAudioQuestion2() {
        if (audioQuestion2.clip != null) {
            Image audioIcon = audioButton2.GetComponent<Image>();
            if (audioQuestion2.isPlaying) {
                audioQuestion2.Stop();
                audioIcon.sprite = notPlayingAudioIcon;
            } else {
                audioQuestion2.Play();
                audioIcon.sprite = playingAudioIcon;
            }
        } else {
            Debug.Log("Audio clip is null");
        }
    }

    private void Update() {
        // Check if the audio has finished playing
        if (selectedQuestionType == 2) {
            if (!audioQuestion2.isPlaying && audioButton2.GetComponent<Image>().sprite == playingAudioIcon) {
                audioButton2.GetComponent<Image>().sprite = notPlayingAudioIcon;
            }
        }

        // Update the timer
        if (timer > 0) {
            timer -= Time.deltaTime;
            timerBar.fillAmount = timer / timerDuration;
        }
    }
}
