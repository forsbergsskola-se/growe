using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CancelAction : MonoBehaviour, IPointerDownHandler {
    bool isCutting;

    public Image _image;
    
    void Awake() {
        isCutting = GetComponent<CuttingTool>().isCutting;
        _image = GetComponent<Image>();
    }

    void Update() {
        //TODO: Add other tools here, as booleans.
        if (!isCutting) return;
        _image.color = Color.red;
    }

    public void OnPointerDown(PointerEventData eventData) {
        //TODO: Add other tools here, as booleans.
        if (!isCutting) return;

        foreach (var go in eventData.hovered) {
            
        }
    }
}
