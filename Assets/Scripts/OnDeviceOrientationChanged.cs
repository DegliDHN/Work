using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDeviceOrientationChanged : MonoBehaviour
{
    public Action<DeviceOrientation> onOrientationChanged; 
    private DeviceOrientation prevOrientation; 

    private void Start() {
        Debug.Log("OnDeviceOrientationChanged Component Added");    
    }

    void Update()
    {
        if(Input.deviceOrientation != prevOrientation){
            if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft ||
                Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {
                Debug.Log("we landscape now.");
                
            }
            else if (Input.deviceOrientation == DeviceOrientation.Portrait)
            {
                Debug.Log("we portrait now");
                
            }
            onOrientationChanged.Invoke(Input.deviceOrientation);
        }

        prevOrientation = Input.deviceOrientation;
    }
}
