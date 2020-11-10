using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System;

[System.Serializable]
public enum Qualifica{
	Visitatore, 
	Espositore, 
	Professionista,
	Giornalista
}
public class SelezionaQualificaUtente : MonoBehaviour
{
	public Button visitatoreBtn, espositoreBtn, professionistaBtn, giornalistaBtn, giornalistaInfoConfermaBtn;
	public GameObject pannelloSelezioneGo, giornalistaInfoAddizionaliGo;
	public TMP_InputField testataField, cellulareField;
	private Action<Qualifica> onQualificaSelected;
	private string restRoute = "https://biglietteria.officinevalis.it/wp-json/qualifica-utente/v1/";

	private Button[] qualificheBtns => new Button[]{visitatoreBtn, espositoreBtn, professionistaBtn, giornalistaBtn, giornalistaInfoConfermaBtn};

	public void Awake(){
		pannelloSelezioneGo.SetActive(false);
		giornalistaInfoAddizionaliGo.SetActive(false);

		visitatoreBtn.onClick.AddListener( ()=> SelezionaQualifica(Qualifica.Visitatore));
		espositoreBtn.onClick.AddListener( ()=> SelezionaQualifica(Qualifica.Espositore));
		professionistaBtn.onClick.AddListener( ()=> SelezionaQualifica(Qualifica.Professionista));

		giornalistaBtn.onClick.AddListener( ()=> giornalistaInfoAddizionaliGo.SetActive(true));
		giornalistaBtn.onClick.AddListener( ()=> pannelloSelezioneGo.SetActive(false));
		giornalistaInfoConfermaBtn.onClick.AddListener( ()=> OnGiornalistaAdditionalInfoConferma() );
	}

	[ContextMenu("Foo")]
	public void ShowSelectQualificaUI(Action<Qualifica> onQualificaSelected){
		Debug.Log("Show Seleziona qualifica");
		this.onQualificaSelected = onQualificaSelected;
		pannelloSelezioneGo.SetActive(true);
		giornalistaInfoAddizionaliGo.SetActive(false);
	}

	public void HideSelectQualificaUI(){
		pannelloSelezioneGo.SetActive(false);
		giornalistaInfoAddizionaliGo.SetActive(false);
		this.gameObject.SetActive(false);
	}

	private void OnGiornalistaAdditionalInfoConferma()
    {
		StartCoroutine(OnGiornalistaAdditionalInfoConferma_Coro());
	}

    private IEnumerator OnGiornalistaAdditionalInfoConferma_Coro()
    {
		string user_id = AppDB.Instance.GetUser_ID();
        string testata = testataField.text;
		string cellulare = cellulareField.text;
		
		// cellulare = cellulare.Replace('-', '\0');
		// cellulare = cellulare.Replace(' ', '\0');
		
		// if(cellulare[0] != '+' && Char.IsDigit(cellulare[0]) == false ){
		// 	Debug.LogError("Invalid first character of cellphone number");
		// } 
		// long tmp;
		// if(long.TryParse(cellulare.Skip(1), out tmp) == false){
		// 	Debug.LogError("Invalid character in cellphone number");
		// }

		string routeWithParameters = restRoute + "add_info_giornalista" + $"?user_id={user_id}&testata={testata}&cellulare={cellulare}";
		
		Debug.Log("WebRequest => "+routeWithParameters);

		UnityWebRequest request = UnityWebRequest.Get(routeWithParameters);

		yield return request;

		Debug.Log("Answer: "+request.downloadHandler.text);

		SelezionaQualifica(Qualifica.Giornalista);		
    }

    private void SelezionaQualifica(Qualifica qualifica){
		qualificheBtns.ForEach( b => b.interactable = false );

		StartCoroutine(SelezionaQualifica_Coro(qualifica));
	}

	private IEnumerator SelezionaQualifica_Coro(Qualifica qualifica){
		string qualificaUser = qualifica.ToString().ToLower();
		string user_id = AppDB.Instance.GetUser_ID();
		string routeWithParameters = restRoute + "update_qualifica_utente" + $"?user_id={user_id}&qualifica={qualificaUser}";
		UnityWebRequest request = UnityWebRequest.Get(routeWithParameters);
		
		yield return request.SendWebRequest();

		Debug.Log("WebRequest => "+routeWithParameters);
		Debug.Log("Answer: "+request.downloadHandler.text);

		AppDB.Instance.SaveUser_Qualifica(qualificaUser);
		Debug.Log($"Saving user qualifica => {qualificaUser}");

		onQualificaSelected(qualifica);
	}

	void Update(){
		if(giornalistaInfoAddizionaliGo.activeInHierarchy){
			giornalistaInfoConfermaBtn.interactable = testataField.text?.Length >= 3;
		}
	}
}