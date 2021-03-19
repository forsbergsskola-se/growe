using System;
using UnityEngine;

namespace Inventory_and_Store
{
    public class InfoBoxButton : MonoBehaviour
    {
        private ItemInfoData ItemInfoData => GetComponent<ItemInfoData>();
        private PlantSpawner plantSpawner;

        // UI references
        public GameObject inventoryUI;
        public GameObject testingCanvasUI;
        
        private void Start()
        {
            plantSpawner = FindObjectOfType<PlantSpawner>();
            if (plantSpawner == null)
                Debug.Log("plantSpawner not found", this);
        }

        public void ButtonInteract()
        {
            if (ItemInfoData.itemData.ItemInfo.ItemSo.itemType == ItemSO.ItemType.Plant ||
                ItemInfoData.itemData.ItemInfo.ItemSo.itemType == ItemSO.ItemType.Seed) // TODO change to pot with something planted
            {
                plantSpawner.SpawnPlant(ItemInfoData.itemData.ItemInfo.ItemSo);
                inventoryUI.SetActive(false);
                testingCanvasUI.SetActive(false);
                this.gameObject.SetActive(false);
                // TODO remove item from inventory
                // TODO place back in inventory if not placed by player
            }
                

            Item itemInfo = ItemInfoData.itemData.ItemInfo;
            
            if (itemInfo.ItemSo.itemType == ItemSO.ItemType.Seedbag && itemInfo.ItemSo.tradeState != ItemSO.TradeState.Buyable)
            {
                itemInfo.ItemSo.Seedbag.Open(amount: 3, inventory: ItemInfoData.playerInventory);
                ItemInfoData.playerInventory.Remove(ItemInfoData.itemData.ItemInfo);
                ItemInfoData.gameObject.SetActive(false);
            }
            else if (itemInfo.ItemSo.tradeState == ItemSO.TradeState.Buyable)
            {
                ItemSO clone = Instantiate(itemInfo.ItemSo);
                clone.tradeState = ItemSO.TradeState.Sellable;
                clone.name = itemInfo.ItemSo.name;
                ItemInfoData.playerInventory.Add(clone);
                ItemInfoData.storeInventory.Remove(itemInfo);
            }
        }
    }
}
