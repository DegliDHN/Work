using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class ClickableTrackableUX : MonoBehaviour
{
	public Canvas arWorldCanvas;
	// public Image borderRecognized;
	public MeshRenderer imageTexturePlane;
	public Button activateARContentsBtn;
	// public GameObject clickMe;
	public CanvasGroup paper2app_user_page_canvasgrp;
	// public Button select_img_btn, deselect_img_btn;

	public Action onARContentsActivated = ()=>{}, onARContentsDeactivated = ()=>{};
	// public Action onARContentEnabled = ()=>{};
	public ArObjectLinker linker;

	private static ClickableTrackableUX CURRENTLY_ACTIVE;
	private bool hookOverrittenFlag;
	public GameObject touchIconGO;
	private ARSessionOrigin session;
	public TMP_Text actionDescriptor;
	public GameObject detectionUI;

	//Hack a roo
	[HideInInspector] public bool tracking = false, flag = false;
	[HideInInspector] public float lastTrackedTime = -1;


	void Awake(){
		print("Instantiated");
		activateARContentsBtn.onClick.AddListener(OnARImageActivated);
	}

	public void SetListenerForMarkerWithAction(Action onARContentEnabled){
		activateARContentsBtn.onClick.RemoveAllListeners();
		activateARContentsBtn.onClick.AddListener( ()=>onARContentEnabled()) ;
	}


    public void OnEnable()	//Imaged Recognized
    {
		Debug.Log("[DBG] OnEnable");
		ResetState();
		touchIconGO.SetActive(true);
		linker?.gameObject.SetActive(true);
		detectionUI.SetActive(true);
    }

	public void ResetState(){
		// borderRecognized.gameObject.SetActive(true);	//Lerp
		imageTexturePlane.gameObject.SetActive(false);
		
		activateARContentsBtn.gameObject.SetActive(true);
	}

	public void OnDisable(){
		print("[DBG] Disable");
		OnARImageDeactivated();
		linker.gameObject.SetActive(false);
		touchIconGO.SetActive(false);
		detectionUI.SetActive(false);
	}

	private void OnDestroy() {
		print("[DBG] Destroyed");
		linker.gameObject.SetActive(false);
		touchIconGO.SetActive(false);
	}

	public virtual void OnARImageActivated(){	//User Input - Expand UI
		Debug.Log("Image Activated");
		CURRENTLY_ACTIVE?.OnARImageDeactivated();
		CURRENTLY_ACTIVE = this;
		//borderRecognized.gameObject.SetActive(false);
		detectionUI.SetActive(false);

		activateARContentsBtn.gameObject.SetActive(false);
		

		//FX
		AROverlay_AnimatorHandler.Instance.TargetFound_StartAnim();
		onARContentsActivated();
	}

	public void OnARImageDeactivated()
	{
		CURRENTLY_ACTIVE = null;
		ResetState();

		AROverlay_AnimatorHandler.Instance.ShowAROverlay_StartAnim();

		onARContentsDeactivated();
	}

}
