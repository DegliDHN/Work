using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fiera365_Footer : MonoBehaviour
{
    public Button homeBtn, covidBtn, profiloBtn, leFiereBtn;
    public TMP_Text homeTxt, covidTxt, profiloTxt, leFiereTxt;

	private Animator myAnimator;

    public TMP_Text[] FooterTexts => new TMP_Text[]{
        homeTxt, covidTxt, profiloTxt, leFiereTxt
    };

	public void ShowFooterAnim(bool showFooter){
		//TODO: missing anim
		this.gameObject.SetActive(showFooter);
	}
	
}
