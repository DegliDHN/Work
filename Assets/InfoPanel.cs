using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoPanel : MonoBehaviour
{
    public Button closeBtn;
	public Transform startPosition, finalPosition;

	private void Awake() {
		this.transform.position = startPosition.position;
	}

	[ContextMenu("Show")]
	public void Show_AnimFromBottom(){
		this.transform.DOMove(finalPosition.position, .45f).SetEase(Ease.InOutCubic);
		
	}

	[ContextMenu("Hide")]
	public void Hide_AnimFromBottom(){
		this.transform.DOMove(startPosition.position, .40f).SetEase(Ease.OutSine);
	}
	
	// public void OnEnable(){
	// 	Show_AnimFromBottom();
	// }

}
