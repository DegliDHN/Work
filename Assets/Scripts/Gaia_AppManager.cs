using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum GaiaStates
{
	None = 0,
	Home,
	SidebarShown,
	AR,
	DisancoredMarker,
	InfoPanel,
	PrivacyPolicy
}

public class Gaia_AppManager : Singleton<Gaia_AppManager> 
{
	public Login_AppManager login_AppManager;
	public Button screenTouchDetector; //TODO: Change later
	public Button hamburgerButton;
	public CanvasGroup hamburgerButton_canvasGroup;
	public SidePanel sidePanel;
	public Button exitARModeButton;
	public CanvasGroup exitARModeButton_canvasGroup;
	public AROverlay_AnimatorHandler arOverlay_Animator;
	public HomeUI_AnimatorHandler homeUI_Animator;
	public GameObject arScriptsContainer;
	private StateMachine<GaiaStates> appStateMachine;
	public GameObject webviewsContainer;
    public GameObject appLogo;
	
	protected override void InitTon()
	{
		OverlayCanvas.Instance.gameObject.SetActive(false);
		
		exitARModeButton_canvasGroup = exitARModeButton.gameObject.AddOrGetComponent<CanvasGroup>();
		exitARModeButton_canvasGroup.alpha = 0f;
		exitARModeButton.interactable = false;
		
		hamburgerButton_canvasGroup = hamburgerButton.gameObject.AddOrGetComponent<CanvasGroup>();

		appStateMachine = StateMachine<GaiaStates>.Initialize(this);

		appStateMachine.Changed += TimeElapsedInState_Tracker;	//Set up analytics by monitoring this state machine's changes

		// sidePanel.profileTab.gameObject.SetActive(false);

		login_AppManager.onUserLoggedIn += () => {
			Debug.Log("Addressable Load Successful");
			string name = AppDB.Instance.GetUser_Name();
			string surname = AppDB.Instance.GetUser_Surname();
			string email = AppDB.Instance.GetUser_Email();
			string qualifica = AppDB.Instance.GetUser_Qualifica();
			
			sidePanel.profileTab.SetInfo_WithLabels(name, surname, email, qualifica);
			appStateMachine.ChangeState(GaiaStates.Home);
		};

		this.Log("Login Linked");

		login_AppManager.enabled = (true);

		hamburgerButton.onClick.AddListener( ()=> appStateMachine.ChangeState(GaiaStates.SidebarShown) );

		sidePanel.exitAppBtn.onClick.AddListener( () => Application.Quit() ); 
		
		sidePanel.activateARBtn.onClick.AddListener ( () => appStateMachine.ChangeState(GaiaStates.AR));

		screenTouchDetector.onClick.AddListener( ()=> appStateMachine.ChangeState(GaiaStates.AR) );

		exitARModeButton.onClick.AddListener( ()=> appStateMachine.ChangeState(GaiaStates.Home) );


		for(int i=0; i< sidePanel.ContentButtons.Length; i++){
			InfoPanel infoPanel = sidePanel.InfoPanels[i];
			Button contentBtn = sidePanel.ContentButtons[i];
			contentBtn.onClick.AddListener(()=> ChangeStateToInfoPanel(infoPanel));
		}
		
		sidePanel.infoPanelsContainer.SetActive(true);
		var infoPanelBackBtns = sidePanel.InfoPanels.Select(ip => ip.closeBtn);
		sidePanel.gameObject.SetActive(false);

		foreach(var backBtn in infoPanelBackBtns){
			backBtn.onClick.AddListener( () => appStateMachine.ChangeState(GaiaStates.SidebarShown));
		}

		login_AppManager.enabled = (true);

		arScriptsContainer.SetActive(false);

		webviewsContainer.SetActive(true);

	}

	private void TimeElapsedInState_Tracker(GaiaStates previous, GaiaStates next){
		if(previous != GaiaStates.None){
			EventAnalyticsTracker.Instance.NotifyState_Finish(previous);
		}
		EventAnalyticsTracker.Instance.NotifyState_Start(next);
	}

	private void ChangeStateToHome(){
		appStateMachine.ChangeState(GaiaStates.Home);
	}

	private void ChangeStateToInfoPanel(InfoPanel selectedInfoPanel){
		currInfopanel = selectedInfoPanel;
		appStateMachine.ChangeState(GaiaStates.InfoPanel);
	}

	private void Home_Enter(){
		this.Log("[App] Home_Enter");
		if(homeUI_Animator.IsUIShown == false){
			homeUI_Animator.ShowUI_StartAnim();
		}

		//hamburgerButton.gameObject.SetActive(true);
		hamburgerButton_canvasGroup.alpha = 0f;
		hamburgerButton_canvasGroup.DOFade(1f, 0.5f);
		hamburgerButton.gameObject.SetActive(true);
		screenTouchDetector.gameObject.SetActive(true);

		// foreach(var webview in webviewsContainer.GetComponentsInChildren<UniWebView>()){
		// 	webview.Reload();
		// }
	}

	private void Home_Exit(){
		this.Log("[App] Home_Exit");
		screenTouchDetector.gameObject.SetActive(false);
	}


    private IEnumerator SidebarShown_Enter()
    {
        this.Log("[App] SidebarShown_Enter");
        sidePanel.gameObject.SetActive(true);
        sidePanel.outsideSidepanelRect.gameObject.SetActive(true);
        sidePanel.onClickOutsidePanel += ChangeStateToHome;

        hamburgerButton_canvasGroup.DOFade(0f, 0.25f).onComplete += () => hamburgerButton.gameObject.SetActive(false);
        if (sidePanel.IsShown == false)
        {
            appLogo.SetActive(false);
            sidePanel.ShowSideBar_StartAnim();
        }
        yield return new WaitUntil(() => sidePanel.IsAnimating() == false);
    }

    private IEnumerator SidebarShown_Exit()
    {
        this.Log("[App] SidebarShown_Exit, going to " + appStateMachine.NextState);
        sidePanel.onClickOutsidePanel -= ChangeStateToHome;

        appLogo.SetActive(true);
        sidePanel.HideSideBar_StartAnim();
        yield return null;
        yield return new WaitUntil(() => sidePanel.IsAnimating() == false);
    }

    private void AR_Enter(){
		this.Log("[App] AR_Enter");
		hamburgerButton.interactable = false;
		exitARModeButton.interactable = true;
		hamburgerButton_canvasGroup.DOFade(0f, 0.4f);
		exitARModeButton_canvasGroup.DOFade(1f,0.8f);
		//Hide all UI
		homeUI_Animator.HideUI_StartAnim();
		//Enable image recognition scripts
		//Activate Ar
		arOverlay_Animator.ShowAROverlay_StartAnim();
		arScriptsContainer.SetActive(true);

		sidePanel.gameObject.SetActive(false);
		sidePanel.infoPanelsContainer.SetActive(false);
	}

	private void AR_Exit(){
		this.Log("[App] AR_Exit");
		hamburgerButton.interactable = true;
		exitARModeButton.interactable = false;
		hamburgerButton_canvasGroup.DOFade(1f, 0.8f);
		exitARModeButton_canvasGroup.DOFade(0f,0.4f);
		//Restore UI?
		arOverlay_Animator.HideAROverlay_StartAnim();
		// arOverlay_Animator.HideFooter_Panel();
		//Disable image recognition scripts
		arScriptsContainer.SetActive(false);

		//Deactivate all ar contents
		GameObjectEx.FindAllOfType<ClickableTrackableUX>().ForEach(obj => obj.gameObject.SetActive(false));

		sidePanel.gameObject.SetActive(true);
		sidePanel.infoPanelsContainer.SetActive(true);
	}

	private InfoPanel currInfopanel;

	private void InfoPanel_Enter(){
		this.Log("[App] InfoPanel_Enter");
		hamburgerButton.gameObject.SetActive(false);


		// sidePanel.infoPanelsContainer.SetActive(true);
		// sidePanel.gameObject.SetActive(false);
	}

	private void InfoPanel_Exit(){
		this.Log("[App] InfoPanel_Exit");
		// sidePanel.gameObject.SetActive(true);
		// sidePanel.InfoPanels.ForEach( ip => ip.gameObject.SetActive(false) );
		// sidePanel.InfoPanels.ForEach( ip => ip.Hide_AnimFromBottom() );
		// sidePanel.infoPanelsContainer.SetActive(false);

		currInfopanel.Hide_AnimFromBottom();
		currInfopanel = null;
	}
}
