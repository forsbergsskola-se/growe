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
            itemRarity.value = (int) itemSO.rarity + 1;
            itemAmount.text = itemData.amount.ToString();
            itemSize.text = $"{itemSO.sizeDimensions.x} x {itemSO.sizeDimensions.y}";
            itemSurvivability.text = itemSO.survivability.ToString();
            itemCompostValue.text = itemSO.compostValue.ToString();
            itemSellValue.text = itemSO.sellValue.ToString();
            itemLore.text = itemSO.itemLore;
            
            plantButtonText.text = itemSO.itemType == ItemSO.ItemType.Seedbag ? "Open" : "Plant";
            
            //TODO Try to move
            if (itemSO.tradeState == ItemSO.TradeState.Buyable) plantButtonText.text = "Buy";
            else if (itemSO.tradeState == ItemSO.TradeState.Sellable && storeUi.activeSelf) plantButtonText.text = "Sell";
        }

        public void CompostPlant() {
            FindObjectOfType<Currency>().AddCompost(itemData.ItemSO.compostValue);
            Inventories.Instance.playerInventory.Remove(itemData.ItemSO);
        }
    }
}
