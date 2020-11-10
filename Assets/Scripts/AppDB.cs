using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppDB : Singleton<AppDB>
{
	public string nameKey = "name", surnameKey = "surname", emailKey = "email", authenticationTokenKey = "authenticationToken", qualificaKey = "qualifica", user_idKey = "user_id";

    public string QrCodeFullSavePath => Application.persistentDataPath + "/QRCodeTexture.jpg";

	public string GetUser_ID(){
		if(PlayerPrefs.HasKey(user_idKey)){
			return PlayerPrefs.GetString(user_idKey);
		}else {
			return null;
		}
	}

	public string GetUser_Name(){
		if(PlayerPrefs.HasKey(nameKey)){
			return PlayerPrefs.GetString(nameKey);
		}else {
			return null;
		}
	}

	public string GetUser_Surname(){
		if(PlayerPrefs.HasKey(surnameKey)){
			return PlayerPrefs.GetString(surnameKey);
		}else {
			return null;
		}
	}

	public string GetUser_Email(){
		if(PlayerPrefs.HasKey(emailKey)){
			return PlayerPrefs.GetString(emailKey);
		}else {
			return null;
		}
	}

	public string GetUser_AuthToken(){
		if(PlayerPrefs.HasKey(authenticationTokenKey)){
			return PlayerPrefs.GetString(authenticationTokenKey);
		}else {
			return null;
		}
	}

	public string GetUser_QrCodePath(){
		return QrCodeFullSavePath;
	}

	public string GetUser_Qualifica(){
		if(PlayerPrefs.HasKey(qualificaKey)){
			return PlayerPrefs.GetString(qualificaKey);
		}else {
			return null;
		}
	}

	public void SaveUser_Name(string name){
		PlayerPrefs.SetString(nameKey, name);
		PlayerPrefs.Save();
	}

	public void SaveUser_Surname(string surname){
		PlayerPrefs.SetString(surnameKey, surname);
		PlayerPrefs.Save();
	}

	public void SaveUser_Email(string email){
		PlayerPrefs.SetString(emailKey, email);
		PlayerPrefs.Save();
	}

	public void SaveUser_AuthToken(string authenticationToken){
		PlayerPrefs.SetString(authenticationTokenKey, authenticationToken);
		PlayerPrefs.Save();
	}

	protected override void InitTon()
	{
		
	}

	public void SaveUser_Qualifica(string qualifica){
		PlayerPrefs.SetString(qualificaKey, qualifica);
		PlayerPrefs.Save();
	}

    public void SaveUser_Id(string user_id)
    {
        PlayerPrefs.SetString(user_idKey, user_id);
		PlayerPrefs.Save();
    }
}
