using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class InternetConnectionChecker : MonoBehaviour
{
    string urlToPing;
    int timeout;

    
    public void CheckInternetConnection(Action<bool> onResultCallback)
    {
        StartCoroutine(Ping_Coro(onResultCallback));
    }

    IEnumerator Ping_Coro(Action<bool> onResultCallback){
        // WWW pingRequest = new WWW(urlToPing);
        UnityWebRequest pingRequest = UnityWebRequest.Get(urlToPing);
        pingRequest.timeout = timeout;
        yield return pingRequest;
        
        if (pingRequest.isNetworkError || pingRequest.isHttpError){
            onResultCallback(false);
        } else {
            onResultCallback(true);
        }
    }
}
