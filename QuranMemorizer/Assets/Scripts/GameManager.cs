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

    List<int> chooseMatching_Answer = new List<int>();
    List<int> chooseMatching_PlayerAnswer = new List<int>();

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

    [Header("Question Type 4")]
    [SerializeField] GameObject questionType4;
    [SerializeField] List<GameObject> answerPlaceHolder;
    [SerializeField] List<Text> answerTexts4;

    [Header("Question Type 5")]
    [SerializeField] GameObject questionType5;
    [SerializeField] Text mainQuestionText5;
    [SerializeField] AudioSource audioQuestion5;
    [SerializeField] AudioSource audioPlayback5;
    [SerializeField] Button answerButton5;
    [SerializeField] Sprite micIcon;
    Button audioButton5;
    Button playbackButton5;

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
        audioButton5 = audioQuestion5.GetComponent<Button>();
        playbackButton5 = audioPlayback5.GetComponent<Button>();

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

        for (int i = 0; i < answerTexts4.Count; i++) { // type 4
            int index = i;
            answerTexts4[i].GetComponentInParent<Button>().onClick.AddListener(() => OnType4_answerButtonClicked(index));
            answerPlaceHolder[i].GetComponent<Button>().onClick.AddListener(() => OnType4_removeAnswerButtonClicked(index));
        }
        
        audioButton2.onClick.AddListener(() => PlayAudioQuestion("2")); // audio button for type 2
        audioButton5.onClick.AddListener(() => PlayAudioQuestion("5")); // audio button for type 5
        playbackButton5.onClick.AddListener(() => PlayAudioQuestion("5_playback")); // playback button for type 5
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
        questionType4.SetActive(false);
        questionType5.SetActive(false);

        foreach (var item in answerPlaceHolder) 
            item.SetActive(false);

        yield return new WaitForSeconds(1);

        GetQuestion();

        yield return new WaitForSeconds(.5f);

        timer = timerDuration;
        timerBar.fillAmount = 1;
        loadingPanel.SetActive(false);
        
        int randomAudio = Random.Range(1, 4);
        AudioManager.instance.PlayMusic("Gameplay" + randomAudio);
    }





    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ GET QUESTION ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void GetQuestion() {
        int tempType = 0;
        if (currentQuestionNumber < 5) {
            // Get the question type based on the current question number
            tempType = currentQuestionNumber;
        }
        else if (currentQuestionNumber == totalQuestions) {
            // Get the last question type
            tempType = 5;
        }
        else {
            // Randomly select a question type 1 to 4
            while (tempType == selectedQuestionType || tempType == 0) {
                tempType = Random.Range(1, 5);
            }
        }
        selectedQuestionType = tempType;

        // selectedQuestionType = 5;

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
            case 4:
                GetQuestionType4();
                break;
            case 5:
                GetQuestionType5();
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
            answerTexts2[i].GetComponent<ArabicFixer>().tempSaveData = randomizedOptions[i];
            if (!isAnswerSet && randomizedOptions[i] == multipleChoiceAnswer) {
                isAnswerSet = true;
            }
        };

        // Randomly place the correct 
        if (!isAnswerSet) {
            int randomAnswerIndex = Random.Range(0, 4);
            answerTexts2[randomAnswerIndex].GetComponent<ArabicFixer>().fixedText = multipleChoiceAnswer;
            answerTexts2[randomAnswerIndex].GetComponent<ArabicFixer>().tempSaveData = multipleChoiceAnswer;
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

    void GetQuestionType4() {
        selectedQuestionType = 4;

        int questionBasedOnAyah = currentAyah;
        // if (questionBasedOnAyah > questionDatabase.questionsType_4[currentSurah].Count - 1) {
        //     questionBasedOnAyah = questionDatabase.questionsType_4[currentSurah].Count - 1;
        // }

        // Randomly select a question from the database
        var currentLevel = questionDatabase.questionsType_4[currentSurah];
        var currentQuestion = currentLevel[Random.Range(0, questionBasedOnAyah)];

        foreach (int item in currentQuestion) {
            chooseMatching_Answer.Add(item);
        }
        chooseMatching_PlayerAnswer.Add(chooseMatching_Answer[0]);

        // Display question text
        GetChooseMatchPlaceholder();


        // Get options
        List<int> currentOptions = new List<int>();

        for (int i = 0; i < answerTexts4.Count; i++) { // Get the correct answers and fill the rest with random answers
            if (i != 0 && i < chooseMatching_Answer.Count) {
                currentOptions.Add(chooseMatching_Answer[i]);
            } else {
                while (true) {
                    int randomOption = Random.Range(0, questionDatabase.optionsType_4[currentSurah].Count);
                    if (!currentOptions.Contains(randomOption) && randomOption != chooseMatching_Answer[0]) {
                        currentOptions.Add(randomOption);
                        break;
                    }
                }
            }
        }

        currentOptions = currentOptions.OrderBy(x => UnityEngine.Random.value).ToList(); // Use OrderBy with a random value to shuffle the list

        for (int i = 0; i < answerTexts4.Count; i++) { // Display the options
            answerTexts4[i].GetComponent<ArabicFixer>().fixedText = questionDatabase.optionsType_4[currentSurah].ElementAt(currentOptions[i]);
            answerTexts4[i].GetComponent<ArabicFixer>().tempSaveData = currentOptions[i].ToString();
        }

        questionType4.SetActive(true);
    }

    void GetQuestionType5() {
        selectedQuestionType = 5;

        var currentLevel = questionDatabase.questionsType_5[currentSurah];
        var currentQuestion = currentLevel[currentAyah - 1];

        // Display question text
        mainQuestionText5.GetComponent<ArabicFixer>().fixedText = currentQuestion;

        // Set the audio clip
        var currentAudio = questionDatabase.optionsType_5[currentSurah];
        audioQuestion5.clip = Resources.Load<AudioClip>(currentAudio[currentAyah - 1]);

        questionType5.SetActive(true);
    }





    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ON BUTTON CLICKED ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~    
    public void OnAnswerButtonClicked(int answerIndex) // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ TYPE 1 AND 2 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    {
        AudioManager.instance.PlaySFX("Click");
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
                clickedMultipleChoiceAnswer = answerTexts2[answerIndex - 1].GetComponent<ArabicFixer>().tempSaveData;

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

    public void OnType3_AnswerButtonClicked(int answerIndex, bool isTop) // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ TYPE 3 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    {
        AudioManager.instance.PlaySFX("Click");
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

    public void OnType4_answerButtonClicked(int answerIndex) { // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ TYPE 4 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        AudioManager.instance.PlaySFX("Click");
        if (chooseMatching_PlayerAnswer.Count >= 8) return;

        chooseMatching_PlayerAnswer.Add(int.Parse(answerTexts4[answerIndex].GetComponent<ArabicFixer>().tempSaveData));
        answerTexts4[answerIndex].GetComponentInParent<Button>().interactable = false;

        GetChooseMatchPlaceholder();

        if (!submitButton.interactable) submitButton.interactable = true;
    }

    public void OnType4_removeAnswerButtonClicked(int answerIndex) {
        AudioManager.instance.PlaySFX("Click");
        if (chooseMatching_PlayerAnswer.Count < 1) return;

        int tempIndex = chooseMatching_PlayerAnswer[answerIndex];
        chooseMatching_PlayerAnswer.RemoveAt(answerIndex);
        
        for (int i = 0; i < answerTexts4.Count; i++) {
            if (answerTexts4[i].GetComponent<ArabicFixer>().tempSaveData == tempIndex.ToString()) {
                answerTexts4[i].GetComponentInParent<Button>().interactable = true;
                break;
            }
        }

        GetChooseMatchPlaceholder();

        if (chooseMatching_PlayerAnswer.Count == 1) submitButton.interactable = false;
    }

    public void OnSubmitButtonClicked() { // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ SUBMIT BUTTON ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        StartCoroutine(OnSubmitButtonClicked_Coroutine());
    }

    IEnumerator OnSubmitButtonClicked_Coroutine() {
        submitButton.interactable = false;
        ColorBlock colors = submitButton.colors;

        // Disable the button
        switch (selectedQuestionType) {
            case 1:
                for (int i = 0; i < answerTexts1.Count; i++) {
                    answerTexts1[i].GetComponentInParent<Button>().interactable = false;
                }
                break;
            case 2:
                for (int i = 0; i < answerTexts2.Count; i++) {
                    answerTexts2[i].GetComponentInParent<Button>().interactable = false;
                }
                break;
            case 4:
                for (int i = 0; i < answerTexts4.Count; i++) {
                    answerTexts4[i].GetComponentInParent<Button>().interactable = false;
                    answerPlaceHolder[i].GetComponent<Button>().interactable = false;
                }
                break;
            case 5:
                answerButton5.interactable = false;
                break;
        }

        // Check if the clicked answer is the correct answer
        if (
            ((selectedQuestionType == 1 || selectedQuestionType == 2) && clickedMultipleChoiceAnswer == multipleChoiceAnswer) // type 1 and 2
            || (selectedQuestionType == 3 && !isWrongAnswer) // type 3
            || (selectedQuestionType == 4 && chooseMatching_Answer.SequenceEqual(chooseMatching_PlayerAnswer)) // type 4
            || (selectedQuestionType == 5 && Random.Range(1, 4) > 1) // type 5
            ) {
            AudioManager.instance.PlaySFX("Correct");
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
            AudioManager.instance.PlaySFX("Wrong");
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

        // Reset the color of all answers and enable the buttons
        switch (selectedQuestionType) {
            case 1:
                for (int i = 0; i < answerTexts1.Count; i++) {
                    answerTexts1[i].GetComponentInParent<Image>().color = new Color32(255, 255, 255, 255);
                    answerTexts1[i].color = new Color32(51, 51, 51, 255);

                    answerTexts1[i].GetComponentInParent<Button>().interactable = true;
                }
                questionType1.SetActive(false);
                break;
            case 2:
                for (int i = 0; i < answerTexts2.Count; i++) {
                    answerTexts2[i].GetComponentInParent<Image>().color = new Color32(255, 255, 255, 255);
                    answerTexts2[i].color = new Color32(51, 51, 51, 255);

                    answerTexts2[i].GetComponentInParent<Button>().interactable = true;
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
            case 4:
                for (int i = 0; i < answerTexts4.Count; i++) {
                    answerTexts4[i].GetComponentInParent<Button>().interactable = true;
                    if (i != 0) answerPlaceHolder[i].GetComponent<Button>().interactable = true;
                }
                questionType4.SetActive(false);
                break;
            case 5:
                answerButton5.interactable = true;
                playbackButton5.interactable = false;
                questionType5.SetActive(false);
                break;
        }

        // Reset state
        clickedMultipleChoiceAnswer = "";
        multipleChoiceAnswer = "";

        topMatchingAnswer = null;
        bottomMatchingAnswer = null;
        matchCounter = 0;
        isWrongAnswer = false;

        chooseMatching_Answer.Clear();
        chooseMatching_PlayerAnswer.Clear();
                
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





    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ OTHERS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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

    public void PlayAudioQuestion(string audioName) {
        AudioSource audio = audioName == "2" ? audioQuestion2 : audioName == "5" ? audioQuestion5 : audioName == "5_playback" ? audioPlayback5 : null;
        
        if (audio.clip != null) {
            Image audioIcon = audio.GetComponent<Image>();
            if (audio.isPlaying) {
                audio.Stop();
                AudioManager.instance.VolumeDownMusic(false);
                audioIcon.sprite = notPlayingAudioIcon;
            } else {
                AudioManager.instance.VolumeDownMusic(true);
                audio.Play();
                audioIcon.sprite = playingAudioIcon;
            }
        } else {
            Debug.Log("Audio clip is null");
        }
    }

    public void RecordAudio5(bool isDown) {
        Image playButtonImage = answerButton5.GetComponent<Image>();
        AudioSource audio = playbackButton5.GetComponent<AudioSource>();

        if (isDown) {
            playButtonImage.sprite = playingAudioIcon;
            AudioManager.instance.VolumeDownMusic(true);
            audio.clip = Microphone.Start(null, false, 5, 44100);
        } else {
            playButtonImage.sprite = micIcon;
            Microphone.End(null);
            AudioManager.instance.VolumeDownMusic(false);
            // audio.clip = SavWav.TrimSilence(audio.clip, 0.01f);
            // audio.Play();

            if (!playbackButton5.interactable) playbackButton5.interactable = true;
            if (!submitButton.interactable) submitButton.interactable = true;
        }
    }

    void GetChooseMatchPlaceholder() {
        // Reset the placeholder
        foreach (var item in answerPlaceHolder) 
            item.SetActive(false);

        // Get options
        var currentOptions = questionDatabase.optionsType_4[currentSurah];

        // Display question text
        for (int i = 0; i < chooseMatching_PlayerAnswer.Count; i++) {
            ArabicFixer fixer = answerPlaceHolder[i].GetComponentInChildren<ArabicFixer>();

            fixer.fixedText = currentOptions.ElementAt(chooseMatching_PlayerAnswer[i]);
            fixer.tempSaveData = chooseMatching_PlayerAnswer[i].ToString();

            answerPlaceHolder[i].SetActive(true);
        }
    }

    private void Update() {
        // Check if the audio has finished playing
        if (selectedQuestionType == 2) {
            if (!audioQuestion2.isPlaying && audioButton2.GetComponent<Image>().sprite == playingAudioIcon) {
                audioButton2.GetComponent<Image>().sprite = notPlayingAudioIcon;
                AudioManager.instance.VolumeDownMusic(false);
            }
        } else if (selectedQuestionType == 5) {
            if (!audioQuestion5.isPlaying && audioButton5.GetComponent<Image>().sprite == playingAudioIcon) {
                audioButton5.GetComponent<Image>().sprite = notPlayingAudioIcon;
                AudioManager.instance.VolumeDownMusic(false);
            }
            if (!audioPlayback5.isPlaying && playbackButton5.GetComponent<Image>().sprite == playingAudioIcon) {
                playbackButton5.GetComponent<Image>().sprite = notPlayingAudioIcon;
                AudioManager.instance.VolumeDownMusic(false);
            }
        }

        // Update the timer
        if (timer > 0) {
            timer -= Time.deltaTime;
            timerBar.fillAmount = timer / timerDuration;
        }
    }
}
