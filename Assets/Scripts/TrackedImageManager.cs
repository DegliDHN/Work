using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System.Collections;

/// This component listens for images detected by the <c>XRImageTrackingSubsystem</c>
/// and overlays some information as well as the source Texture2D on top of the
/// detected image.
/// </summary>
[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The camera to set on the world space UI canvas for each instantiated image info.")]
    Camera m_WorldSpaceCanvasCamera;

    /// <summary>
    /// The prefab has a world space UI canvas,
    /// which requires a camera to function properly.
    /// </summary>
    public Camera worldSpaceCanvasCamera
    {
        get { return m_WorldSpaceCanvasCamera; }
        set { m_WorldSpaceCanvasCamera = value; }
    }

    [SerializeField]
    [Tooltip("If an image is detected but no source texture can be found, this texture is used instead.")]
    Texture2D m_DefaultTexture;

    /// <summary>
    /// If an image is detected but no source texture can be found,
    /// this texture is used instead.
    /// </summary>
    public Texture2D defaultTexture
    {
        get { return m_DefaultTexture; }
        set { m_DefaultTexture = value; }
    }

    ARTrackedImageManager m_TrackedImageManager;
    private Coroutine hideTrackableCoro = null;

    // public GameObject arContentPrefab;

    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    protected virtual void InitTrackedObject(Transform trackedImage, string trackedImageName)
    {
        ClickableTrackableUX clickableUx = trackedImage.GetComponent<ClickableTrackableUX>();
        var canvas = clickableUx.arWorldCanvas;
        canvas.worldCamera = worldSpaceCanvasCamera;

        //Instantiate AR Content
        // GameObject arContent = Instantiate(arContentPrefab);
        //Link AR content to ARTrackedImage
        // var planeParentGo = trackedImage.GetChild(0).GetChild(0).gameObject;
        // var planeGo = planeParentGo.transform.GetChild(0).gameObject;

        // var material = planeGo.GetComponentInChildren<MeshRenderer>().material;
        // SetTexture(trackedImage, material);
    }

    private float lostTracking_timeStamp;


    void UpdateInfo(ARTrackedImage trackedImage)
    {
        // var planeParentGo = trackedImage.transform.GetChild(0).gameObject;
        // var planeGo = planeParentGo.transform.GetChild(0).gameObject;

        // var canvasGo = trackedImage.transform.GetChild(1).gameObject;
        // var detectinoObjs = canvasGo.transform.GetChild(0).gameObject;
        var clickableUx = trackedImage.GetComponent<ClickableTrackableUX>();
        UpdateTrackedImageInfo(trackedImage, null);

        // Disable the visual plane if it is not being tracked
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            if (clickableUx.tracking == false && clickableUx.flag == false)
            {
                clickableUx.lastTrackedTime = Time.time;
                clickableUx.flag = true;
                Debug.Log($"[Tracking] Half Tracked: {trackedImage.referenceImage.name}");

            }
            if (clickableUx.flag && Time.time - clickableUx.lastTrackedTime > 0.5f)
            {
                clickableUx.tracking = true;
                clickableUx.flag = false;
                Debug.Log($"[Tracking] Fully Tracked: {trackedImage.referenceImage.name}");
                Handheld.Vibrate();
                // lastTrackedTime = -1;
            }

            // if(hideTrackableCoro != null ){
            // 	Debug.Log("Stop Hiding Coro");

            // 	this.StopCoroutine(hideTrackableCoro);
            // 	hideTrackableCoro = null;
            // }
            // // trackedImage.gameObject.GetComponentInChildren<CanvasGroup>().alpha = 1f;
            // trackedImage.gameObject.SetActive(true);
            // // The image extents is only valid when the image is being tracked
            // trackedImage.transform.localScale = new Vector3(trackedImage.size.x, 1f, trackedImage.size.y);
        }
        else if (trackedImage.trackingState == TrackingState.None)
        {
            if (clickableUx.tracking)
            {
                clickableUx.tracking = clickableUx.flag = false;
                Debug.Log($"[Tracking] Lost - None: {trackedImage.referenceImage.name}");
            }

            // Debug.Log($"[Tracking] None: {trackedImage.referenceImage.name}");
            // if(trackedImage.gameObject.activeSelf && hideTrackableCoro == null){	
            // 	hideTrackableCoro = StartCoroutine( HideImageIfCantTrackInSeconds(0.3f, trackedImage) );
            // }
        }
        else if (trackedImage.trackingState == TrackingState.Limited)
        {
            // StartCoroutine(SetTrackingToFalse_Delayed_Coro(0.3f));
            // tracking = false;
            if (clickableUx.tracking)
            {
                clickableUx.tracking = clickableUx.flag = false;
                Debug.Log($"[Tracking] Lost - Limited: {trackedImage.referenceImage.name}");
            }
            // Debug.Log($"[Tracking] Limited: {trackedImage.referenceImage.name}");
        }

        if (clickableUx.tracking)
        {
            if (hideTrackableCoro != null)
            {
                Debug.Log("Stop Hiding Coro");

                this.StopCoroutine(hideTrackableCoro);
                hideTrackableCoro = null;
            }
            // trackedImage.gameObject.GetComponentInChildren<CanvasGroup>().alpha = 1f;
            trackedImage.gameObject.SetActive(true);
            // The image extents is only valid when the image is being tracked
            trackedImage.transform.localScale = new Vector3(trackedImage.size.x, 1f, trackedImage.size.y);
        }
        else
        {
            if (trackedImage.gameObject.activeSelf && hideTrackableCoro == null)
            {
                hideTrackableCoro = StartCoroutine(HideImageIfCantTrackInSeconds(0.6f, trackedImage));
            }
        }
    }

    // private IEnumerator SetTrackingToFalse_Delayed_Coro(float delay){
    // 	yield return new WaitForSeconds(delay);
    // 	tracking = false;
    // }

    private IEnumerator HideImageIfCantTrackInSeconds(float timeInSeconds, ARTrackedImage trackedImage)
    {
        Debug.Log("Start Hiding Coro " + trackedImage.referenceImage.name);
        yield return new WaitForSeconds(timeInSeconds);
        // Debug.Log("trackedImage.trackingState != TrackingState.Tracking "+ (trackedImage.trackingState != TrackingState.Tracking));
        // if(trackedImage.trackingState != TrackingState.Tracking){
        trackedImage.gameObject.SetActive(false);
        // }
        hideTrackableCoro = null;
        this.Log("Hidden " + trackedImage.referenceImage.name);
    }

    protected virtual void UpdateTrackedImageInfo(ARTrackedImage trackedImage, GameObject planeGo) { }

    protected virtual void SetTexture(ARTrackedImage trackedImage, Material material)
    {
        material.mainTexture = (trackedImage.referenceImage.texture == null) ? defaultTexture : trackedImage.referenceImage.texture;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Give the initial image a reasonable default scale
            trackedImage.transform.localScale = new Vector3(0.01f, 1f, 0.01f);

            this.Log($"[Trackable] Added{trackedImage.referenceImage.name}");

            // InitTrackedObject(trackedImage);
            InitTrackedObject(trackedImage.transform, trackedImage.referenceImage.name);

            UpdateInfo(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateInfo(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            trackedImage.gameObject.SetActive(false);
            this.Log($"[Trackable] Removed {trackedImage.referenceImage.name}");
        }

    }
}
