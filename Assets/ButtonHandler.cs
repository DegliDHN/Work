using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button button;
    public string url;

    private void Start()
    {
        button.onClick.AddListener(OpenUrlClick);
    }

    void OpenUrlClick()
    {
        Application.OpenURL(url);
    }
}
