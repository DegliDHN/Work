using UnityEngine;

public class WebViewOpenLinksExternal : MonoBehaviour
{
    public bool openLinksInExternal;

    void Awake()
    {
		if(openLinksInExternal){
			UniWebView uniWebView = GetComponent<UniWebView>();
			uniWebView.OnPageFinished += (view, statusCode, url) => {
				Debug.Log("Opening links in external browser : True");
				uniWebView.SetOpenLinksInExternalBrowser(true);
			};
			uniWebView.OnPageStarted += (_,__) => {
				Debug.Log("Opening links in external browser : False");
				uniWebView.SetOpenLinksInExternalBrowser(false);
			};
		}
    }
}
