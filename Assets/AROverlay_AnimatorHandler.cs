using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;

public class AROverlay_AnimatorHandler : Singleton<AROverlay_AnimatorHandler>
{
    private Animator myAnim;
	public FooterPanel bottomPanel;
	private bool showFlag = false;


	[Button]
	public void ShowAROverlay_StartAnim(){
		if(showFlag){
			return;
		}
		
		myAnim.SetBool("IsShown", true);
		myAnim.SetTrigger("Show");
		showFlag = true;
	}

	[Button]
	public void HideAROverlay_StartAnim(){
		this.Log("Hide");
		myAnim.SetBool("IsShown", false);
		myAnim.SetTrigger("Hide");
		showFlag = false;
	}
	[Button]

	public void TargetFound_StartAnim(){
		this.Log("TargetFound");
		// ShowAROverlay_StartAnim();	//?
		myAnim.SetTrigger("TargetFound");
		// HideAROverlay_StartAnim();
		showFlag = false;
	}

	protected override void InitTon()
	{
		myAnim = GetComponent<Animator>();
		myAnim.SetBool("IsShown", false);
	}

	// public void ShowFooter_Panel(){
	// 	bottomPanel.gameObject.SetActive(true);
	// 	var targetY = 0f;
	// 	bottomPanel.GetComponent<RectTransform>().DOAnchorPosY(targetY, 0.5f).SetEase(Ease.InCubic);
	// }

	// public void HideFooter_Panel(){
	// 	var targetY = bottomPanel.GetComponent<RectTransform>().sizeDelta.y * -1f;
	// 	bottomPanel.GetComponent<RectTransform>().DOAnchorPosY(targetY, 0.5f).SetEase(Ease.InCubic);
	// }
}
