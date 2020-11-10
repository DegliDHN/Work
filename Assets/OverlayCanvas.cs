using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class OverlayCanvas : Singleton<OverlayCanvas>
{
	public FooterPanel footerPanel;
	// public Button[] ArButtons => new Button[]{bioBtn, linkedinBtn, emailBtn, phoneBtn};

	public TMP_Text nameText, roleText;
	// public GameObject name_and_role_panel;
	public GameObject bioPanel;
	private ScrollRect bioPanel_scrollview;
	public TMP_Text bioText;
	public Button closeBtn_bio;
	public TMP_Text arBubbleText;
	public RawImage picture;
    public String markerName;

	protected override void InitTon()
	{
		bioPanel_scrollview = bioPanel.GetComponentInChildren<ScrollRect>(true);
	}

	public void UpdateInfoOVerlay_WithArContent(Ar_Content arContent)
	{
		arBubbleText.text = arContent.fraseDellaNonna;
		bioText.text = arContent.bioText;
		nameText.text = arContent.name;
		roleText.text = arContent.role;
		picture.texture = arContent.texture;
        markerName = TrackedImageInfoManager_OffV.markerName;

		bioPanel.SetActive(false);
		bioPanel_scrollview.verticalScrollbar.value = 1f;

		// footerPanel.BindFooterPanelBtns(arContent);

		footerPanel.bioBtn_outline.enabled = false;
		footerPanel.bioBtn_image.color = footerPanel.bioBtn_outline.effectColor;

		foreach (var btn in footerPanel.ArButtons)
		{
			btn.onClick.RemoveAllListeners();
		}

		footerPanel.bioBtn.onClick.AddListener(() =>
		{
			bool isBioPanelActive = bioPanel.activeSelf;

			isBioPanelActive = !isBioPanelActive; //toggle panel
			this.Log("Setting bio panel to " + isBioPanelActive);
			bioPanel.SetActive(isBioPanelActive); //toggle panel

			// trackableUx.arBubble.gameObject.SetActive(!isPanelActive); //bubble is opposite of panel
			if (isBioPanelActive)
			{
				// clickableUx.deactivateARContentsBtn.gameObject.SetActive(false);
				// trackableUx.bioPanel.GetComponentInChildren<ScrollRect>(true).verticalScrollbar.value = 1f;

				//change button style
				footerPanel.bioBtn_outline.enabled = true;
				footerPanel.bioBtn_image.color = Color.white;
				bioPanel_scrollview.verticalScrollbar.value = 1f;
			}
			else
			{
				// clickableUx.deactivateARContentsBtn.gameObject.SetActive(true);
				footerPanel.bioBtn_outline.enabled = false;
				footerPanel.bioBtn_image.color = footerPanel.bioBtn_outline.effectColor;
			}
		}
		);

		// trackableUx.closeBtn_bio.onClick.AddListener(() => trackableUx.bioBtn.onClick.Invoke());

		footerPanel.linkedinBtn.onClick.AddListener(() => {
			// GameObjectEx.FindAllOfType<ClickableTrackableUX>().ForEach(obj => obj.gameObject.SetActive(false));
			Application.OpenURL(arContent.linkedinUrl);
			}
		);
		footerPanel.emailBtn.onClick.AddListener(() => {
			// GameObjectEx.FindAllOfType<ClickableTrackableUX>().ForEach(obj => obj.gameObject.SetActive(false));
			Application.OpenURL("mailto:" + arContent.email);
			}
		);
		footerPanel.phoneBtn.onClick.AddListener(() => {
			var phoneNumber = arContent.phoneNumber.Trim();
			phoneNumber = phoneNumber.Replace(" ", "");
			Debug.Log("Opening Telephone " + phoneNumber);
			// GameObjectEx.FindAllOfType<ClickableTrackableUX>().ForEach(obj => obj.gameObject.SetActive(false));
			Application.OpenURL("tel://" + phoneNumber);
		});
	}

}
