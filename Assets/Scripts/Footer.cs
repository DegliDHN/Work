using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Footer : MonoBehaviour
{
    public Button homeBtn, bigliettoBtn, mappaBtn, profiloBtn;
    public TMP_Text homeTxt, bigliettoTxt, mappaTxt, profiloTxt;

	private Animator myAnimator;

    public TMP_Text[] FooterTexts => new TMP_Text[]{
        homeTxt, bigliettoTxt, mappaTxt, profiloTxt
    };

	public void ShowFooterAnim(bool showFooter){
		//TODO: missing anim
		this.gameObject.SetActive(showFooter);
	}

	
}
