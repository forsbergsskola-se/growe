using System.Collections;
using System.Collections.Generic;
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
        bool toolSelected; 
    
        //references
        public GameObject radialWheel;
        private EventSystem eventSystem;
    
        private void OnEnable()
        {
            eventSystem = GetComponent<EventSystem>();
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
                toolSelected = true;
                
                switch (result.gameObject.name) {
                    case "CuttingTool":
                        MessageBroker.Instance().Send(new CuttingToolSelectedMessage());
                        MessageBroker.Instance().Send(new ToolSelectedMessage(toolSelected));
                        return;
                    case "WateringTool":
                        MessageBroker.Instance().Send(new WateringToolSelectedMessage());
                        MessageBroker.Instance().Send(new ToolSelectedMessage(toolSelected));
                        return;
                    case "FertilizerTool":
                        MessageBroker.Instance().Send(new FertilizerToolSelectedMessage());
                        MessageBroker.Instance().Send(new ToolSelectedMessage(toolSelected));
                        return;
                    default :
                        toolSelected = false;
                        MessageBroker.Instance().Send(new ToolSelectedMessage(toolSelected));
                        return;
                }
            }
            //TODO WHY DOES CODE NOT EXECUTE HERE??
        }
    }
}
