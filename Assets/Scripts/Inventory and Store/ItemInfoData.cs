using UnityEngine;
using UnityEngine.UI;

namespace Inventory_and_Store {
    public class ItemInfoData : MonoBehaviour
    {
        public Inventory inventory;
        public Seedbag Seedbag;
        public Image itemIcon;

        public Text itemName,
            itemAmount,
            itemRarity,
            itemSize,
            itemSurvivability,
            itemCompostValue,
            itemSellValue,
            itemLore,
            plantButtonText;

        public Button PlantButton;
        public ItemData itemData;

        public void UpdateItemInfo()
        {
            var itemInfo = itemData.ItemInfo.ItemSo;
            itemName.text = itemInfo.name;
            itemIcon.sprite = itemInfo.icon;
            itemRarity.text = itemInfo.rarity.ToString();
            itemAmount.text = itemData.amount.ToString();
            itemSize.text = $"{itemInfo.sizeDimensions.x} x {itemInfo.sizeDimensions.y}";
            itemSurvivability.text = itemInfo.survivability.ToString();
            itemCompostValue.text = itemInfo.compostValue.ToString();
            itemSellValue.text = itemInfo.sellValue.ToString();

            plantButtonText.text = itemData.ItemInfo.ItemSo.itemType == ItemSO.ItemType.Seedbag ? "Open" : "Plant";

            itemLore.text = itemInfo.itemLore;
        }
    }
}
