using InventoryAndStore;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class PlantCloseUp : MonoBehaviour {
        ItemSO _item;

        public Text plantName;
        public Text plantLore;
        public Text plantRarity;
        public Slider growthSlider;

        void OnEnable() {
            //TODO get itemSO
            
            plantName.text = _item.name;
            plantLore.text = _item.itemLore;
            plantRarity.text = _item.rarity.ToString();
            growthSlider.value = (int) _item.CurrentGrowthStage;
        }
    }
}
