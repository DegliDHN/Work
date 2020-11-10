using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public Image map;
    private RectTransform rectTransform;

    void Awake(){
        rectTransform = map.GetComponent<RectTransform>();
    }

    void Start()
    {
        this.gameObject.AddOrGetComponent<OnDeviceOrientationChanged>().onOrientationChanged += RotateMap;
    }

    void RotateMap(DeviceOrientation orientation)
    {
        
        switch (orientation) {
            case DeviceOrientation.Portrait:
                map.transform.localEulerAngles = new Vector3(0f,0f,0f);  
                // rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
                break;
            case DeviceOrientation.LandscapeLeft:
                map.transform.localEulerAngles = new Vector3(0f,0f,-90f);
                // rectTransform.sizeDelta = new Vector2(Screen.height, Screen.width);
                break;
            case DeviceOrientation.LandscapeRight:
                map.transform.localEulerAngles = new Vector3(0f,0f,90f);
                // rectTransform.sizeDelta = new Vector2(Screen.height, Screen.width);
                break;
            default:
                break;
        }
    }

    //TODO: Zoom
}
