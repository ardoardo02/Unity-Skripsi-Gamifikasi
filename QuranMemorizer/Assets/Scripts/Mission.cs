using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    [SerializeField] TMP_Text taskText;
    [SerializeField] TMP_Text progressText;
    [SerializeField] TMP_Text rewardText;
    [SerializeField] Image progressBar;
    [SerializeField] Button claimButton;
    string key;

    public string Key { get => key; }
    public Button ClaimButton { get => claimButton; }

    public void SetMissionData(string key, string missionText, int progress, int goal, int reward, bool isClaimed)
    {
        this.key = key;
        taskText.text = missionText;
        progressText.text = progress + "/" + goal;
        rewardText.text = reward.ToString();

        progressBar.fillAmount = (float)progress / goal;

        if (!isClaimed && progress >= goal) return;

        if (isClaimed) {
            ClaimReward();
        } else {
            claimButton.gameObject.SetActive(false);
        }
    }

    public void ClaimReward() {
        claimButton.interactable = false;
        claimButton.GetComponentInChildren<TMP_Text>().gameObject.SetActive(false);
    }

    public void ReSetMission(int progress, int goal) {
        progressText.text = progress + "/" + goal;
        progressBar.fillAmount = (float)progress / goal;
        
        claimButton.gameObject.SetActive(false);

    }
}
