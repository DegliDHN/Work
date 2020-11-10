using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UniWebView;

public class WebviewErrorHandler : MonoBehaviour
{
    public PageErrorReceivedDelegate onPageError;

    void Start()
    {
        GetComponent<UniWebView>().OnPageErrorReceived += onPageError;
    }

    // // Update is called once per frame
    // void HandlePageError(UniWebView webView, int errorCode, string message)
    // {
    //     onPageError(webView, errorCode, message);
    // }
}
