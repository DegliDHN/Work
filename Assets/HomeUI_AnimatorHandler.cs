using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUI_AnimatorHandler : MonoBehaviour
{
    private Animator animator;
	private bool isUIShown = false;

	public bool IsUIShown { get => isUIShown; set => isUIShown = value; }

	void Awake()
    {
        this.animator = this.GetComponent<Animator>();
    }

    public void ShowUI_StartAnim(){
        animator.SetTrigger("Close");
		IsUIShown = true;
    }

    public void HideUI_StartAnim(){
		animator.SetTrigger("Open");
		IsUIShown = false;
    }


}
