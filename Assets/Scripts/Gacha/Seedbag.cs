using InventoryAndStore;
using UnityEngine;

namespace Gacha
{
    [System.Serializable]
    public class Seedbag
    {
        [Header("Seedbag Item Drop Table")]
        public DropTable items;

        public ItemSO[] Open(int amount, Inventory inventory)
        {
            ItemSO[] droppedItem = new ItemSO[amount];

            for (int i = 0; i < amount; i++)
            {
                var randomItem = items.GetRandomItem();
                if (randomItem.tradeState == ItemSO.TradeState.Sellable)
                {
                    droppedItem[i] = randomItem;
                }
                else Debug.LogWarning($"Removed {randomItem} from seed bag due to wrong tradestate");
            }
            inventory.Add(droppedItem);

            return droppedItem;
        }
    }
}