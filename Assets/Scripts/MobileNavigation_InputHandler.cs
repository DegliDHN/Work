using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobileNavigation_InputHandler : MonoBehaviour
{
    public UnityEvent onBack;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            onBack.Invoke();
        }
    }
}
