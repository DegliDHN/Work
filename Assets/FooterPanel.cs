using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FooterPanel : MonoBehaviour
{
	public Button bioBtn, linkedinBtn, emailBtn, phoneBtn;
	// public GameObject phoneBtn_Separator;

	public Button[] ArButtons => new Button[]{bioBtn, linkedinBtn, emailBtn, phoneBtn};

	public Outline bioBtn_outline;
	public Image bioBtn_image;
	private bool isBioPanelActive = false;

	void Awake(){
		bioBtn_outline = bioBtn.GetComponent<Outline>();
		bioBtn_image = bioBtn.GetComponent<Image>();
	}
	

}
