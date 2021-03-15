using UnityEngine;

namespace Inventory
{
    public class TestingScript : MonoBehaviour
    {
        public ItemSO test1, test2;
        
        public void TestingButton()
        {
            Inventory.Add(test1);
            Inventory.Add(test2);
            //if (Inventory.CreateItemSlot.foundUniqueItem.Contains(test2) == true) {    Debug.Log("Item exists!"); }
        }
    }
}
