using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Broker;
using Broker.Messages;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI {
    public class RadialMenu : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [Header("Settings")]
        [SerializeField, Range(0.0f, 10f), Tooltip("How long the button must be held until the menu shows up")] float waitTime = 1f;
    
        //variables
        private bool menuButtonHeldDown = false;
        private float endTime;
        private bool RadialWheelActive => radialWheel.activeSelf;
        private bool toolSelected;
        private Image buttonImage;

        void FixedUpdate() {
            buttonImage.color = toolSelected ? Color.red : Color.white;
        }

        //references
        public GameObject radialWheel;
        private EventSystem eventSystem;
    
        private void OnEnable() {
            buttonImage = GetComponent<Image>();
            eventSystem = GetComponent<EventSystem>();
            MessageBroker.Instance().SubscribeTo<ToolSelectedMessage>(UpdateToolSelected);
        }

        void OnDisable() {
            MessageBroker.Instance().UnSubscribeFrom<ToolSelectedMessage>(UpdateToolSelected);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (toolSelected) 
                return;

            menuButtonHeldDown = true;
            endTime = Time.time + waitTime;
            StartCoroutine(ButtonHeldRoutine());
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (toolSelected) {
                Debug.Log("Tool Selected, sending Cancel message");
                MessageBroker.Instance().Send(new CancelSelectedToolMessage());
                toolSelected = false;
                return;
            }
            
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
        
        void UpdateToolSelected(ToolSelectedMessage m) {
            toolSelected = m.toolSelected;
        }

        private void CheckIfToolIsHovered()
        {
            PointerEventData eventData = new PointerEventData(eventSystem);
            eventData.position = Input.mousePosition;
             
            GraphicRaycaster graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(eventData, results);

            if (results.Count == 0) return;
            var result = results[0];
            toolSelected = true;
                
            switch (result.gameObject.name) {
                case "CuttingTool":
                    MessageBroker.Instance().Send(new CuttingToolSelectedMessage());
                    break;
                case "WateringTool":
                    MessageBroker.Instance().Send(new WateringToolSelectedMessage());
                    break;
                case "FertilizerTool":
                    MessageBroker.Instance().Send(new FertilizerToolSelectedMessage());
                    break;
                default :
                    toolSelected = false;
                    break;
            }
            MessageBroker.Instance().Send(new ToolSelectedMessage(toolSelected));
            Debug.Log("ToolSelectedMessage: True");
        }
    }
}
