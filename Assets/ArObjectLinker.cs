using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArObjectLinker : MonoBehaviour
{
	[HideInInspector] public Transform linkedTrackable = null;
	private bool wasMarkerActiveLastFrame;

	public void Awake(){
		// canvasGroup = GetComponent<CanvasGroup>();
	}

    public void LinkToARMarker(Transform trackable){
		this.linkedTrackable = trackable;
		this.transform.SetParent(null, true);
		this.transform.position = trackable.position;
		this.transform.rotation = trackable.rotation;
		this.transform.localScale = trackable.lossyScale;
	}

	public void UnlLinkToARMarker(ARTrackedImage trackable){
		this.linkedTrackable = null;
	}

	public void LateUpdate(){
		
		if(linkedTrackable == null){ return; }

		if(linkedTrackable.gameObject.activeInHierarchy == false){
			// canvasGroup.alpha = 0f;
			// if(wasMarkerActiveLastFrame){
				this.transform.GetChildren().ForEach(c => c.gameObject.SetActive(false));
			// }
			// wasMarkerActiveLastFrame = false;
			return;
		} else {
			// canvasGroup.alpha = 1f;
			// if(wasMarkerActiveLastFrame == false){
				this.transform.GetChildren().ForEach(c => c.gameObject.SetActive(true));
				//Snap to place
				this.transform.position = linkedTrackable.position;
				this.transform.rotation = linkedTrackable.rotation;
				this.transform.localScale = linkedTrackable.lossyScale;
			// }
		}

		this.transform.localScale = Vector3.Lerp(this.transform.localScale, linkedTrackable.transform.lossyScale, 0.4f);
		
		if(Vector3.Distance(this.transform.localScale, linkedTrackable.localScale) < 0.1f){
			this.transform.localScale = linkedTrackable.localScale;
		}

		this.transform.position = Vector3.Lerp(this.transform.position, linkedTrackable.position, 0.3f);
		
		if(Vector3.Distance(this.transform.position, linkedTrackable.position) < 0.1f){
			this.transform.position = linkedTrackable.position;
		}

		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, linkedTrackable.rotation, 0.3f);
		
		if(Mathf.Abs(Quaternion.Angle(this.transform.rotation, linkedTrackable.rotation)) < 1f){
			this.transform.rotation = linkedTrackable.rotation;
		}
		
		// wasMarkerActiveLastFrame = linkedTrackable.gameObject.activeSelf;
		
	}
	


}
