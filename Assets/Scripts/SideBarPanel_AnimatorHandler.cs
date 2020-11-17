using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarPanel_AnimatorHandler : MonoBehaviour
{
    private Animator animator;
	private bool isShown;
	public Action onShowAnim_Finish, onHideAnim_Finish;
    //public GameObject appLogo;
	public bool IsShown { get => isShown; set => isShown = value; }

	private bool animating;

	void Awake()
    {
        this.animator = this.GetComponent<Animator>();
		IsShown = false;
    }

	IEnumerator CallEventOnAnimatorAnimationFinish()
	{
		animating = true;
		while(animating){
			yield return new WaitForEndOfFrame();
			if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !animator.IsInTransition(0)){
				
				animating = false;
			}
			// for (int i = 0; i < 5; i++)
			// {
			// 	yield return new WaitForEndOfFrame();
			// }
		}
		// onHideAnimFinish.Invoke();
	}

	public void OnShowAnim_Finish(){
		onShowAnim_Finish?.Invoke(); 
	}

    public void OnHideAnim_Finish(){
        onHideAnim_Finish?.Invoke();
        //appLogo.SetActive(true);
    }

    public void ShowSideBar_StartAnim(){
        animator.SetBool("IsShown", true);
		animator.SetTrigger("Show"); //??
		IsShown = true;
        //appLogo.SetActive(false);
        // this.StartCoroutine(CallEventOnAnimatorAnimationFinish());
    }

    public void HideSideBar_StartAnim(Action onHideAnimFinish = null){
        animator.SetBool("IsShown", false);
		animator.SetTrigger("Hide");	//??
		IsShown = false;
        // if(onHideAnimFinish != null){
        // 	this.StartCoroutine(CallEventOnAnimatorAnimationFinish(onHideAnimFinish));
        // }
    }

    public void ToggleSideBar_StartAnim(){
		bool curr = animator.GetBool("IsShown");
        animator.SetBool("IsShown", !(curr) );
		IsShown = animator.GetBool("IsShown");
    }

	public bool IsAnimating(){
		return animator.IsAnimating();
	}
	
}
