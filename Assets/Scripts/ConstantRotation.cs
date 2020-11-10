using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{

    public Vector3 constantRotation;
    void Update()
    {
        this.transform.Rotate(constantRotation);
    }
}
