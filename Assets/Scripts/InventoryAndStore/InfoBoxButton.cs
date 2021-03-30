using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryAndStore
{
    public class InfoBoxButton : MonoBehaviour
    {
        private ItemInfoData ItemInfoData => GetComponent<ItemInfoData>();
        private PlantSpawner _plantSpawner;

        public Button fusionButtom;

        // UI references
        public GameObject inventoryUI;

        public GameObject gachaPopup;
        public SeedbagPopup seedbagPopup;

        private void Start()
        {
            _plantSpawner = FindObjectOfType<PlantSpawner>();
            if (_plantSpawner == null)
                Debug.Log("plantSpawner not found", this);
        }

        private void Update()
        {
            ItemTypeChecker();

            if (ItemInfoData.itemData.amount >= 3 && ItemInfoData.itemData.ItemSO.itemType == ItemSO.ItemType.Seed)
            {
                fusionButtom.interactable = true;
            }
            else
            {
                fusionButtom.interactable = false;
            }
        }

        public void fusionButtomFunction()
        {
            ItemSO clone = Instantiate(ItemInfoData.itemData.ItemSO);
            clone.name = "idk man looks kinda shiny";
            clone.isShiny = true;
            ItemInfoData.gameObject.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                ItemInfoData.playerInventory.Remove(ItemInfoData.itemData.ItemSO);
            }
            ItemInfoData.playerInventory.Add(clone);
        }

        public void ItemTypeChecker()
        {
            if (ItemInfoData.itemData.ItemSO.itemType == ItemSO.ItemType.Seedbag)
            {
                fusionButtom.gameObject.SetActive(false);
                ItemInfoData.CompostButton.gameObject.SetActive(false);
            }
            else
            {
                fusionButtom.gameObject.SetActive(true);
                ItemInfoData.CompostButton.gameObject.SetActive(true);
            }
        }


        public void ButtonInteract()
        {
            ItemSO itemSO = ItemInfoData.itemData.ItemSO;

            if (!ItemInfoData.storeUi.activeSelf) {
                if (itemSO.itemType == ItemSO.ItemType.Plant && itemSO.tradeState != ItemSO.TradeState.Buyable ||
                    itemSO.itemType == ItemSO.ItemType.Seed && itemSO.tradeState != ItemSO.TradeState.Buyable ||
                    itemSO.itemType == ItemSO.ItemType.Cutting && itemSO.tradeState != ItemSO.TradeState.Buyable
                    )
                    // TODO change to pot with something planted
                {
                    _plantSpawner.SpawnPlant(itemSO, ItemInfoData.playerInventory);
                    ItemInfoData.playerInventory.Remove(itemSO);

                    inventoryUI.SetActive(false);
                    this.gameObject.SetActive(false);
                }
            }

            if (itemSO.itemType == ItemSO.ItemType.Seedbag && itemSO.tradeState != ItemSO.TradeState.Buyable)
            {
                ItemSO[] items = itemSO.seedbag.Open(3, Inventories.Instance.playerInventory);
                Inventories.Instance.playerInventory.Remove(itemSO);
                ItemInfoData.gameObject.SetActive(false);
                seedbagPopup.UpdateItemDisplay(items);
                gachaPopup.gameObject.SetActive(true);
            }
            else
                switch (itemSO.tradeState)
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