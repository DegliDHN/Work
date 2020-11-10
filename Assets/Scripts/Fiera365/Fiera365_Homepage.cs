using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fiera365_Homepage : MonoBehaviour
{
   	// public Button scandicciBtn, terranuovaBtn;
	public Button[] fiereBtns;
	public GameObject homePageGO;
	
	private void Awake() {
		homePageGO.gameObject.SetActive(false);
	}
	
	void Start(){
		
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
}
