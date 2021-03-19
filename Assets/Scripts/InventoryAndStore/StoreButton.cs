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
                    Buy();
                    break;
                case ItemSO.TradeState.Sellable:
                    Sell();
                    break;
            }
        }

        private void Sell()
        {
            Inventories.Instance.storeInventory.Remove(ItemData.ItemSO);
            foreach (ItemSO itemSO in Inventories.Instance.playerInventory.items.Where(itemSO => itemSO.name == ItemData.ItemSO.name)) {
                Inventories.Instance.playerInventory.Remove(itemSO);
                return;
            }
        }

        private void Buy()
        {
            ItemSO clone = Instantiate(ItemData.ItemSO);
            clone.tradeState = ItemSO.TradeState.Sellable;
            clone.name = ItemData.ItemSO.name;
            Inventories.Instance.playerInventory.Add(clone);
            Inventories.Instance.storeInventory.Remove(ItemData.ItemSO);
        }
    
    }
}
