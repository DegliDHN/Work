using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveDelayed : MonoBehaviour
{
    public float delay;

    public void SetActive(GameObject go)
    {
        StartCoroutine(SetActive_Coro(go));
    }

    IEnumerator SetActive_Coro(GameObject go){
        yield return new WaitForSeconds(delay);
        go.SetActive(true);
    }
}
