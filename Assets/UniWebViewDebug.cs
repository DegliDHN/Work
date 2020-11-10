using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniWebViewDebug : MonoBehaviour
{
    public UniWebViewLogger.Level logLevel = UniWebViewLogger.Level.Critical;
    void Start()
    {
        UniWebViewLogger.Instance.LogLevel = logLevel;
    }

}
