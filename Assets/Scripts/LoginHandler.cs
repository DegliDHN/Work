using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    public UniWebView login_UniWebView;
	//public GameObject profileTabGO;


	public bool IsUserLoggedIn(){
		bool tokenExists = PlayerPrefs.HasKey(AppDB.Instance.nameKey);
		return tokenExists;
	}

    public void ShowLoginPage(Action onLoginSuccessful, string url = "")
    {
		if(url.IsNullOrEmpty()){ url = login_UniWebView.urlOnStart; }

        login_UniWebView.OnMessageReceived += OnLoginSuccessful;
        login_UniWebView.OnMessageReceived += (_, __) => onLoginSuccessful();

        this.Log($"[Login] Loading Login Page => {url}");

		// login_UniWebView.GetComponent<WebViewSpinner>().EnableSpinnerOnce_ThenDisable();
		
		login_UniWebView.Load(url);
		login_UniWebView.gameObject.AddOrGetComponent<ShowHideWebView>().Show();
    }

	public void HideLoginPage(){
		login_UniWebView.gameObject.SetActive(false);
		login_UniWebView.GetComponent<ShowHideWebView>().Hide();
	}

    private void OnLoginSuccessful(UniWebView webView, UniWebViewMessage msg)
    {
        this.Log("[Login] Raw Message: "+msg.RawMessage);
        this.Log("[Login] "+string.Join(", ", msg.Args.Select(pair => $"{pair.Key} => {pair.Value}")));

        SaveUserInfo_ToAppDB(msg.Args["user_id"], msg.Args["name"], msg.Args["surname"], msg.Args["email"], msg.Args["token"], msg.Args["qualifica"]?.ToLower());
    }

    public void SaveUserInfo_ToAppDB(string user_id, string name, string surname, string email, string authenticationToken, string qualifica){
        AppDB dbInstance = AppDB.Instance;
		dbInstance.SaveUser_Id(user_id);
        dbInstance.SaveUser_Name(name);
        dbInstance.SaveUser_Surname(surname);
        dbInstance.SaveUser_Email(email);
        dbInstance.SaveUser_AuthToken(authenticationToken);
		dbInstance.SaveUser_Qualifica(qualifica);	
        PlayerPrefs.Save();
        this.Log($"[Login] Saved User Info: \n UserID = {user_id}\n Name = {name}\n Surname = {surname}\n Email = {email}\n Authentication Token = {authenticationToken} \nQualifica = {qualifica}");
    }

    public void Logout(){
        Debug.Log("Logout - Start");
		//TODO: Delete all playerprefs???
		PlayerPrefs.DeleteKey(AppDB.Instance.nameKey);
        PlayerPrefs.DeleteKey(AppDB.Instance.surnameKey);
        PlayerPrefs.DeleteKey(AppDB.Instance.emailKey);
        PlayerPrefs.DeleteKey(AppDB.Instance.qualificaKey);
        PlayerPrefs.DeleteKey(AppDB.Instance.authenticationTokenKey);
        PlayerPrefs.DeleteKey(AppDB.Instance.user_idKey);
		PlayerPrefs.Save();
        // File.Delete(AppDB.Instance.GetUser_QrCodePath());

        // profile_UniWebView.Hide();
		//profileTabGO?.SetActive(false);
		UniWebView.ClearCookies();	//TODO: change to SetCookies()
		login_UniWebView.CleanCache();	//TODO: unsure we need it, but just in case
		
        Debug.Log("Logout - End");

		Application.Quit();
        // Login_AppManager.Instance.ChangeStateToLogin();
    }

}
