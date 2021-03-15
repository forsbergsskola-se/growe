using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemData : MonoBehaviour
    {
        public ItemSO itemSo;
        public int amount;
        public Text AmountText;
        public Text TypeText;

        private void Start()
        {
            AmountText.text = amount.ToString();
            TypeText.text = itemSo.itemType;
        }
    }
}
