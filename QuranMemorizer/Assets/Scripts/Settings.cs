using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [Header("Sliders")]
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    [Header("Texts")]
    [SerializeField] TMP_Text masterVolumeText;
    [SerializeField] TMP_Text musicVolumeText;
    [SerializeField] TMP_Text sfxVolumeText;

    [Header("Buttons")]
    [SerializeField] Button applyButton;
    [SerializeField] Button cancelButton;
    [SerializeField] Button backButton;

    private void Awake() {
        applyButton.onClick.AddListener(ApplySettings);
        cancelButton.onClick.AddListener(CancelSettings);
        backButton.onClick.AddListener(CancelSettings);
    }

    private void Start() {
        if (PlayerPrefs.HasKey("MasterVolume")) {
            LoadSettings();
        } else {
            PlayerPrefs.SetFloat("MasterVolume", 1);
            PlayerPrefs.SetFloat("MusicVolume", 1);
            PlayerPrefs.SetFloat("SFXVolume", 1);
            LoadSettings();
        }
        
        gameObject.SetActive(false);
    }

    public void SetMasterVolume() {
        float volume = masterVolumeSlider.value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        masterVolumeText.text = (int)(volume * 100) + "%";
    }

    public void SetMusicVolume() {
        float volume = musicVolumeSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        musicVolumeText.text = (int)(volume * 100) + "%";
    }

    public void SetSFXVolume() {
        float volume = sfxVolumeSlider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        sfxVolumeText.text = (int)(volume * 100) + "%";
    }

    void LoadSettings() {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        masterVolumeText.text = (int)(masterVolumeSlider.value * 100) + "%";
        musicVolumeText.text = (int)(musicVolumeSlider.value * 100) + "%";
        sfxVolumeText.text = (int)(sfxVolumeSlider.value * 100) + "%";
    }

    public void ApplySettings() {
        AudioManager.instance.PlaySFX("ClickOpen");
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }

    public void CancelSettings() {
        AudioManager.instance.PlaySFX("ClickOpen");
        LoadSettings();
    }
}
