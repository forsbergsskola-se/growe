using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory_and_Store
{
    public class ItemData : MonoBehaviour
    {
        public Item ItemInfo;
        public int amount;
        public Text amountText;
        public ItemInfoData itemInfoData;
        private Image Icon => GetComponent<Image>();

        private void Start()
        {
            itemInfoData = UIReferences.Instance.itemInfoBox;
            Icon.sprite = ItemInfo.ItemSo.icon;
            amountText.text = amount.ToString();
        }

        public void ShowItemInfo()
        {
            itemInfoData.gameObject.SetActive(true);
            itemInfoData.itemData = this;
            itemInfoData.UpdateItemInfo();
        }
    }
}
