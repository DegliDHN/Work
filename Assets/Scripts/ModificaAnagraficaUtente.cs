using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System;

public class ModificaAnagraficaUtente : MonoBehaviour
{
	public Button changeNameBtn, changeSurnameBtn, changeEmailBtn, changePasswordBtn;
	
	private string restRoute = "https://login.paper2app.it/wp-json/api/v1/";

	private Button[] btns => new Button[]{changeNameBtn, changeSurnameBtn, changeEmailBtn, changePasswordBtn};

	public void Awake(){
		
	}

	private void UpdateNameInWordpressDB(string newName)
    {
		StartCoroutine(UpdateAnagraphicInWordpressDB_Coro(name: newName));
	}

	private void UpdateSurnameInWordpressDB(string newSurname)
    {
		StartCoroutine(UpdateAnagraphicInWordpressDB_Coro(surname: newSurname));
	}

    private IEnumerator UpdateAnagraphicInWordpressDB_Coro(string name=null, string surname=null, string email=null, string password=null)
    {
		string user_id = AppDB.Instance.GetUser_ID();
        // string testata = testataField.text;
		// string cellulare = cellulareField.text;

		string routeWithParameters = restRoute + "update_user_name" + $"?user_id={user_id}";

		if(name.IsNullOrEmpty() == false){
			routeWithParameters = routeWithParameters + $"name={name}";
		}
		if(surname.IsNullOrEmpty() == false){
			routeWithParameters = routeWithParameters + $"surname={surname}";
		}
		if(email.IsNullOrEmpty() == false){
			routeWithParameters = routeWithParameters + $"email={email}";
		}
		if(password.IsNullOrEmpty() == false){
			routeWithParameters = routeWithParameters + $"password={password}";
		}
		
		Debug.Log("WebRequest => "+routeWithParameters);

		UnityWebRequest request = UnityWebRequest.Get(routeWithParameters);

		yield return request.SendWebRequest();

		Debug.Log("Answer: "+request.downloadHandler.text);
    }
}