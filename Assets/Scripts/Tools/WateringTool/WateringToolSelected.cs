using System.Collections;
using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine;

namespace Tools.WateringTool
{
    public class WateringToolSelected : MonoBehaviour
    {
        private MoveMetronome MoveMetronome => GetComponent<MoveMetronome>();

        private void Start()
        {
            MessageBroker.Instance().SubscribeTo<WateringToolSelectedMessage>(UpdateToolSelected);
        }
        
        void UpdateToolSelected(WateringToolSelectedMessage m) {
            StartCoroutine(WaterThePlant());
        }


        private IEnumerator WaterThePlant()
        {
            while(true){
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonDown(0))
                {
                    var currentPlant = hit.collider.transform.GetChild(2).GetComponent<GridPlant>();
                    if (currentPlant != null)
                    {
                        MoveMetronome.UISetActive(true);
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
