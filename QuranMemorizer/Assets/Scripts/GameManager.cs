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

    int score;
    int selectedQuestionType;

    string multipleChoiceAnswer;
    string clickedMultipleChoiceAnswer;

    [SerializeField] QuestionDatabase questionDatabase;
    [SerializeField] Button submitButton;
    [SerializeField] TMP_Text scoreText;

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
        currentSurah = PlayerPrefs.GetInt("SurahNumber", 999);
        currentAyah = PlayerPrefs.GetInt("AyahNumber", 999);

        score = 0;
        scoreText.text = score.ToString();

        answerTexts1.Add(answerText1_1);
        answerTexts1.Add(answerText1_2);
        answerTexts1.Add(answerText1_3);
        answerTexts1.Add(answerText1_4);

        answerTexts2.Add(answerText2_1);
        answerTexts2.Add(answerText2_2);
        answerTexts2.Add(answerText2_3);
        answerTexts2.Add(answerText2_4);

        // Add listener to the answer buttons
        submitButton.onClick.AddListener(OnSubmitButtonClicked);

        for (int i = 0; i < answerTexts1.Count; i++) {
            int index = i;
            answerTexts1[i].GetComponentInParent<Button>().onClick.AddListener(() => OnAnswerButtonClicked(index + 1));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (currentSurah == 999 || currentAyah == 999) {
            Debug.Log("No surah and ayah data found");
        }
        
        Debug.Log("Current Surah: " + currentSurah);
        Debug.Log("Current Ayah: " + currentAyah);

        GetQuestionType1();
    }

    void GetQuestionType1() {
        selectedQuestionType = 1;

        int questionBasedOnAyah = currentAyah * 2;
        if (questionBasedOnAyah > questionDatabase.questions.Count) {
            questionBasedOnAyah = questionDatabase.questions.Count;
        }
        // Randomly select a question from the database
        QuestionDatabase.Question currentQuestion = questionDatabase.questions[Random.Range(0, questionBasedOnAyah)];

        // Display question text
        mainQuestionText.text = currentQuestion.mainQuestionText;
        secondaryQuestionText.GetComponent<ArabicFixer>().fixedText = currentQuestion.secondaryQuestionText;
        
        // Get the correct answer
        multipleChoiceAnswer = questionDatabase.options[currentQuestion.correctAnswerIndex];

        // Randomize the options
        List<string> randomizedOptions = new List<string>(questionDatabase.options);
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
            if (randomAnswerIndex == 0) {
                answerText1_1.GetComponent<ArabicFixer>().fixedText = multipleChoiceAnswer;
            } else if (randomAnswerIndex == 1) {
                answerText1_2.GetComponent<ArabicFixer>().fixedText = multipleChoiceAnswer;
            } else if (randomAnswerIndex == 2) {
                answerText1_3.GetComponent<ArabicFixer>().fixedText = multipleChoiceAnswer;
            } else if (randomAnswerIndex == 3) {
                answerText1_4.GetComponent<ArabicFixer>().fixedText = multipleChoiceAnswer;
            }
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
            score += 100;
            scoreText.text = score.ToString();
        } else {
            colors.disabledColor = new Color32(171, 0, 0, 255); // Wrong answer color
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

        clickedMultipleChoiceAnswer = "";
        multipleChoiceAnswer = "";
        selectedQuestionType = 0;
        questionType1.SetActive(false);

        GetQuestionType1();
    }
}
