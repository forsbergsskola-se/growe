using InventoryAndStore;
using UnityEngine;

namespace Gacha
{
    [System.Serializable]
    public class Seedbag
    {
        [Header("Seedbag Item Drop Table")]
        public DropTable items;

        public void Open(int amount, Inventory inventory)
        {
            for (int i = 0; i < amount; i++)
            {
                //Get a random item from the drop table
                var randomItem = items.GetRandomItem();
                //Add the random item to the inventory
                inventory.Add(randomItem);
            }
        }
    }
}