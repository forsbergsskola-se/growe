using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemData : MonoBehaviour
    {
        public ItemSO itemSo;
        private GameObject ShowInfoScreen;
        public int amount;
        public Text amountText;

        private void Start()
        {
            amountText.text = amount.ToString();
        }

        public void ShowItemInfo()
        {
            var go = ItemInfoData.Instance;
            go.gameObject.SetActive(true);
            go.itemData = this;
            go.UpdateItemInfo();
        }
    }
}
