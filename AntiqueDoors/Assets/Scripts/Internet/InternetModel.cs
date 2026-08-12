using System;
using System.Collections;
using UnityEngine;

public class InternetModel
{
    public event Action<string> OnGetStatusDescription;
    public event Action OnInternetAvailable;
    public event Action OnInternetUnvailable;

    public void StartCheckConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Debug.Log("Internet disable");
            OnInternetUnvailable?.Invoke();
            OnGetStatusDescription?.Invoke("Unable to connect. Please check your internet connection");
        }
        else
        {
            //Debug.Log("Internet enable");
            OnInternetAvailable?.Invoke();
        }
    }

    public void Dispose()
    {

    }
}
