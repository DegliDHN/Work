using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public ButtonHandler handler_bottone1;
    public ButtonHandler handler_bottone2;
    public ButtonHandler handler_bottone3;
    public ButtonHandler handler_bottone4;
    public ButtonHandler handler_bottone5;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject arCamera;
    public Button exitAr;

    private void Start()
    {
        exitAr.onClick.AddListener(ExitAR);
    }
    private void Update()
    {
        
    }
    public void CanvasArManager(string markerId)
    {
        if (markerId == "copertina_nardini")
        {
            button3.SetActive(true);
            button4.SetActive(true);
            button5.SetActive(true);
            handler_bottone3.url = "https://www.youtube.com/watch?v=2XV0Z0rpXmk";
            handler_bottone4.url = "https://www.youtube.com/watch?v=x30MekBDHFM";
            handler_bottone5.url = "https://www.youtube.com/watch?v=cUQHJVwILRM";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker1_nardini")
        {
            button1.SetActive(true);
            handler_bottone1.url = "https://www.youtube.com/watch?v=HN5bfLXF5Qs";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker2_nardini")
        {
            button1.SetActive(true);
            handler_bottone1.url = "https://www.youtube.com/watch?v=3Tlw-7j_FL4";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker3_nardini")
        {
            button1.SetActive(true);
            handler_bottone1.url = "https://www.youtube.com/watch?v=etIdVBjiHEI";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker4_nardini")
        {
            button2.SetActive(true);
            handler_bottone2.url = "https://p3d.in/wxFl9/shadeless";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker5_nardini")
        {
            button1.SetActive(true);
            handler_bottone1.url = "https://www.youtube.com/watch?v=L-k-Guh755I";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker6_nardini")
        {
            button1.SetActive(true);
            handler_bottone1.url = "https://www.youtube.com/watch?v=-h-NXpx9RQQ";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker7_nardini")
        {
            button1.SetActive(true);
            handler_bottone1.url = "https://www.youtube.com/watch?v=10pcAMka1F8";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker8_nardini")
        {
            button1.SetActive(true);
            handler_bottone1.url = "https://www.youtube.com/watch?v=LyhYJ3Y38Ew";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker9_nardini")
        {
            button1.SetActive(true);
            handler_bottone1.url = "https://www.youtube.com/watch?v=nn7MeVoeq78";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker10_nardini")
        {
            button1.SetActive(true);
            handler_bottone1.url = "https://www.youtube.com/watch?v=bYMQiHEGlP0";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker11_nardini")
        {
            button2.SetActive(true);
            handler_bottone2.url = "https://p3d.in/Ica8s";
            arCamera.SetActive(false);
        }
        else if (markerId == "marker12_nardini")
        {
            button1.SetActive(true);
            handler_bottone1.url = "https://www.youtube.com/watch?v=MKY0jgqoYow";
            arCamera.SetActive(false);
        }
    }

    void ExitAR()
    {
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
        button5.SetActive(false);
        arCamera.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
