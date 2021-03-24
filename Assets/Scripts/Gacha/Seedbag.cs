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
                //Get a random item from the drop table
                var randomItem = items.GetRandomItem();
                droppedItem[i] = randomItem;
                //Add the random item to the inventory
                inventory.Add(randomItem);
            }

            return droppedItem;
        }
    }
}