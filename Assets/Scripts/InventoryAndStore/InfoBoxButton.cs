using UnityEngine;

namespace InventoryAndStore
{
    public class InfoBoxButton : MonoBehaviour
    {
        private ItemInfoData ItemInfoData => GetComponent<ItemInfoData>();

        public void ButtonInteract()
        {
            ItemSO itemSO = ItemInfoData.itemData.ItemSO;
            
            if (itemSO.itemType == ItemSO.ItemType.Seedbag && itemSO.tradeState != ItemSO.TradeState.Buyable)
            {
                itemSO.Seedbag.Open(3, Inventories.Instance.playerInventory);
                Inventories.Instance.playerInventory.Remove(itemSO);
                ItemInfoData.gameObject.SetActive(false);
            }
            else if (itemSO.tradeState == ItemSO.TradeState.Buyable)
            {
                ItemSO clone = Instantiate(itemSO);
                clone.tradeState = ItemSO.TradeState.Sellable;
                clone.name = itemSO.name;
                ItemInfoData.playerInventory.Add(clone);
                ItemInfoData.storeInventory.Remove(itemSO);
            }
        }
    }
}
