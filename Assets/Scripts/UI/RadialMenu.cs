using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,  IEndDragHandler, IPointerUpHandler
{
    public float waitTime = 1f;
    public Image radialWheel;
    private bool menuButtonHeldDown = false;
    private float endTime;
    
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        menuButtonHeldDown = true;
        endTime = Time.time + waitTime;
        Debug.Log("pointer down on radial menu");
        StartCoroutine(ButtonHeldRoutine());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        menuButtonHeldDown = false;
    }

    IEnumerator ButtonHeldRoutine()
    {
        while (Time.time < endTime) {
            if (!menuButtonHeldDown)
                yield break;
        }
        radialWheel.enabled = true;
    }
}
