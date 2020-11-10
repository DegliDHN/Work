using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public string link;
    
	public void Do()
    {
        Application.OpenURL(link);
    }



}
