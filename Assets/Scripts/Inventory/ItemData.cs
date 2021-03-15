using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemData : MonoBehaviour
    {
        public ItemSO itemSo;
        public int amount;
        private Text AmountText => GetComponentInChildren<Text>();

        private void Start()
        {
            AmountText.text = amount.ToString();
        }
    }
}
