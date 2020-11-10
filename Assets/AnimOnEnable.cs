using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimOnEnable : MonoBehaviour
{
    void OnEnable(){
		this.GetComponent<Animator>().SetTrigger("Show");
	}
}
