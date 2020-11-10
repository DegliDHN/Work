using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialFill : MonoBehaviour
{
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    public void Fill(float duration)
    {
        StartCoroutine(Fill_Coro(duration));
    }

    IEnumerator Fill_Coro(float duration){
        float startTime = Time.time;
        float t = 0;
        while(Time.time - startTime < duration){
            t = (Time.time-startTime)/duration;
            float value = Mathf.Lerp(0, 1, t);
            image.fillAmount = value;
            yield return null;
        }
        image.fillAmount = 1f;
    }
}
