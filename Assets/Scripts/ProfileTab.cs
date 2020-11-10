using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ProfileTab : MonoBehaviour
{
    public TMP_Text user_name;
	public TMP_Text user_surname;
	public TMP_Text user_email;
	public TMP_Text user_qualifica;

	public void HideQualifica()
	{
		user_qualifica.gameObject.SetActive(false);
	}

	public void SetInfo_WithLabels(string name, string surname, string email, string qualifica=null)
	{
		// if(qualifica.IsNullOrEmpty() == false){
		// 	qualifica = "Sono un "+char.ToUpper(qualifica[0]) + qualifica.Substring(1);
		// }

		// this.SetInfo("Nome: Beta", "Cognome: Build", "E-mail: coming@soon.it", "Sono un: Utente Base");
		// this.SetInfo("Nome: "+name, "Cognome: "+surname, "E-mail: "+email, "");
		this.SetInfo(name, surname, email);
	}

	public void SetInfo(string name, string surname, string email, string qualifica=null)
	{
		this.user_name.text = name;
		this.user_surname.text = surname;
		this.user_email.text = email;

		if(qualifica.IsNullOrEmpty() == false){
			this.user_qualifica.text = char.ToUpper(qualifica[0]) + qualifica.Substring(1);
		} else {
			this.HideQualifica();
		}
	}
}
