using System;
using Gacha;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryAndStore {
    public class ItemInfoData : MonoBehaviour
    {
        public Inventory storeInventory, playerInventory;
        public Image itemIcon;
        public GameObject storeUi;

        public Text itemName,
            itemAmount,
            itemSize,
            itemSurvivability,
            itemCompostValue,
            itemSellValue,
            itemLore,
            plantButtonText;

        public Slider itemRarity;
        public Button plantButton;
        public Button CompostButton;
        [HideInInspector]
        public ItemData itemData;

        public void UpdateItemInfo()
        {
            ItemSO itemSO = itemData.ItemSO;
            itemName.text = itemSO.name;
            itemIcon.sprite = itemSO.icon;
            itemRarity.value = Array.IndexOf(Enum.GetValues(itemSO.rarity.GetType()), itemSO.rarity) + 1;
            itemAmount.text = itemData.amount.ToString();
            itemSize.text = $"{itemSO.sizeDimensions.x} x {itemSO.sizeDimensions.y}";
            itemSurvivability.text = itemSO.survivability.ToString();
            itemCompostValue.text = itemSO.compostValue.ToString();
            itemSellValue.text = itemSO.sellValue.ToString();
            itemLore.text = itemSO.itemLore;

            UpdateItemInfoButton(itemSO);
        }

        public void UpdateItemInfoButton(ItemSO itemSO)
        {
            plantButtonText.text = 
                itemSO.tradeState switch {
                    ItemSO.TradeState.Buyable => "Buy",
                    ItemSO.TradeState.Sellable when storeUi.activeSelf => "Sell",
                    _ => itemSO.itemType == ItemSO.ItemType.Seedbag ? "Open" : "Plant" 
            };
        }

        public void CompostPlant() {
            FindObjectOfType<Currency>().AddCompost(itemData.ItemSO.compostValue);
            Inventories.Instance.playerInventory.Remove(itemData.ItemSO);
        }
    }
}
