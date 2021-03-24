using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryAndStore
{
    public class StoreButton : MonoBehaviour
    {
        private ItemData ItemData => GetComponentInParent<ItemData>();
        
        private Text Text => GetComponentInChildren<Text>();

        private void OnEnable()
        {
            StartCoroutine(UpdateText());
        }

        private IEnumerator UpdateText()
        {
            while (ItemData.ItemSO is null) yield return null;
            Text.text = ItemData.ItemSO.tradeState switch
            {
                ItemSO.TradeState.Buyable => ItemData.ItemSO.buyValue + " Buy",
                ItemSO.TradeState.Sellable => ItemData.ItemSO.sellValue + " Sell",
                _ => Text.text
            };
        }

        public void ButtonPressed()
        {
            switch (ItemData.ItemSO.tradeState)
            {
                case ItemSO.TradeState.Buyable:
                    StoreDatabase.instance.Buy(ItemData.ItemSO);
                    break;
                case ItemSO.TradeState.Sellable:
                    StoreDatabase.instance.Sell(ItemData.ItemSO);
                    break;
            }
        }
    }
}
