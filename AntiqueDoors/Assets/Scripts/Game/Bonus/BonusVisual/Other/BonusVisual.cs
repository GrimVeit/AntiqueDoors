using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusVisual : MonoBehaviour
{
    public BonusType BonusType => bonusType;

    [SerializeField] private BonusType bonusType;
    [SerializeField] private TextMeshProUGUI textCount;

    public void SetBonusCount(int count)
    {
        textCount.text = count.ToString();
    }
}
