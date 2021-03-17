using Inventory_and_Store;
using UnityEngine;

public class SeedbagButton : MonoBehaviour
{
    public ItemInfoData itemInfoData;

    public void OpenSeedbag()
    {
        if (itemInfoData.itemData.ItemInfo.ItemSo.itemType == ItemSO.ItemType.Seedbag)
        {
            itemInfoData.itemData.ItemInfo.ItemSo.Seedbag.Open(amount: 3, inventory: itemInfoData.inventory);
            itemInfoData.inventory.Remove(itemInfoData.itemData.ItemInfo);
            itemInfoData.gameObject.SetActive(false);
        }
    }
}
