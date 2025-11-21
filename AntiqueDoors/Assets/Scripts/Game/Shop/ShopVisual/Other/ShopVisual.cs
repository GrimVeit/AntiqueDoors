using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopVisual : MonoBehaviour
{
    [SerializeField] private int idLevel;
    [SerializeField] private int price;
    [SerializeField] private Button buttonBuy;

    [SerializeField] private GameObject objectReceived;
    [SerializeField] private GameObject objectPrice;

    public void Initialize()
    {
        buttonBuy.onClick.AddListener(Buy);
    }

    public void Dispose()
    {
        buttonBuy.onClick.RemoveListener(Buy);
    }

    public void SetReceived()
    {
        transform.gameObject.SetActive(true);
        objectReceived.SetActive(true);
        objectPrice.SetActive(false);
        buttonBuy.enabled = false;
    }

    public void SetAvailabled()
    {
        transform.gameObject.SetActive(true);
        objectReceived.SetActive(false);
        objectPrice.SetActive(true);
        buttonBuy.enabled = true;
    }

    public void SetLocked()
    {
        transform.gameObject.SetActive(false);
    }

    #region Output

    public event Action<int, int> OnBuy;

    private void Buy()
    {
        OnBuy?.Invoke(idLevel, price);
    }

    #endregion
}
