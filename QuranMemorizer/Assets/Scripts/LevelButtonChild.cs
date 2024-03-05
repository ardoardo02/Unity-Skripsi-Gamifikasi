using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonChild : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text gradeScoreText;
    [SerializeField] Image lockImage;
    [SerializeField] Button button;
    
    Color titleColor;
    Color scoreColor;

    public TMP_Text TitleText { get => titleText; }
    public TMP_Text ScoreText { get => scoreText; }
    public TMP_Text GradeScoreText { get => gradeScoreText; }

    private void Start()
    {
        // button = GetComponent<Button>();
        // button.onClick.AddListener(OnClick);

        titleColor = TitleText.color;
        scoreColor = scoreText.color;
    }

    public void SetLevelData(int title, int score, string gradeScore, string prevGradeScore)
    {
        TitleText.text = "Ayat " + title;
        scoreText.text = score > 0 ? score.ToString() : "";

        if (prevGradeScore == "") {
            gradeScoreText.text = "";
            lockImage.gameObject.SetActive(true);
            button.interactable = false;
        } else {
            gradeScoreText.text = gradeScore;
        };
    }

    // change button color on click
    public void ChangeColor()
    {
        if (button.image.color == Color.white)
        {
            button.image.color = new Color32(0, 169, 171, 255);
            TitleText.color = Color.white;
            scoreText.color = Color.white;
        }
        else
        {
            button.image.color = Color.white;
            TitleText.color = titleColor;
            scoreText.color = scoreColor;
        }
    }
}
