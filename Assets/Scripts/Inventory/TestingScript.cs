using UnityEngine;

namespace Inventory
{
    public class TestingScript : MonoBehaviour
    {
        public ItemSO[] ItemSos;
        public Seedbag seedbag;

        public void TestingButton()
        {
            foreach (var itemSo in ItemSos)
            {
                Inventory.Add(itemSo);
            }
            //if (Inventory.CreateItemSlot.foundUniqueItem.Contains(test2) == true) {    Debug.Log("Item exists!"); }
        }

        public void TestOpen()
        {
            //Get Three Items Into The Inventory
            seedbag.Open(3);
        }
    }
}
