using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    public Transform targetPosition9_14, targetPosition9_19;
    private Vector3 currentVelocity;
    public float animTime;
    public float tolerance;
    public float rotationSpeed;
    public GameObject activateOnAnimationEnd;
    public CanvasGroup fadeOutCanvas;

    private bool moving;
    private float startTime;
    private Quaternion startRotation;
    private bool fadeOutBackground;
    private bool visible = false;
    private float fadeOutstartTime;

    private Vector3 startPosition;
    private Transform startParent = null;

    void Awake()
    {
        if(startParent == null){
            startParent = this.transform.parent;
            startPosition = this.transform.position;
            startRotation = this.transform.rotation;
        } 
        // Move();
    }

    // Start is called before the first frame update
    [ContextMenu("Move")]
    public void Move()
    {
        if(this.visible == false){
            this.visible = true;
        } else {
            //Don't restart animation if we have already done that
            return;
        }

        Debug.Log("Move");
        this.transform.parent = null;
        moving = true;
        startTime = Time.time;
    }

    [ContextMenu("Reset")]
    public void Reset(){
        Debug.Log("Reset, startParent is "+startParent);
        this.transform.position = startPosition;
        this.transform.rotation = startRotation;
        this.transform.parent = startParent;
        this.moving = false;
        this.fadeOutBackground = false;
        this.visible = false;
        fadeOutCanvas.alpha = 1f;
        activateOnAnimationEnd.SetActive(false);
        this.gameObject.SetActive(false);    
    }

    Vector3 ComputeTargetPosition(){
        Vector3 position = targetPosition9_19.position;
        
        var t = Mathf.InverseLerp(9f/14f, 9f/19f, ComputeDeviceAspectRatio());
        position.z = Mathf.Lerp(targetPosition9_14.position.z, targetPosition9_19.position.z, t);

        // position.z = (ComputeDeviceAspectRatio() * targetPosition9_19.position.z) / (9/19f);

        return position;
    }

    float ComputeDeviceAspectRatio(){
        return Screen.width / (float) Screen.height;
    }

    void Update(){
        if(moving){
            float timeSinceBeginMoving = Time.time - startTime;
            var targetPosition = ComputeTargetPosition();

            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref currentVelocity, animTime - timeSinceBeginMoving);
            
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetPosition9_19.rotation, rotationSpeed);
            
            if(Vector3.Distance(this.transform.position, targetPosition) < tolerance){
                moving = false;
                activateOnAnimationEnd.SetActive(true);
                fadeOutBackground = true;
                fadeOutstartTime = Time.time;
            }
        }
        if(fadeOutBackground){
            if(fadeOutCanvas.alpha <= 0.2f){
                fadeOutCanvas.alpha = 0.2f;
                fadeOutBackground = false;
            }
            var t = (Time.time - fadeOutstartTime) / 3;
            fadeOutCanvas.alpha = Mathf.Lerp(1f, 0.2f,  t);

        }
    }

}
