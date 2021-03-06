using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelIndicator : MonoBehaviour
{
    [SerializeField]
    Image levelBar;
    [SerializeField]
    TextMeshProUGUI percentage;

    void Update()
    {
        levelBar.fillAmount = LevelManager.Instance.NormalizedStartedFillAmount();
        percentage.SetText($"%{(LevelManager.Instance.NormalizedStartedFillAmount() * 100f).ToString("f0")}");
    }
}