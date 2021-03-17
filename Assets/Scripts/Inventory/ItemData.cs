using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemData : MonoBehaviour
    {
        public Item ItemInfo;
        public int amount;
        public Text amountText;
        private ItemInfoData _showInfoScreen;
        private Image Icon => GetComponent<Image>();

        private void Start()
        {
            _showInfoScreen = UIReferences.Instance.itemInfoBox;
            Icon.sprite = ItemInfo.ItemSo.icon;
            amountText.text = amount.ToString();
        }

        public void ShowItemInfo()
        {
            _showInfoScreen.gameObject.SetActive(true);
            _showInfoScreen.itemData = this;
            _showInfoScreen.UpdateItemInfo();
        }
    }
}
