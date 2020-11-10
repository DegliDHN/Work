using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetBool : MonoBehaviour
{
    private Animator myAnimator;
    public string boolName;
    

    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();        
    }

    public void SetBool(bool value){
        this.myAnimator.SetBool(boolName, value);
    }

}
