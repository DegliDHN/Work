using UnityEngine;
using UnityEngine.UI;

public class WebviewTab : MonoBehaviour
{
	public UniWebView webView;
	public Button openBtn;
	public Button backBtn;
	private ShowHideWebView showHideWebview;
	
    void Start()
    {
		showHideWebview = webView.GetComponent<ShowHideWebView>();
		backBtn.gameObject.SetActive(false);
		
		openBtn.onClick.AddListener(ShowWebview);
    }

	public void ShowWebview(){
		webView.Reload();
		showHideWebview.Show();
		backBtn.gameObject.SetActive(true);
		backBtn.onClick.AddListener(HideWebview);
	}

	public void HideWebview(){
		showHideWebview.Hide();
		backBtn.gameObject.SetActive(false);
		backBtn.onClick.RemoveListener(HideWebview);
	}
}
