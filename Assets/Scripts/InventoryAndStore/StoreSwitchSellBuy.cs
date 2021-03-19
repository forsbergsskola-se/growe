using System.Collections;
using System.Collections.Generic;
using InventoryAndStore;
using UnityEngine;

public class StoreSwitchSellBuy : MonoBehaviour
{
    private List<ItemSO> SellableList = new List<ItemSO>();
    private List<Item> StoreItems = new List<Item>();

    private bool StoreSwitch;

    void FindSellables()
    {
        foreach (var item in Inventories.instance.playerInventory.Items)
        {
            if (item.ItemSo.tradeState == ItemSO.TradeState.Sellable)
            {
                SellableList.Add(item.ItemSo);
            }
        }
    }

    public void OnSwitchButton()
    {
        StoreSwitch = !StoreSwitch;
        if (StoreSwitch) SwitchToSell();
        else if (!StoreSwitch) SwitchToBuy();
    }

    void SwitchToSell()
    {
        FindSellables();
        foreach (var x in Inventories.instance.storeInventory.Items)
        {
            StoreItems.Add(x);
        }
        Inventories.instance.storeInventory.Items.Clear();
        foreach (var itemSo in SellableList)
        {
            Inventories.instance.storeInventory.Add(itemSo);
        }
        SellableList.Clear();
    }

    void SwitchToBuy()
    {
        Inventories.instance.storeInventory.Items.Clear();
        foreach (var item in StoreItems)
        {
            Inventories.instance.storeInventory.Add(item.ItemSo);
        }
        StoreItems.Clear();
    }
}
