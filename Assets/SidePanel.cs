using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class SidePanel : MonoBehaviour
{
	public Image outsideSidepanelRect;
    public Button exitAppBtn, activateARBtn;
	public Button content1Btn, content2Btn, content3Btn, profileBtn;
	public InfoPanel infoPanel1, infoPanel2, infoPanel3;
	public ProfileTab profileTab;

	public SideBarPanel_AnimatorHandler sideBarPanel_AnimHandler;
	public GameObject buttonsContainer;
	private RectTransform rectTransform;
	public Action onClickOutsidePanel;

	public Button[] ContentButtons { get => new Button[]{content1Btn, content2Btn, content3Btn, profileBtn}; }

	public InfoPanel[] InfoPanels { get => new InfoPanel[]{infoPanel1, infoPanel2, infoPanel3, profileTab.GetComponent<InfoPanel>()}.Where(elem => elem != null).ToArray(); }
	public GameObject infoPanelsContainer;


	public void Awake(){
		// InfoPanels.ForEach(ip => ip.gameObject.SetActive(false));

		content1Btn.onClick.AddListener( ()=> infoPanel1.Show_AnimFromBottom() );
		content2Btn.onClick.AddListener( ()=> infoPanel2.Show_AnimFromBottom() );
		content3Btn.onClick.AddListener( ()=> infoPanel3.Show_AnimFromBottom() );
		profileBtn.onClick.AddListener( () => profileTab.GetComponent<InfoPanel>().Show_AnimFromBottom() );

		// content1Btn.onClick.AddListener( ()=> buttonsContainer.SetActive(false) );
		// content2Btn.onClick.AddListener( ()=> buttonsContainer.SetActive(false) );
		// content3Btn.onClick.AddListener( ()=> buttonsContainer.SetActive(false) );
		// profileBtn.onClick.AddListener( ()=> buttonsContainer.SetActive(false) );

		rectTransform = GetComponent<RectTransform>();

		// InfoPanels.ForEach( ip => ip.closeBtn.onClick.AddListener( ()=> buttonsContainer.SetActive(true) ) );

		outsideSidepanelRect.raycastTarget = false;
		outsideSidepanelRect.gameObject.AddOrGetComponent<OnClick>().onClick +=  ()=> onClickOutsidePanel?.Invoke() ;
	}

	public void ShowSideBar_StartAnim(){
        sideBarPanel_AnimHandler.ShowSideBar_StartAnim();
		outsideSidepanelRect.raycastTarget = true;
    }

	public void HideSideBar_StartAnim(){
		outsideSidepanelRect.raycastTarget = false;
		sideBarPanel_AnimHandler.HideSideBar_StartAnim();
    }

    public void HideSideBar_StartAnim(Action onHideAnimFinish){
		outsideSidepanelRect.raycastTarget = false;
		sideBarPanel_AnimHandler.HideSideBar_StartAnim(onHideAnimFinish);
    }

	public bool IsAnimating()
	{
		return sideBarPanel_AnimHandler.IsAnimating();
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
