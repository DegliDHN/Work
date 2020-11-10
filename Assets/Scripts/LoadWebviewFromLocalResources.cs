using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWebviewFromLocalResources : MonoBehaviour
{
    [Tooltip("The file path relative to the Assets/StreamingAssets folder")]
    public string filePath;
    public bool loadOnStart;
    public bool showOnStart;

    void Start()
    {   
        if(loadOnStart){
            LoadFromStreamingAssets(filePath);
        }
    }

    public void LoadFromStreamingAssets(string path){
        var url = UniWebViewHelper.StreamingAssetURLForPath(filePath);
        GetComponent<UniWebView>().Load(url);
    }
}
