using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserGrid : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textNickname;
    [SerializeField] private TextMeshProUGUI textRecord;

    [SerializeField] private List<GameObject> tops = new();

    public void SetData(string nickname, int record, int count)
    {
        textNickname.text = nickname;
        textRecord.text = record.ToString();

        switch (count)
        {
            case 0:
                tops.ForEach(data => data.SetActive(false));
                break;
            case 1:
                tops.ForEach(data => data.SetActive(false));
                tops[0].SetActive(true);
                break;
            case 2:
                tops.ForEach(data => data.SetActive(false));
                tops[0].SetActive(true);
                tops[1].SetActive(true);
                break;
            case 3:
                tops.ForEach(data => data.SetActive(false));
                tops[0].SetActive(true);
                tops[1].SetActive(true);
                tops[2].SetActive(true);
                break;
            default:
                tops.ForEach(data => data.SetActive(false));
                break;
        }
    }
}
