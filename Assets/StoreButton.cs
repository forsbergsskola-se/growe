using System;
using System.Collections;
using System.Collections.Generic;
using Inventory_and_Store;
using UnityEngine;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour
{
    private ItemData ItemData => GetComponentInParent<ItemData>();
    private Text Text => GetComponentInChildren<Text>();

    private void OnEnable()
    {
        Invoke(nameof(UpdateText), 0.1f);
    }

    private void UpdateText()
    {
        if (ItemData.ItemInfo.ItemSo.tradeState == ItemSO.TradeState.Buyable)
        {
            Text.text = ItemData.ItemInfo.ItemSo.buyValue + " Buy";
        }
        else if (ItemData.ItemInfo.ItemSo.tradeState == ItemSO.TradeState.Sellable)
        {
            Text.text = ItemData.ItemInfo.ItemSo.sellValue + " Sell";
        }
    }

    public void ButtonPressed()
    {
        if (ItemData.ItemInfo.ItemSo.tradeState == ItemSO.TradeState.Buyable)
        {
            Buy();
        }
        else if (ItemData.ItemInfo.ItemSo.tradeState == ItemSO.TradeState.Sellable)
        {
            Sell();
        }
    }

    private void Sell()
    {
        ItemData.itemInfoData.playerInventory.Remove(ItemData.ItemInfo);
    }

    void Buy()
    {
        ItemSO clone = Instantiate(ItemData.ItemInfo.ItemSo);
        clone.tradeState = ItemSO.TradeState.Sellable;
        clone.name = ItemData.ItemInfo.ItemSo.name;
        ItemData.itemInfoData.playerInventory.Add(clone);
        ItemData.itemInfoData.storeInventory.Remove(ItemData.ItemInfo);
    }
    
}
