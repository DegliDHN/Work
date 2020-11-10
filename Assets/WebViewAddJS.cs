using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebViewAddJS : MonoBehaviour
{
	private UniWebView myUniwebview;
	private ShowHideWebView showHideWebview;

	[TextArea]
	public string javascript_code;

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

		string custom_javascript = javascript_code;
        
		myUniwebview.EvaluateJavaScript(custom_javascript, (payload) => {
			if (payload.resultCode.Equals("0")) {
				print(payload.data);
			}
		});
    }
}
