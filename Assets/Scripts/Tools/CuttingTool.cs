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
            if (hit.collider.transform.GetChild(2).GetComponent<GridItem>() != null) {
                var currentPlant = hit.collider.transform.GetChild(2).GetComponent<GridItem>();
                if (currentPlant.item.growthStage <= 3) return;
                
                var timesCut = currentPlant.item.timesCut;

                var shouldDie = Random.Range(0f, 1f);
                if (shouldDie >= currentPlant.item.survivability || timesCut >= 4) {
                    Destroy(currentPlant.transform.parent.gameObject);
                }
                else {
                    currentPlant.item.growthStage = 3;
                    timesCut += 1;
                }

                //TODO: Add cuttings to inventory here.
                isCutting = false;
                ItemSO newCutting = Instantiate(currentPlant.item);
                newCutting.itemType = ItemSO.ItemType.Cutting;
                newCutting.timesCut = 0;
                newCutting.growthStage = 0;
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
