using System;
using System.Collections.Generic;
using NaughtyAttributes;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Video;
using UnityEngine.Events;

[System.Serializable]
public class Dictionary_StringToAction : SerializableDictionaryBase<string, ExternalApplication> { }

[Serializable] public class StringEvent : UnityEvent<string> { }

[Serializable]
public enum P2A_ButtonAction{
	OpenARContent=0,
	OpenLink=1,
	OpenMail=2,
	OpenPhone=3
}

[Serializable]
public struct ExternalApplication{
	// public P2A_ButtonAction action;
	public string actionText_manina;
	public StringEvent action;
}

public enum TransitionAnimation{
	OnOff = -1,
	Fade = 0
}

[Serializable]
public struct Ar_Content{
	public Texture texture;
	public string actionText;
	public TransitionAnimation animation;
	public string linkedinUrl;
	public string email;
	public string phoneNumber;
	public string name;
	public string role;
	[TextArea]
	public string fraseDellaNonna;
	[TextArea]
	public string bioText;
	public VideoClip videoNonna;
}

[RequireComponent(typeof(ARAnchorManager))]
public class TrackedImageInfoManager_OffV : TrackedImageManager
{
	public Dictionary_StringToAction nameToAction;
	Dictionary<ARTrackedImage, ARAnchor> image_to_anchor;
	private ARTrackedImageManager arManager;
	private ARSession aRSession;
    public static string markerName;

	private void Awake() {
		// OverlayCanvas.Instance.gameObject.SetActive(false);
		arManager = GetComponent<ARTrackedImageManager>();
		aRSession = GameObject.FindObjectOfType<ARSession>();
	}

    protected override void SetTexture(ARTrackedImage trackedImage, Material material){
		//non serve fare nulla, tanto non usiamo questa feature.
	}

	protected override void InitTrackedObject(Transform trackedImage, string trackedImageName)
	{
		base.InitTrackedObject(trackedImage, trackedImageName);
		var clickableUx = trackedImage.gameObject.GetComponent<ClickableTrackableUX>();
		clickableUx.linker = trackedImage.GetComponentInChildren<ArObjectLinker>();
		clickableUx.linker.LinkToARMarker(trackedImage);

		this.Log("[AR] Tracking found image "+trackedImageName);
		
		clickableUx.actionDescriptor.text = nameToAction[trackedImageName].actionText_manina;
		if (nameToAction.ContainsKey(trackedImageName))
		{

			Action newAction = () =>
			{
				//nameToAction[trackedImageName].action.Invoke();
                nameToAction[trackedImageName].action.Invoke(trackedImageName);
                markerName = trackedImageName;
                Debug.Log(markerName);

            };

			clickableUx.SetListenerForMarkerWithAction(newAction);
			return;
		}
	}


	private ARTrackedImage trackedImageToDebug;
	public string trackableName = "";
	[Button]
	public void Debug_OnImageRecognized(){
		if(trackedImageToDebug != null){
			Destroy(trackedImageToDebug);
		}

		var trackablePrefab = GetComponent<ARTrackedImageManager>().trackedImagePrefab;
		GameObject newTrackable = Instantiate(trackablePrefab);

		InitTrackedObject(newTrackable.transform, trackableName);
		trackedImageToDebug = newTrackable.GetComponent<ARTrackedImage>();
	}

	[Button]
	public void Debug_OnImageClicked(){
		trackedImageToDebug.GetComponent<ClickableTrackableUX>().OnARImageActivated();
	}


	private Coroutine hideCoro;
  
	private void OnApplicationFocus(bool hasFocus){
		// Debug.Log("2 Application Focus: "+hasFocus);
		aRSession.Reset();

		if(hasFocus){
			aRSession.enabled = true;
			// Debug.Log("4 Enable AR Session");
		}
	}

	private void OnApplicationPause(bool pauseStatus){
		// Debug.Log("2 Application Pause: "+pauseStatus);
		aRSession.Reset();

		if(pauseStatus == false){
			aRSession.enabled = true;
			// Debug.Log("5 Enable AR Session");
		}
	}

	private void RemoveARTrackables()
	{
		GameObjectEx.FindAllOfType<ClickableTrackableUX>().ForEach(obj => Destroy(obj.gameObject));
	}
}
