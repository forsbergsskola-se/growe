using UnityEngine;

namespace InventoryAndStore
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
            ItemSO itemSO = ItemInfoData.itemData.ItemSO;
            
            if ((itemSO.itemType == ItemSO.ItemType.Plant && itemSO.tradeState != ItemSO.TradeState.Buyable) ||
                (itemSO.itemType == ItemSO.ItemType.Seed && itemSO.tradeState != ItemSO.TradeState.Buyable)) // TODO change to pot with something planted
            {
                plantSpawner.SpawnPlant(itemSO,ItemInfoData.playerInventory);
                ItemInfoData.playerInventory.Remove(itemSO);
                
                inventoryUI.SetActive(false);
                testingCanvasUI.SetActive(false);
                this.gameObject.SetActive(false);
                // TODO remove item from inventory
                // TODO place back in inventory if not placed by player
            }
            
            if (itemSO.itemType == ItemSO.ItemType.Seedbag && itemSO.tradeState != ItemSO.TradeState.Buyable)
            {
                itemSO.seedbag.Open(3, Inventories.Instance.playerInventory);
                Inventories.Instance.playerInventory.Remove(itemSO);
                ItemInfoData.gameObject.SetActive(false);
            }
            else if (itemSO.tradeState == ItemSO.TradeState.Buyable)
            {
                ItemSO clone = Instantiate(itemSO);
                clone.tradeState = ItemSO.TradeState.Sellable;
                clone.name = itemSO.name;
                ItemInfoData.playerInventory.Add(clone);
                ItemInfoData.storeInventory.Remove(itemSO);
            }
            else if (itemSO.tradeState == ItemSO.TradeState.Sellable && ItemInfoData.storeUi.activeSelf)
            {
                StoreDatabase.instance.Sell(itemSO);
            }
        }
    }
}
