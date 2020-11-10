using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCloseWithBackBtn : MonoBehaviour
{
    void Start()
    {
        GetComponent<UniWebView>().OnShouldClose += (_) => false;
    }
}
