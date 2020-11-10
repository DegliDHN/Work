using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentReparent : MonoBehaviour
{

    private Transform parent;
    
    public void Uparent(){
        this.parent = this.transform.parent;
        this.transform.parent = null;
        this.transform.SetParent(null, false);
    }

    public void Reparent(){
        if(this.parent != null){
            this.transform.SetParent(parent, false);
        }
    }

    
}
