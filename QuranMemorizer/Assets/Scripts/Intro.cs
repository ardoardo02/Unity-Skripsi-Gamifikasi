using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] bool ResetPlayerPrefs;
    [Header("Character Panel")]
    [SerializeField] GameObject CharacterPanel;
    [SerializeField] Button BoyButton;
    [SerializeField] Button GirlButton;

    [Header("Username Panel")]
    // [SerializeField] GameObject UsernamePanel;
    [SerializeField] GameObject BoyImg;
    [SerializeField] GameObject GirlImg;
    [SerializeField] TMP_InputField UsernameInput;
    [SerializeField] TMP_Text FirstParagraphStory;
    [SerializeField] TMP_Text SecondParagraphStory;
    [SerializeField] Button UsernameButton;

    string CharacterSelected;

    // Keys for PlayerPrefs
    const string KEY_CHARACTER = "CHARACTER";
    const string KEY_USERNAME = "USERNAME";

    // Start is called before the first frame update
    void Start()
    {
        if (ResetPlayerPrefs) PlayerPrefs.DeleteAll();
        StartCoroutine("Welcome");
    }

    IEnumerator Welcome()
    {
        yield return new WaitForSeconds(3);

        if (PlayerPrefs.HasKey(KEY_CHARACTER) && PlayerPrefs.HasKey(KEY_USERNAME)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        } else {
            CharacterPanel.SetActive(true);
        }

        // CharacterPanel.SetActive(true);
    }

    // update character
    public void OnCharacterSelected(string character)
    {
        CharacterSelected = character;

        // update boy button's normal color if boy is selected
        if (character == "boy") {
            ColorBlock cb = BoyButton.colors;
            cb.normalColor = new Color32(74, 183, 184, 255);
            BoyButton.colors = cb;

            cb = GirlButton.colors;
            cb.normalColor = new Color32(80, 101, 108, 255);
            GirlButton.colors = cb;

            BoyImg.SetActive(true);
            GirlImg.SetActive(false);
        } else {
            ColorBlock cb = GirlButton.colors;
            cb.normalColor = new Color32(74, 183, 184, 255);
            GirlButton.colors = cb;

            cb = BoyButton.colors;
            cb.normalColor = new Color32(80, 101, 108, 255);
            BoyButton.colors = cb;

            GirlImg.SetActive(true);
            BoyImg.SetActive(false);
        }
    }

    // update value when input field is edited
    public void OnUsernameValueEdit()
    {
        string username = UsernameInput.text;
        
        if (username.Length == 0) username = "(nama kamu)";

        FirstParagraphStory.text = "Ada seorang anak <b>laki-laki</b> kecil yang tinggal di desa kecil di Indonesia, namanya <b><color=#00AAAB>" + username + "</color></b>. <b><color=#00AAAB>" + username + "</color></b> suka banget dengan lantunan <b>Al-Quran</b> dan mempunyai mimpi untuk tampil di kompetisi lantunan <b>Al-Quran</b> setiap tahunnya di desanya. Kompetisinya tuh keren banget dan banyak banget orang dari desa dan kota-kota tetangga yang datang nonton.";
        SecondParagraphStory.text = "Tapi tentu saja, nggak gampang banget buat  <b><color=#00AAAB>" + username + "</color></b> buat siap-siap tampil di sana. Dia harus hafal <b>Al-Quran</b> sampai mampu mengesankan. Hanya dalam beberapa bulan,  <b><color=#00AAAB>" + username + "</color></b> memulai rutinitasnya untuk menghafal ayat-ayat <b>Al-Quran</b> setiap hari.";

        if (UsernameInput.text.Length >= 4 && UsernameInput.text.Length <= 12){
            UsernameButton.interactable = true;
        } else {
            UsernameButton.interactable = false;
        }
    }

    public void OnSubmit()
    {
        PlayerPrefs.SetString(KEY_CHARACTER, CharacterSelected);
        PlayerPrefs.SetString(KEY_USERNAME, UsernameInput.text);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
