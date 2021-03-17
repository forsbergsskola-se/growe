using UnityEngine;

namespace Inventory_and_Store
{
    public class InfoBoxButton : MonoBehaviour
    {
        private ItemInfoData ItemInfoData => GetComponent<ItemInfoData>();

        public void ButtonInteract()
        {
            var itemInfo = ItemInfoData.itemData.ItemInfo;
            
            if (itemInfo.ItemSo.itemType == ItemSO.ItemType.Seedbag && itemInfo.ItemSo.tradeState != ItemSO.TradeState.Buyable)
            {
                itemInfo.ItemSo.Seedbag.Open(amount: 3, inventory: ItemInfoData.playerInventory);
                ItemInfoData.playerInventory.Remove(ItemInfoData.itemData.ItemInfo);
                ItemInfoData.gameObject.SetActive(false);
            }
            else if (itemInfo.ItemSo.tradeState == ItemSO.TradeState.Buyable)
            {
                ItemSO clone = Instantiate(itemInfo.ItemSo);
                clone.tradeState = ItemSO.TradeState.Sellable;
                clone.name = itemInfo.ItemSo.name;
                ItemInfoData.playerInventory.Add(clone);
                ItemInfoData.storeInventory.Remove(itemInfo);
            }
        }
    }
}
