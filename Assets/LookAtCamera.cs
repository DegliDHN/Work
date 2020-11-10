using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
		var tmp = transform.eulerAngles;
        tmp.y = Camera.main.transform.eulerAngles.y;
		transform.eulerAngles = tmp;
    }
}
