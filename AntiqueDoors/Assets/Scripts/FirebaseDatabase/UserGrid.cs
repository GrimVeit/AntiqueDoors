using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserGrid : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textNumber;
    [SerializeField] private TextMeshProUGUI textNickname;
    [SerializeField] private TextMeshProUGUI textRecord;

    public void SetData(int number, string nickname, int record)
    {
        textNumber.text = number.ToString();
        textNickname.text = nickname;
        textRecord.text = record.ToString();
    }
}
