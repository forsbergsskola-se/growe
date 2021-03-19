using UnityEngine;

namespace InventoryAndStore
{
    public class Inventories : MonoBehaviour
    {
        public Inventory playerInventory, storeInventory;

        public static Inventories Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}
