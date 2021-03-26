using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryAndStore
{
    public class StoreSwitchSellBuy : MonoBehaviour
    {
        private List<ItemSO> _sellableList = new List<ItemSO>();
        private List<ItemSO> _storeItems = new List<ItemSO>();

        private bool _storeSwitch;

        private List<ItemSO> GetSellables()
        {
            _sellableList.Clear();
            foreach (ItemSO itemSO in Inventories.Instance.playerInventory.items.Where(itemSO => itemSO.tradeState == ItemSO.TradeState.Sellable)) 
                _sellableList.Add(itemSO);
            return _sellableList;
        }

        public void OnSwitchButton() {
            _storeSwitch = !_storeSwitch;
            if (_storeSwitch) SwitchToSell();
            else if (!_storeSwitch) SwitchToBuy();
        }

        private void SwitchToSell()
        {
            _storeItems = Inventories.Instance.storeInventory.items;
            Inventories.Instance.storeInventory.items.Clear();
            Inventories.Instance.storeInventory.Add(GetSellables());
            foreach (var x in _storeItems)
            {
                Debug.Log("sell" + x.name);
            }
        }

        private void SwitchToBuy()
        {
            Inventories.Instance.storeInventory.items.Clear();
            Inventories.Instance.storeInventory.Add(_storeItems);
            
            foreach (var x in _storeItems)
            {
                Debug.Log("buy" + x.name);
            }
            _storeItems.Clear();
        }
    }
}
