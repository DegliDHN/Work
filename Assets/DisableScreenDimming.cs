using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScreenDimming : MonoBehaviour
{
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

}
