using System.Collections;
using Inventory_and_Store;
using UnityEngine;
using UnityEngine.UI;

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
        while (ItemData.ItemInfo is null) yield return null;
        Text.text = ItemData.ItemInfo.ItemSo.tradeState switch
        {
            ItemSO.TradeState.Buyable => ItemData.ItemInfo.ItemSo.buyValue + " Buy",
            ItemSO.TradeState.Sellable => ItemData.ItemInfo.ItemSo.sellValue + " Sell",
            _ => Text.text
        };
    }

    public void ButtonPressed()
    {
        switch (ItemData.ItemInfo.ItemSo.tradeState)
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
        ItemData.itemInfoData.playerInventory.Remove(ItemData.ItemInfo);
    }

    private void Buy()
    {
        ItemSO clone = Instantiate(ItemData.ItemInfo.ItemSo);
        clone.tradeState = ItemSO.TradeState.Sellable;
        clone.name = ItemData.ItemInfo.ItemSo.name;
        ItemData.itemInfoData.playerInventory.Add(clone);
        ItemData.itemInfoData.storeInventory.Remove(ItemData.ItemInfo);
    }
    
}
