using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnEnable : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Animator>().SetTrigger("MoveBook");    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
