using System.Collections;
using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine;
using WorldGrid;

namespace Tools.WateringTool
{
    public class WateringToolSelected : MonoBehaviour {
        private bool toolSelected;
        private MoveMetronome MoveMetronome => GetComponent<MoveMetronome>();

        private void Start() {
           
            MessageBroker.Instance().SubscribeTo<WateringToolSelectedMessage>(UpdateToolSelected);
            MessageBroker.Instance().SubscribeTo<CancelSelectedToolMessage>(UpdateToolSelected);
        }
        
        void UpdateToolSelected(WateringToolSelectedMessage m) {
            toolSelected = true;
            StartCoroutine(WaterThePlant());
           
        }
        
        void UpdateToolSelected(CancelSelectedToolMessage m) {
            toolSelected = false;
        }
        
        


        private IEnumerator WaterThePlant() {
            
            while(toolSelected){
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonDown(0))
                {
                    var currentPlant = hit.collider.transform.GetComponent<GridPlant>();
                    if (currentPlant != null)
                    {
                        MoveMetronome.UISetActive(true);
                        MoveMetronome.selectedPlant = currentPlant;
                        MoveMetronome.currentAngle = Random.Range(MoveMetronome.rotateAt - MoveMetronome.rotateAt * 2, MoveMetronome.rotateAt);
                        MoveMetronome.speed = (int)currentPlant.plant.rarity;
                        MoveMetronome.StartCoroutine(MoveMetronome.Rotate());
                        yield break;
                    }
                }
                yield return null; 
            }
        }
    }
}
