using System.Linq;
using UnityEngine;

namespace InventoryAndStore
{
    public class StoreDatabase : MonoBehaviour
    {
        public static StoreDatabase Instance;

        private void Start()
        {
            Instance = this;
        }

        private static Currency Currency => FindObjectOfType<Currency>();
        public void Sell(ItemSO itemSo)
        {
            Currency.AddSoftCurrency(itemSo.sellValue);
            Inventories.Instance.storeInventory.Remove(itemSo);
            foreach (ItemSO item in Inventories.Instance.playerInventory.items.Where(item => item.name == itemSo.name)) {
                Inventories.Instance.playerInventory.Remove(item);
                return;
            }
        }

        public void Buy(ItemSO itemSo)
        {
            if (!Currency.TryRemoveSoftCurrency(itemSo.buyValue)) 
                return;
            ItemSO clone = Instantiate(itemSo);
            clone.tradeState = ItemSO.TradeState.Sellable;
            clone.name = itemSo.name;

            clone.isNew = true;
            
            Inventories.Instance.playerInventory.Add(clone);
            //Inventories.Instance.storeInventory.Remove(itemSo);
        }
    }
}
