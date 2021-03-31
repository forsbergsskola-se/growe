using System.Collections;
using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine;
using WorldGrid;

namespace Tools {
    public class CuttingTool : MonoBehaviour {
        bool _toolSelected;

        void Start() {
            MessageBroker.Instance().SubscribeTo<CuttingToolSelectedMessage>(UpdateToolSelected);
            MessageBroker.Instance().SubscribeTo<CancelSelectedToolMessage>(UpdateToolSelected);
        }

        void OnDestroy() {
            MessageBroker.Instance().UnSubscribeFrom<CuttingToolSelectedMessage>(UpdateToolSelected);
            MessageBroker.Instance().UnSubscribeFrom<CancelSelectedToolMessage>(UpdateToolSelected);
        }

        void UpdateToolSelected(CuttingToolSelectedMessage message) {
            _toolSelected = true;
            StartCoroutine(CutPlant());
        }
        void UpdateToolSelected(CancelSelectedToolMessage message) {
            _toolSelected = false;
        }

        IEnumerator CutPlant() {
            
            while (_toolSelected) {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonUp(0)) {
                    var currentPlant = hit.collider.transform.GetComponent<GridPlant>();
                    
                    if (currentPlant != null) {
                        
                        if (currentPlant.plant.CurrentGrowthStage == ItemSO.GrowthStage.Growing ||
                            currentPlant.plant.CurrentGrowthStage == ItemSO.GrowthStage.Mature) {
                            //timesCut is plant survivability
                            var timesCut = currentPlant.plant.timesCut;

                            if (timesCut >= 4) {
                                Destroy(currentPlant.transform.gameObject);
                            }
                            else {
                                currentPlant.plant.CurrentGrowthStage = ItemSO.GrowthStage.Sapling;
                                currentPlant.plant.timesCut += 1;
                            }

                            _toolSelected = false;
                            ItemSO newCutting = Instantiate(currentPlant.plant);
                            newCutting.itemType = ItemSO.ItemType.Cutting;
                            newCutting.timesCut = 0;
                            newCutting.CurrentGrowthStage = ItemSO.GrowthStage.Cutting;
                            newCutting.icon = newCutting.cuttingIcon;
                            newCutting.survivability = 0;
                            newCutting.hasLifeTime = true;

                            Inventories.Instance.playerInventory.Add(newCutting);
                            ValueChangedFeedback.instance.ValueFeedbackAdd(1);
                            MessageBroker.Instance().Send(new ToolSelectedMessage(false));
                            yield break;
                        }
                    }
                }
                yield return null;
            }
        }
    }
}
