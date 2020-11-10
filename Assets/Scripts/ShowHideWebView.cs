using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Used to call Show and Hide functions via inspector buttons, while defining all optional parameters
/// </summary>
public class ShowHideWebView : MonoBehaviour
{
    public bool fade = false;
    public UniWebViewTransitionEdge transitionEdge;
    public float transitionDuration = 0.4f;
    public Action<UniWebView> onUniWebviewShown = (_) =>{}, onUniWebviewHidden = (_) =>{};

    private UniWebView myWebview;

    void Awake(){
        this.myWebview = this.GetComponent<UniWebView>();
    }

    public void Show(){
        // bool result = this.myWebview.Show(fade, transitionEdge, transitionDuration, UniWebviewShownEvent);
        bool result = this.myWebview.Show(fade, transitionEdge, transitionDuration);
        StartCoroutine(OnShowComplete_Coro());
        Debug.Log("Showing Webview: " + result);
    }

    private IEnumerator OnShowComplete_Coro(){
        yield return new WaitForSeconds(transitionDuration);
        yield return new WaitForSeconds(transitionDuration/10); //Wait 10% more time, to be sure
        onUniWebviewShown(myWebview);
    }

    // private void UniWebviewShownEvent(){
    //     Debug.Log("UniWebView Shown");
    //     onUniWebviewShown(myWebview);
    // }

    public void Hide(){
        this.myWebview.Hide(fade, transitionEdge, transitionDuration, UniWebviewHideEvent);
		if(this.myWebview.Url != this.myWebview.urlOnStart){
			//Don't show loading spinner while reloading, then set it back to how it was before
			this.myWebview.SetShowSpinnerWhileLoading(false);
			bool showSpinner = this.GetComponent<WebViewSpinner>()?.enableLoadingSpinner ?? true;
			UniWebView.PageFinishedDelegate resetSpinnerSetting = null;
			resetSpinnerSetting = (_, __, ___) => {
				this.myWebview.SetShowSpinnerWhileLoading(showSpinner);
				this.myWebview.OnPageFinished -= resetSpinnerSetting;
			};
			this.myWebview.OnPageFinished += resetSpinnerSetting;
			this.myWebview.Load(this.myWebview.urlOnStart);
			//--------------
		}
    }

    private void UniWebviewHideEvent(){
        onUniWebviewHidden(myWebview);
        Debug.Log("UniWebView Hidden");
    }
}
