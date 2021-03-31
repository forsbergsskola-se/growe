using System;
using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine;
using WorldGrid;

public class CuttingTool : MonoBehaviour {
    public bool isCutting;
    LayerMask layerToHit;

    private void Start() {
        layerToHit = LayerMask.GetMask("IsometricGrid");
        MessageBroker.Instance().SubscribeTo<CuttingToolSelectedMessage>(SetBool);
        MessageBroker.Instance().SubscribeTo<CancelSelectedToolMessage>(SetBool);
    }

    void SetBool(CuttingToolSelectedMessage message)
    {
        isCutting = true;
        //Debug.Log((isCutting));
    }
    void SetBool(CancelSelectedToolMessage message) {
        isCutting = false;
  
    }

    void Update() {
        //if (Input.touchSupported)
          //  MobileCut();
        //else
            PcCut();
    }
    
    

    void PcCut() {
        if (!isCutting) return;
        if (!Input.GetMouseButtonUp(0)) return;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit)) {
            var currentPlant = hit.collider.transform.GetComponent<GridPlant>();
            if (currentPlant != null) {
                if (currentPlant.plant.CurrentGrowthStage == ItemSO.GrowthStage.Growing || 
                    currentPlant.plant.CurrentGrowthStage == ItemSO.GrowthStage.Mature)
                {
                    //timesCut is plant survivability
                    var timesCut = currentPlant.plant.timesCut;
                    
                    if (timesCut >= 4) {
                        Destroy(currentPlant.transform.gameObject);
                    }
                    else {
                        currentPlant.plant.CurrentGrowthStage = ItemSO.GrowthStage.Sapling;
                        currentPlant.plant.timesCut += 1;
                    }

                    isCutting = false;
                    ItemSO newCutting = Instantiate(currentPlant.plant);
                    newCutting.itemType = ItemSO.ItemType.Cutting;
                    newCutting.timesCut = 0;
                    newCutting.CurrentGrowthStage = ItemSO.GrowthStage.Cutting;
                    newCutting.icon = newCutting.cuttingIcon;
                    newCutting.survivability = 0;
                    newCutting.hasLifeTime = true;

                    //newCutting.Icon = cutting icon (?)
                
                    Inventories.Instance.playerInventory.Add(newCutting);
                    ValueChangedFeedback.instance.ValueFeedbackAdd(1);
                
                    // Debug.Log(timesCut);
                    // Debug.Log(currentPlant.item.growthStage);
                    // currentPlant.item.growthStage = 5;
                    MessageBroker.Instance().Send(new ToolSelectedMessage(false));
                }
            }
        }
    }

    void MobileCut() {
        throw new NotImplementedException();   
    }
}
