using System.Collections;
using Broker;
using Broker.Messages;
using UnityEngine;
using WorldGrid;

namespace Tools {
    public class FertilizerTool : MonoBehaviour {
        
        bool _toolSelected;

        void Start() {
            MessageBroker.Instance().SubscribeTo<FertilizerToolSelectedMessage>(UpdateToolSelected);
            MessageBroker.Instance().SubscribeTo<CancelSelectedToolMessage>(UpdateToolSelected);
        }

        void OnDestroy() {
            MessageBroker.Instance().UnSubscribeFrom<FertilizerToolSelectedMessage>(UpdateToolSelected);
            MessageBroker.Instance().UnSubscribeFrom<CancelSelectedToolMessage>(UpdateToolSelected);
        }

        void UpdateToolSelected(FertilizerToolSelectedMessage m) {
            _toolSelected = true;
            StartCoroutine(FertilizePlant());
        }
        
        void UpdateToolSelected(CancelSelectedToolMessage m) {
            _toolSelected = false;
        }

        IEnumerator FertilizePlant() {
            
            while(_toolSelected) {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonUp(0)) {
                    var currentPlant = hit.collider.transform.GetComponent<GridPlant>();
                    if (currentPlant != null) {
                        currentPlant.plant.isFertilized = true;
                        
                        MessageBroker.Instance().Send(new ToolSelectedMessage(false));
                        yield break;
                    }
                }
                yield return null; 
            }
        }
    }
}