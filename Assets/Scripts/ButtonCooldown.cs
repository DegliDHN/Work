using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{
    public float cooldown = 0.3f;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=> ApplyCooldown(this.cooldown));
    }

    public void ApplyCooldown(float customCooldown){
        StartCoroutine(ApplyCooldown_Coro(customCooldown));
    }
    IEnumerator ApplyCooldown_Coro(float customCooldown){
        yield return new WaitForEndOfFrame();
        button.interactable = false;
        yield return new WaitForSeconds(customCooldown);
        button.interactable = true;
    }
    
}
