using UnityEngine;

namespace Inventory_and_Store
{
    public class InfoBoxButton : MonoBehaviour
    {
        private ItemInfoData itemInfoData => GetComponent<ItemInfoData>();

        public void ButtonInteract()
        {
            var itemSO = itemInfoData.itemData.ItemInfo.ItemSo;
            
            if (itemSO.itemType == ItemSO.ItemType.Seedbag && itemSO.tradeState == ItemSO.TradeState.SoulBound)
            {
                itemInfoData.itemData.ItemInfo.ItemSo.Seedbag.Open(amount: 3, inventory: itemInfoData.playerInventory);
                itemInfoData.playerInventory.Remove(itemInfoData.itemData.ItemInfo);
                itemInfoData.gameObject.SetActive(false);
            }
            else if (itemSO.tradeState == ItemSO.TradeState.Buyable)
            {
                itemInfoData.playerInventory.Add(itemSO);
                itemInfoData.storeInventory.Remove(itemInfoData.itemData.ItemInfo);
            }
        }
    }
}
