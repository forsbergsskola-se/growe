using InventoryAndStore;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    public ItemSO item;
    
    public void Init(ItemSO item)
    {
        this.item = item;
    }
}
