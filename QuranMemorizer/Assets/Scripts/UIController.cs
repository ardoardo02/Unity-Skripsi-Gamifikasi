using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void PlaySFX(string name)
    {
        AudioManager.instance.PlaySFX(name);
    }
}
