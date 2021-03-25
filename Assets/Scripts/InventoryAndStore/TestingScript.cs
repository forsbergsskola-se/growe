using UnityEngine;

namespace InventoryAndStore
{
    public class TestingScript : MonoBehaviour
    {
        public Inventory inventory;
        public ItemSO[] itemSos;

        public void TestingButton()
        {
            foreach (var itemSo in itemSos) 
                inventory.Add(itemSo);
            
        }
    }
}
