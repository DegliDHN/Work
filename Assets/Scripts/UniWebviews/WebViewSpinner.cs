using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebViewSpinner : MonoBehaviour
{
    public bool enableLoadingSpinner;
    public string loadingText = "Loading...";
	UniWebView uniWebView;

    void Start()
    {
        uniWebView = GetComponent<UniWebView>();
        uniWebView.SetShowSpinnerWhileLoading(enableLoadingSpinner);
        uniWebView.SetSpinnerText(loadingText);
    }

	public void EnableSpinnerOnce_ThenDisable(){
		uniWebView.SetShowSpinnerWhileLoading(true);
        uniWebView.SetSpinnerText(loadingText);

		UniWebView.PageFinishedDelegate disableSpinnerDelegate = null;
		disableSpinnerDelegate = (webview, __, ___) => {
			uniWebView.SetShowSpinnerWhileLoading(false);
			uniWebView.OnPageFinished -= disableSpinnerDelegate;
		};

		uniWebView.OnPageFinished += disableSpinnerDelegate;
	}
}
