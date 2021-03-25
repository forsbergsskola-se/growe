using System;
using InventoryAndStore;
using UnityEngine;
using Random = UnityEngine.Random;

public class CuttingTool : MonoBehaviour {
    public static bool isCutting;

    void Update() {
        if (Input.touchSupported)
            MobileCut();
        else
            PcCut();
    }

    void PcCut() {
        if (!isCutting) return;
        if (!Input.GetMouseButtonDown(0)) return;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out var hit)){
            if (hit.collider.transform.GetChild(2).GetComponent<GridPlant>() != null) {
                var currentPlant = hit.collider.transform.GetChild(2).GetComponent<GridPlant>();
                //if (currentPlant.plant.growthStage <= 3) return;
                if (currentPlant.plant.CurrentGrowthStage != ItemSO.GrowthStage.Growing ||
                    currentPlant.plant.CurrentGrowthStage != ItemSO.GrowthStage.Mature)
                    return;
                
                var timesCut = currentPlant.plant.timesCut;

                var shouldDie = Random.Range(0f, 1f);
                if (shouldDie >= currentPlant.plant.survivability || timesCut >= 4) {
                    Destroy(currentPlant.transform.parent.gameObject);
                }
                else {
                    //currentPlant.plant.growthStage = 3;
                    currentPlant.plant.CurrentGrowthStage = ItemSO.GrowthStage.Sapling;
                    currentPlant.plant.timesCut += 1;
                }

                //TODO: Add cuttings to inventory here.
                isCutting = false;
                ItemSO newCutting = Instantiate(currentPlant.plant);
                newCutting.itemType = ItemSO.ItemType.Cutting;
                newCutting.timesCut = 0;
                newCutting.CurrentGrowthStage = ItemSO.GrowthStage.Cutting;
                newCutting.survivability = 0;
                newCutting.hasLifeTime = true;

                //newCutting.Icon = cutting icon (?)
                
                Inventories.Instance.playerInventory.Add(newCutting);
                
                // Debug.Log(timesCut);
                // Debug.Log(currentPlant.item.growthStage);
                // currentPlant.item.growthStage = 5;
            }
        }
    }

    void MobileCut() {
        throw new NotImplementedException();   
    }
}
