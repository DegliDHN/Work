using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HomePage : MonoBehaviour
{
	public UniWebView[] homeWebviewsArray;

	private Button[] _homePageButtons;
	public Button[] HomePageButtons { get => _homePageButtons; private set => _homePageButtons = value; }

	public GameObject homePageGO;
	public Button backButton;
	public GameObject areaStampaGo;

	[HideInInspector]
	public UniWebView currentlyActive = null;

	void Awake()
	{
		backButton.gameObject.SetActive(false);
		homeWebviewsArray = homeWebviewsArray.Where(wv => wv.gameObject.activeSelf).ToArray();
		HomePageButtons = this.GetComponentsInChildren<Button>(true);
	}

	void Start()
	{
		var showHideWebviews = homeWebviewsArray.Select(elem => elem.GetComponent<ShowHideWebView>());

		showHideWebviews.ForEach(arg => arg.onUniWebviewShown += OnWebviewShown);
		showHideWebviews.ForEach(arg => arg.onUniWebviewHidden += OnWebviewHidden);
		Debug.Log("ShowHide webview listeners connected");
	}

	private void OnWebviewShown(UniWebView webview)
	{
		Debug.Log("Webview Shown");
		currentlyActive = webview;
		backButton.gameObject.SetActive(true);
	}

	private void OnWebviewHidden(UniWebView webview)
	{
		Debug.Log("Webview Hidden");
		currentlyActive = null;
		backButton.gameObject.SetActive(false);
	}

	public void ShowHomePage()
	{
		this.gameObject.SetActive(true);
		homePageGO.SetActive(true);
	}

	public void HideHomePage()
	{
		homePageGO.SetActive(false);
	}

	public void HandleBackPressed()
	{
		if (currentlyActive.CanGoBack && currentlyActive.urlOnStart != currentlyActive.Url)
		{
			currentlyActive.GoBack();
		}
		else
		{
			currentlyActive.GetComponent<ShowHideWebView>().Hide();
		}
	}

	public void HideHomeWebviews()
	{
		homeWebviewsArray.ForEach(wv => wv.GetComponent<ShowHideWebView>().Hide());
	}

	public void ShowBackButton()
	{
		backButton.gameObject.SetActive(true);
	}

	public void HideBackButton()
	{
		backButton.gameObject.SetActive(false);
	}

	public void ReloadWebviews()
	{
		homeWebviewsArray.ForEach(wv => wv.Reload());
		Debug.Log("Webviews Reloaded");
	}

	// public void ActivateAreaStampa(bool activate)
	// {
	// 	Debug.Log("Area stampa set to " + activate);
	// 	areaStampaGo.SetActive(activate);
	// }
}
