using UnityEngine;
using UnityEngine.UI;

namespace Inventory {
    public class ItemInfoData : MonoBehaviour
    {
        public Image itemIcon;
        public Text itemName, itemAmount, itemRarity, itemSize, itemSurvivability, itemCompostValue, itemSellValue, itemLore;
        public ItemData itemData;
        

        public void UpdateItemInfo()
        {
            var itemInfo = itemData.itemSo;
            itemName.text = itemInfo.name;
            itemIcon.sprite = itemInfo.icon;
            itemRarity.text = itemInfo.rarity.ToString();
            itemAmount.text = itemData.amount.ToString();
            itemSize.text = $"{itemInfo.sizeDimensions.x} x {itemInfo.sizeDimensions.y}";
            itemSurvivability.text = itemInfo.survivability.ToString();
            itemCompostValue.text = itemInfo.compostValue.ToString();
            itemSellValue.text = itemInfo.sellValue.ToString();

            itemLore.text = itemInfo.itemLore;
        }
    }
}
