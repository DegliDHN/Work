using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFooterAndHeaderJavascript : MonoBehaviour
{
	private UniWebView myUniwebview;
	private ShowHideWebView showHideWebview;

	void Awake(){
		myUniwebview = GetComponent<UniWebView>();
		showHideWebview = GetComponent<ShowHideWebView>();
		Debug.Log("MeHere");
		myUniwebview.OnPageFinished += (_, __, ___) => RunJS();
	}

    void Start()
    {
    }

    // Update is called once per frame
    void RunJS()
    {
		Debug.Log("Running JS");

		string removeHeader = "var list = document.getElementsByClassName(\"uk-navbar-container\");for (const elem of list){elem.style.display = \"none\";} ";
        
		string removeCookiesAcceptance = "var list = document.getElementsByClassName(\"cpnb-warningBox-show-fade-in cpnb-outer cpnb-div-position-bottom\");for (const elem of list){  elem.style.display = \"none\";}";

		string removeFooter = "var list = document.getElementsByClassName(\"uk-section-secondary\");for (const elem of list){  elem.style.display = \"none\";}";

		myUniwebview.EvaluateJavaScript(removeHeader, (payload) => {
			if (payload.resultCode.Equals("0")) {
				print(payload.data);
			}
		});

		myUniwebview.EvaluateJavaScript(removeCookiesAcceptance, (payload) => {
			if (payload.resultCode.Equals("0")) {
				print(payload.data);
			}
		});

		myUniwebview.EvaluateJavaScript(removeFooter, (payload) => {
			if (payload.resultCode.Equals("0")) {
				print(payload.data);
			}
		});
    }
}
