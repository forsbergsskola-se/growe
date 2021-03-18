using UnityEngine;

namespace InventoryAndStore
{
    public class Inventories : MonoBehaviour
    {
        public Inventory playerInventory, storeInventory;

        public static Inventories instance;

        private void Awake()
        {
            instance = this;
        }
    }
}
