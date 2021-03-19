using UI;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryAndStore
{
    public class ItemData : MonoBehaviour
    {
        public ItemSO ItemSO;
        public int amount;
        public Text amountText;
        public ItemInfoData itemInfoData;
        private Image Icon => GetComponent<Image>();

        private void Start()
        {
            itemInfoData = UIReferences.Instance.itemInfoBox;
            Icon.sprite = ItemSO.icon;
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
