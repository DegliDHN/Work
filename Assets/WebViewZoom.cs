using UnityEngine;

public class WebViewZoom : MonoBehaviour
{
    public bool enableZoom;

    void Start()
    {
        GetComponent<UniWebView>().SetZoomEnabled(enableZoom);
    }

}
