using UnityEngine;

namespace InventoryAndStore
{
    public class InfoBoxButton : MonoBehaviour
    {
        private ItemInfoData ItemInfoData => GetComponent<ItemInfoData>();
        private PlantSpawner _plantSpawner;
        
        // UI references
        public GameObject inventoryUI;
        public GameObject testingCanvasUI;
        
        private void Start()
        {
            _plantSpawner = FindObjectOfType<PlantSpawner>();
            if (_plantSpawner == null)
                Debug.Log("plantSpawner not found", this);
        }

        public void ButtonInteract()
        {
            ItemSO itemSO = ItemInfoData.itemData.ItemSO;
            
            if ((itemSO.itemType == ItemSO.ItemType.Plant && itemSO.tradeState != ItemSO.TradeState.Buyable) ||
                (itemSO.itemType == ItemSO.ItemType.Seed && itemSO.tradeState != ItemSO.TradeState.Buyable)) // TODO change to pot with something planted
            {
                _plantSpawner.SpawnPlant(itemSO,ItemInfoData.playerInventory);
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
            else switch (itemSO.tradeState)
            {
                case ItemSO.TradeState.Buyable:
                {
                    ItemSO clone = Instantiate(itemSO);
                    clone.tradeState = ItemSO.TradeState.Sellable;
                    clone.name = itemSO.name;
                    ItemInfoData.playerInventory.Add(clone);
                    ItemInfoData.storeInventory.Remove(itemSO);
                    break;
                }
                case ItemSO.TradeState.Sellable when ItemInfoData.storeUi.activeSelf:
                    StoreDatabase.instance.Sell(itemSO);
                    break;
            }
        }
    }
}
