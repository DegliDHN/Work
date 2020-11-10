using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onClick_ispector;
	public Action onClick;

	public void OnPointerClick(PointerEventData eventData)
	{
		onClick?.Invoke();
		onClick_ispector?.Invoke();
	}
}
