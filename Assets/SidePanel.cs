using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class SidePanel : MonoBehaviour
{
	public Image outsideSidepanelRect;
    public Button exitAppBtn, activateARBtn;
	public Button content1Btn, content2Btn, content3Btn, profileBtn;
	public InfoPanel infoPanel1, infoPanel2, infoPanel3;
	public ProfileTab profileTab;

	public Transform sidePanelHiddenPos, sidePanelShownPos;
	public GameObject buttonsContainer;
	private RectTransform rectTransform;
	public Action onClickOutsidePanel;

	public Button[] ContentButtons { get => new Button[]{content1Btn, content2Btn, content3Btn, profileBtn}; }

	public InfoPanel[] InfoPanels { get => new InfoPanel[]{infoPanel1, infoPanel2, infoPanel3, profileTab.GetComponent<InfoPanel>()}.Where(elem => elem != null).ToArray(); }
	public GameObject infoPanelsContainer;
	private bool isAnimating;
	public bool IsShown { get; private set;}
	

	public void Awake(){
		this.transform.position = sidePanelHiddenPos.position;

		content1Btn.onClick.AddListener( ()=> infoPanel1.Show_AnimFromBottom() );
		content2Btn.onClick.AddListener( ()=> infoPanel2.Show_AnimFromBottom() );
		content3Btn.onClick.AddListener( ()=> infoPanel3.Show_AnimFromBottom() );
		profileBtn.onClick.AddListener( () => profileTab.GetComponent<InfoPanel>().Show_AnimFromBottom() );

		rectTransform = GetComponent<RectTransform>();

		outsideSidepanelRect.raycastTarget = false;
		outsideSidepanelRect.gameObject.AddOrGetComponent<OnClick>().onClick +=  ()=> onClickOutsidePanel?.Invoke();
	}

	public void ShowSideBar_StartAnim(){
		this.isAnimating = true;
        this.IsShown = true;
		var anim = this.transform.DOMove(this.sidePanelShownPos.position, 0.4f).SetEase(Ease.InOutCubic);
		
		anim.onComplete += ()=> {
			this.isAnimating = false;
			outsideSidepanelRect.raycastTarget = true;
		};
    }

    public void HideSideBar_StartAnim(Action onHideAnimFinish = null){
		outsideSidepanelRect.raycastTarget = false;
		var anim = this.transform.DOMove(this.sidePanelHiddenPos.position, 0.25f).SetEase(Ease.OutSine);
		this.IsShown = false;
		anim.onComplete += ()=>onHideAnimFinish?.Invoke();
		anim.onComplete += ()=>this.isAnimating = false;
    }

	public bool IsAnimating()
	{
		return isAnimating;
	}

	//??
	void OnDisable(){
		infoPanelsContainer.SetActive(false);
	}

	void OnEnable(){
		infoPanelsContainer.SetActive(true);
	}
	//
}
