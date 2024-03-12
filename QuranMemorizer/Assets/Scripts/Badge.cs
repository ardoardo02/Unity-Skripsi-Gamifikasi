using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Badge : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;
    [SerializeField] TMP_Text reward;
    [SerializeField] TMP_Text progress;
    [SerializeField] Image progressBar;
    [SerializeField] Button claimButton;

    public void SetBadgeData(string icon, Color32 iconColor, string title, string description, string reward, int progress, int goal, bool isClaimed)
    {
        this.icon.sprite = Resources.Load<Sprite>(icon);
        this.icon.color = iconColor;
        this.title.text = title;
        this.description.text = description;
        this.reward.text = reward;
        this.progress.text = progress + "/" + goal;
        progressBar.fillAmount = (float)progress / goal;
        claimButton.gameObject.SetActive(progress >= goal && !isClaimed);
    }
}
