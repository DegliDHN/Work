using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public enum FooterStates{
	Home,
	HomeWebview,
	Biglietto, 
	Mappa, 
	Profilo, 
	PrivacyTab, 
	AR
}

public class Fiera_Scandicci : AFiera365
{
    private StateMachine<FooterStates> stateMachine;


	private void Start()
	{
		if(stateMachine == null){
			stateMachine = StateMachine<FooterStates>.Initialize(this);
		}

		foreach (var home_webview_btn in homePage.HomePageButtons)
		{
			home_webview_btn.onClick.AddListener(() => stateMachine.ChangeState(FooterStates.HomeWebview));
		}
	}

	private void Home_Enter(){
		Debug.Log("[Scandicci] Home Enter");

        homePage.ShowHomePage();
		homePage.backButton.onClick.AddListener(homePage.HandleBackPressed);
		// homePage.ActivateAreaStampa(AppDB.Instance.GetUser_Qualifica() == "giornalista");
        
    }

	private void Home_Exit(){
		Debug.Log("[Scandicci] Home Exit");

		homePage.backButton.onClick.RemoveListener(homePage.HandleBackPressed);
        homePage.HideHomePage();
    }

	private void HomeWebview_Enter(){
		Debug.Log("[Scandicci] Homeview Enter");
		
        homePage.ShowHomePage();
        homePage.ShowBackButton();
		homePage.backButton.onClick.AddListener( homePage.HandleBackPressed );
        foreach(var homeWebview in homePage.homeWebviewsArray){
            homeWebview.GetComponent<ShowHideWebView>().onUniWebviewHidden += ChangeStateToHome;
        }
    }

    private void HomeWebview_Exit(){
		Debug.Log("[Scandicci] Homeview Exit");

        foreach(var homeWebview in homePage.homeWebviewsArray){
            homeWebview.GetComponent<ShowHideWebView>().onUniWebviewHidden -= ChangeStateToHome;
        }
		homePage.backButton.onClick.RemoveListener( homePage.HandleBackPressed );
        homePage.HideBackButton();
        homePage.HideHomeWebviews();
    }

	public void ChangeStateToHome(UniWebView webView = null){
        stateMachine.ChangeState(FooterStates.Home);
    }

	public override void Activate()
	{
		if(stateMachine == null){
			stateMachine = StateMachine<FooterStates>.Initialize(this);
		}

		stateMachine.ChangeState(FooterStates.Home);
	}
}
