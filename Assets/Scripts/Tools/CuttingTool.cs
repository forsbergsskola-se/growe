using System;
using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine;
using Random = UnityEngine.Random;

public class CuttingTool : MonoBehaviour {
    public bool isCutting;
    [SerializeField] public int chanceOfDeath;

    private void Start()
    {
        MessageBroker.Instance().SubscribeTo<CuttingToolSelectedMessage>(SetBool);
    }

    void SetBool(CuttingToolSelectedMessage message)
    {
        isCutting = message.setBool;
        Debug.Log((isCutting));
    }

    void Update() {
        //if (Input.touchSupported)
          //  MobileCut();
        //else
            PcCut();
    }

    void PcCut() {
        if (!isCutting) return;
        if (!Input.GetMouseButton(0)) return;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var chance = chanceOfDeath / 100f;
        
        if (Physics.Raycast(ray, out var hit)){
            //loop currently gets here
            if (hit.collider.transform.GetChild(2).GetComponent<GridPlant>() != null) {
                var currentPlant = hit.collider.transform.GetChild(2).GetComponent<GridPlant>();
                Debug.Log((currentPlant.plant.CurrentGrowthStage));
                //if (currentPlant.plant.growthStage <= 3) return;
                if (currentPlant.plant.CurrentGrowthStage == ItemSO.GrowthStage.Growing || 
                    currentPlant.plant.CurrentGrowthStage == ItemSO.GrowthStage.Mature)
                {
                    var timesCut = currentPlant.plant.timesCut;
                    var canDie = currentPlant.plant.survivability * chance;
                    Debug.Log(currentPlant.plant.survivability);
                    Debug.Log(canDie);

                    var range = Random.Range(0, canDie);
                    Debug.Log(range);
                    if (range <= canDie || timesCut >= 4) {
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
    }

    void MobileCut() {
        throw new NotImplementedException();   
    }
}
