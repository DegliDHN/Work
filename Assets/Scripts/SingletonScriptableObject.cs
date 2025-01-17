﻿using System.Linq;
using UnityEngine;

/// <summary>
/// Abstract class for making reload-proof singletons out of ScriptableObjects
/// Returns the asset created on editor, null if there is none
/// Based on https://www.youtube.com/watch?v=VBA1QCoEAX4
/// </summary>
/// <typeparam name="T">Type of the singleton</typeparam>

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject {
    static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance != null){
                _instance = Resources.Load<T>("") as T;
                if(_instance == null){
                    Debug.Log("Couldn't find Instance of "+typeof(T));
                }
            }
            return _instance;
        }
    }
}