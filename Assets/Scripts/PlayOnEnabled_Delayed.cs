using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayOnEnabled_Delayed : MonoBehaviour
{
    public float delay;

    
    void OnEnable()
    {
        StartCoroutine(Play_Coro());
    }

    IEnumerator Play_Coro()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<VideoPlayer>().Play();
    }
}
