using Inventory_and_Store;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    private ItemSO item;
    
    public void Init(ItemSO item)
    {
        this.item = item;
    }
}
