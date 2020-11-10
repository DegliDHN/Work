using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebViewImmersiveMode : MonoBehaviour
{
    public bool immersiveModeEnabled;

    void Start()
    {
        GetComponent<UniWebView>().SetImmersiveModeEnabled(immersiveModeEnabled);
    }


}
