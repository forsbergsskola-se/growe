using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryAndStore
{
    public class ItemData : MonoBehaviour
    {
        public ItemSO ItemSO;
        public int amount;
        public Text amountText;
        public ItemInfoData itemInfoData;
        private Image Icon => GetComponent<Image>();
        public Image indicatorDot;

        private void Start()
        {
            itemInfoData = UIReferences.Instance.itemInfoBox;
            Icon.sprite = ItemSO.icon;
            amountText.text = amount.ToString();
            if (ItemSO.isNew) indicatorDot.gameObject.SetActive(true);
        }

        public void ShowItemInfo()
        {
            itemInfoData.gameObject.SetActive(true);
            itemInfoData.itemData = this;
            itemInfoData.UpdateItemInfo();
        }

        public void DisableIndicator()
        {
            indicatorDot.gameObject.SetActive(false);
            ItemSO.isNew = false;
        }

        private void OnDisable()
        {
            DisableIndicator();
        }
    }
}
