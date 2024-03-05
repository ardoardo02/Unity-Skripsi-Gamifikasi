using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] TMP_Text surahNumberText;
    [SerializeField] TMP_Text surahNameText;
    [SerializeField] TMP_Text idnNameText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Image iconImage;
    [SerializeField] Image lockImage;

    Button button;
    Color surahNumberColor;
    Color surahNameColor;
    Color idnNameColor;
    Color iconColor;

    private void Start() {
        button = GetComponent<Button>();
        // button.onClick.AddListener(OnClick);

        surahNumberColor = surahNumberText.color;
        surahNameColor = surahNameText.color;
        idnNameColor = idnNameText.color;
        iconColor = iconImage.color;
    }

    public void SetLevelData(int surahNumber, string surahName, string idnName, string gradeScore, int price)
    {
        surahNumberText.text = surahNumber.ToString();
        surahNameText.text = surahName;
        idnNameText.text = idnName;

        if (price > 0) {
            scoreText.text = "";
            lockImage.gameObject.SetActive(true);
        } else {
            scoreText.text = gradeScore;
        }
    }

    // change button color on click
    public void ChangeColor()
    {
        if (button.image.color == Color.white)
        {
            button.image.color = new Color32(0, 169, 171, 255);
            surahNumberText.color = Color.white;
            surahNameText.color = Color.white;
            idnNameText.color = Color.white;
            iconImage.color = Color.white;
        }
        else
        {
            button.image.color = Color.white;
            surahNumberText.color = surahNumberColor;
            surahNameText.color = surahNameColor;
            idnNameText.color = idnNameColor;
            iconImage.color = iconColor;
        }
    }
}
