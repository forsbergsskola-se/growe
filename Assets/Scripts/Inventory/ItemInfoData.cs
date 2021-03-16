using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory {
    public class ItemInfoData : MonoBehaviour
    {
        public static ItemInfoData Instance;
        public Image itemIcon;
        public Text itemName, itemAmount, itemParameters, itemLore;
        public ItemData itemData;
        private void Start()
        {
            Instance = this;
            Instance.gameObject.SetActive(false);
        }

        public void UpdateItemInfo()
        {
            var itemInfo = itemData.itemSo;
            itemName.text = itemInfo.name;
            itemIcon.sprite = itemInfo.icon;
            itemAmount.text = itemData.amount.ToString();
            itemParameters.text = itemInfo.rarity + " " + itemInfo.itemType;
            itemLore.text = itemInfo.itemLore;
        }
    }
}
