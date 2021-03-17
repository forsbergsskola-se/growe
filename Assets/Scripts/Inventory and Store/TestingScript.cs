using UnityEngine;

namespace Inventory_and_Store
{
    public class TestingScript : MonoBehaviour
    {
        public Inventory inventory;
        public ItemSO[] itemSos;
        public Seedbag seedbag;
        
        public void TestingButton()
        {
            foreach (var itemSo in itemSos)
            {
                inventory.Add(itemSo);
            }
        }
        
        public void TestOpen()
        {
            //Get Three Items Into The Inventory
            seedbag.Open(3);
        }
    }
}
