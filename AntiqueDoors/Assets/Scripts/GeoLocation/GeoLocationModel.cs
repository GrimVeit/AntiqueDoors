using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class GeoLocationModel
{
    public event Action OnErrorGetCountry;
    public event Action<string> OnGetCountry;

    private string URL_GET_IP = "https://ipinfo.io/json";

    public void GetUserCountry()
    {
        Coroutines.Start(GetIPInfo_Coroutine());
    }

    private IEnumerator GetIPInfo_Coroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL_GET_IP))
        {
            request.timeout = 4; // Таймаут в секундах

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var jsonResult = request.downloadHandler.text;
                IPInfo ipInfo = JsonUtility.FromJson<IPInfo>(jsonResult);

                OnGetCountry?.Invoke(ipInfo.country);
            }
            else
            {
                // При желании можно посмотреть причину ошибки
                // Debug.LogError($"GetIPInfo failed: {request.result}, {request.error}");

                OnErrorGetCountry?.Invoke();
            }
        }
    }
}

public class IPInfo
{
    public string ip;
    public string city;
    public string region;
    public string country;
    public string loc;
    public string org;
    public string postal;
    public string timezone;
    public string readme;
}
