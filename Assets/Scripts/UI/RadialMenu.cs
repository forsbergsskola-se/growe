using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [Header("Settings")]
    [SerializeField, Range(0.0f, 10f), Tooltip("How long the button must be held until the menu shows up")] float waitTime = 1f;
    
    //variables
    private bool menuButtonHeldDown = false;
    private float endTime;
    private bool RadialWheelActive => radialWheel.activeSelf;
    
    //references
    public GameObject radialWheel;
    private EventSystem eventSystem;
    
    private void OnEnable()
    {
        eventSystem = GetComponent<EventSystem>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        menuButtonHeldDown = true;
        endTime = Time.time + waitTime;
        Debug.Log("pointer down on radial menu");
        StartCoroutine(ButtonHeldRoutine());
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer up");
        menuButtonHeldDown = false;

        if (RadialWheelActive)
        {
            CheckIfToolIsHovered();
            radialWheel.SetActive(false);
        }
    }

    IEnumerator ButtonHeldRoutine()
    {
        while (Time.time < endTime) {
            if (!menuButtonHeldDown)
                yield break;
            yield return null;
        }
        radialWheel.SetActive(true);
    }

    private void CheckIfToolIsHovered()
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;
             
        GraphicRaycaster graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);
        
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
        }
    }
    


}
