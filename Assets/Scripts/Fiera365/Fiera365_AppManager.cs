using System;
using System.Collections;
using System.Collections.Generic;
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Fiera365States
{
	Home, 
    Covid19,
	Profilo,
	PrivacyTab,
	LeFiere,
	Fiera
}

public class Fiera365_AppManager : MonoBehaviour
{
	// public Button scandicciBtn, terranuovaBtn;
	public Color defaultColor, accentColor;
	public GameObject circleBG;
	public Fiera365_Footer footer;
	public Fiera365_Homepage homepage;
	public ProfileTab profilePage;
	public GameObject covidPage;
	public WebviewTab privacyTab;
	public Button backButton;
	public AFiera365[] fiere;
	public UniWebView leFiereUniwebview;
	public ShowHideWebView leFiereUwv_ShowHide;
	private AFiera365 currFiera;

	private StateMachine<Fiera365States> stateMachine;

   	void Awake(){
		homepage.gameObject.SetActive(false);

		profilePage.gameObject.SetActive(false);
		covidPage.SetActive(false);
		fiere.ForEach(f => f.gameObject.SetActive(false));

		footer.ShowFooterAnim(false);

		Login_AppManager.Instance.onUserLoggedIn = OnUserLoggedIn;
		Debug.Log("Listener added to onUserLoggedIn");

        stateMachine = StateMachine<Fiera365States>.Initialize(this);

		footer.homeBtn.onClick.AddListener( () => stateMachine.ChangeState(Fiera365States.Home) );
		footer.covidBtn.onClick.AddListener( () => stateMachine.ChangeState(Fiera365States.Covid19) );
		footer.profiloBtn.onClick.AddListener( () => stateMachine.ChangeState(Fiera365States.Profilo) );
		footer.leFiereBtn.onClick.AddListener( () => stateMachine.ChangeState(Fiera365States.LeFiere) );

		privacyTab.openBtn.onClick.AddListener( () => stateMachine.ChangeState(Fiera365States.PrivacyTab) );

		for (int i = 0; i < fiere.Length; i++)
		{
			Button button = homepage.fiereBtns[i];
			AFiera365 fiera = fiere[i];
			button.onClick.AddListener( ()=> ChangeStateToFiera(fiera) );
			fiera.gameObject.SetActive(false);
		}
		
		
        footer.FooterTexts.ForEach(t => t.color = defaultColor);

		leFiereUwv_ShowHide = leFiereUniwebview.GetComponent<ShowHideWebView>();
   }

	private void ChangeStateToFiera(AFiera365 aFiera365)
	{
		currFiera = aFiera365;
		stateMachine.ChangeState(Fiera365States.Fiera);
	}

	protected void OnUserLoggedIn(){
		footer.ShowFooterAnim(true);
        stateMachine.ChangeState(Fiera365States.Home);
	}

	private void Home_Enter(){
		Debug.Log("Home_Enter");

		homepage.ShowHomePage();
		footer.homeBtn.interactable = false;
        footer.homeTxt.color = accentColor;
	}

	private void Home_Exit(){
		Debug.Log("Home_Exit");

		homepage.HideHomePage();
		footer.homeBtn.interactable = true;
        footer.homeTxt.color = defaultColor;
	}

	private void Covid19_Enter(){
		Debug.Log("Covid19_Enter");
		covidPage.SetActive(true);
		circleBG.SetActive(false);

		footer.covidBtn.interactable = false;
        footer.covidTxt.color = accentColor;
	}

	private void Covid19_Exit(){
		Debug.Log("Covid19_Exit");
		covidPage.SetActive(false);
		circleBG.SetActive(true);

		footer.covidBtn.interactable = true;
        footer.covidTxt.color = defaultColor;
	}

	private void Profilo_Enter(){
		Debug.Log("Profilo Enter");
		

		string userName = AppDB.Instance.GetUser_Name() + " " + AppDB.Instance.GetUser_Surname();
		
		profilePage.user_name.text = userName;
		profilePage.user_qualifica.text = char.ToUpper(AppDB.Instance.GetUser_Qualifica()[0]) + AppDB.Instance.GetUser_Qualifica().Substring(1);
		profilePage.user_email.text = AppDB.Instance.GetUser_Email();

		profilePage.gameObject.SetActive(true);

		footer.profiloBtn.interactable = false;
        footer.profiloTxt.color = accentColor;
	}

	private void Profilo_Exit(){
		Debug.Log("Profilo Exit");
		
		profilePage.gameObject.SetActive(false);

		footer.profiloBtn.interactable = true;
        footer.profiloTxt.color = defaultColor;
	}

	private void PrivacyTab_Enter(){
		this.Log("PrivacyTab Enter");

        privacyTab.ShowWebview();
		this.backButton.gameObject.SetActive(true);
		this.backButton.onClick.AddListener( GoBackToPreviousState );
    }
    
    private void PrivacyTab_Exit(){
		this.Log("PrivacyTab Exit");

		privacyTab.HideWebview();
		this.backButton.gameObject.SetActive(false);
		this.backButton.onClick.RemoveListener ( GoBackToPreviousState );
    }

	private void Fiera_Enter(){
		Debug.Assert(currFiera != null);
		
		currFiera.gameObject.SetActive(true);
		currFiera.Activate();
		this.gameObject.SetActive(false);
	}

	private void Fiera_Exit(){
		this.gameObject.SetActive(true);
		currFiera.homePage.HideHomeWebviews();
		currFiera.gameObject.SetActive(false);
		// currFiera = null;
	}

	private void LeFiere_Enter(){
		// leFiereUniwebview.gameObject.SetActive(true);
		leFiereUwv_ShowHide.Show();

		this.backButton.gameObject.SetActive(true);
		this.backButton.onClick.AddListener( HandleBackPressed_LeFiere );
		
		footer.leFiereBtn.interactable = false;
        footer.leFiereTxt.color = accentColor;
	}

	private void LeFiere_Exit(){
		leFiereUwv_ShowHide.Hide();

		this.backButton.gameObject.SetActive(false);
		this.backButton.onClick.RemoveListener( HandleBackPressed_LeFiere );		

		footer.leFiereBtn.interactable = true;
        footer.leFiereTxt.color = defaultColor;
	}

	private void GoBackToPreviousState(){
		this.Log("Previous state is "+stateMachine.LastState);
		stateMachine.ChangeState(stateMachine.LastState);
	}

	public void HandleBackPressed_LeFiere()
	{
		if (leFiereUniwebview.CanGoBack && leFiereUniwebview.urlOnStart != leFiereUniwebview.Url)
		{
			leFiereUniwebview.GoBack();
		}
		else
		{
			leFiereUwv_ShowHide.Hide();
			stateMachine.ChangeState(stateMachine.LastState);
		}
	}
   
}
