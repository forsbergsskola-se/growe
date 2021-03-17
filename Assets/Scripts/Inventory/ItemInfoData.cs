using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemInfoData : MonoBehaviour
    {
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
            var itemInfo = itemData.itemSo;
            itemName.text = itemInfo.name;
            itemIcon.sprite = itemInfo.icon;
            itemRarity.text = itemInfo.rarity.ToString();
            itemAmount.text = itemData.amount.ToString();
            itemSize.text = $"{itemInfo.sizeDimensions.x} x {itemInfo.sizeDimensions.y}";
            itemSurvivability.text = itemInfo.survivability.ToString();
            itemCompostValue.text = itemInfo.compostValue.ToString();
            itemSellValue.text = itemInfo.sellValue.ToString();

            if (itemData.itemSo.itemType == ItemSO.ItemType.Seedbag)
            {
                plantButtonText.text = "Open";
            }
            else
            {
                plantButtonText.text = "Plant";
            }

            itemLore.text = itemInfo.itemLore;
        }

        // Testing seedbag open function Adam A
        public void openSeedbag()
        {
            if (itemData.itemSo.itemType == ItemSO.ItemType.Seedbag)
            {
                itemData.itemSo.Seedbag.Open(amount: 3);
                
            }
        }
    }
}