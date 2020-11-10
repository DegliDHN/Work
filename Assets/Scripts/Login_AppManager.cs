using System.Collections;
using UnityEngine;
using MonsterLove.StateMachine;
using System;
using NaughtyAttributes;
using System.Linq;
using OranUnityUtils;

public enum LoginStates
{
	ShowLoginPage,
	SelezionaQualifica,
	UserLoggedIn
}

[RequireComponent(typeof(LoginHandler))]
[RequireComponent(typeof(AppDB))]
public class Login_AppManager : Singleton<Login_AppManager> 
{
	//TODO: move to Addressables manager. 
	// public GameObject loadingOverlayCanvas;
	//TODO
	public bool enableLogin;
	[ShowIf("enableLogin")]
	public LoginHandler loginPage;
	public bool enableSelezionaQualifica;
	[ShowIf("enableSelezionaQualifica")]
	public SelezionaQualificaUtente selezionaQualifica;
	//-----------------
	public Action onUserLoggedIn;


    private StateMachine<LoginStates> loginStateMachine;
	
	protected override void InitTon(){
		// UniWebView.ClearCookies();	
		#if UNITY_EDITOR
		enableLogin = false;
		#endif
    }

    protected IEnumerator Start(){
		#if UNITY_EDITOR
		enableLogin = false;
		#endif
		
		Screen.fullScreen = false;

        loginStateMachine = StateMachine<LoginStates>.Initialize(this);

		yield return null;

		if(enableLogin){
			Debug.Log("[Login] Login Enabled");
        	loginStateMachine.ChangeState(LoginStates.ShowLoginPage);
		} else {
			Debug.Log("[Login] Login Disabled - Skipping Login");
			loginStateMachine.ChangeState(LoginStates.UserLoggedIn);
		}

    }

	public void ChangeStateToLogin(){
        loginStateMachine.ChangeState(LoginStates.ShowLoginPage);
    }

	private void ShowLoginPage_Enter(){
		this.Log("[Login] ShowLoginPage_Enter");

		if(loginPage.IsUserLoggedIn()==false){
			loginPage.ShowLoginPage(AfterLogin);
		} else {
			//User already logged in
			loginStateMachine.ChangeState(LoginStates.UserLoggedIn);
		}
	}

	private void AfterLogin(){
		if(enableSelezionaQualifica && AppDB.Instance.GetUser_Qualifica().IsNullOrEmpty()){ 
			Debug.Log("[Login] No Qualifica found");
			loginStateMachine.ChangeState(LoginStates.SelezionaQualifica);
		} else {
			loginStateMachine.ChangeState(LoginStates.UserLoggedIn);
		}
	}

	private void ShowLoginPage_Exit(){
		this.Log("[Login] ShowLoginPage_Exit");
		loginPage.HideLoginPage();
	}

	private void SelezionaQualifica_Enter(){
		Debug.Log("[Login] SelezionaQualifica Enter");
		selezionaQualifica.gameObject.SetActive(true);
		selezionaQualifica.ShowSelectQualificaUI(_ => loginStateMachine.ChangeState(LoginStates.UserLoggedIn));
	}

	private void SelezionaQualifica_Exit(){
		Debug.Log("[Login] SelezionaQualifica Exit");
		selezionaQualifica.HideSelectQualificaUI();
	}

	private void UserLoggedIn_Enter(){
		Debug.Log("[Login] UserLoggedIn Enter (Coro)");
		Debug.Log($"[Login] User info: Id={AppDB.Instance.GetUser_ID()}, Name={AppDB.Instance.GetUser_Name()}, Surname={AppDB.Instance.GetUser_Surname()}, Email={AppDB.Instance.GetUser_Email()}");

		//Load Addressables
		// AddressablesLoadersManager.Instance.LoadAddressables(() => onUserLoggedIn?.Invoke());
		onUserLoggedIn?.Invoke();
	}
}
