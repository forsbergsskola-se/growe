using UnityEngine;
using UnityEngine.UI;

namespace Inventory {
    public class ItemInfoData : MonoBehaviour
    {
        public Image itemIcon;
        public Text itemName, itemAmount, itemParameters, itemLore;
        public ItemData itemData;
        

        public void UpdateItemInfo()
        {
            var itemInfo = itemData.itemSo;
            itemName.text = itemInfo.name;
            itemIcon.sprite = itemInfo.icon;
            itemAmount.text = itemData.amount.ToString();
            itemParameters.text = $"{itemInfo.rarity} {itemInfo.itemType}";
            itemLore.text = itemInfo.itemLore;
        }
    }
}
