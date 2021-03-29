using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI {
    public class PlantCloseUp : MonoBehaviour {
        ItemSO _plant;
        GameObject _plantParentObject;

        public UnityEvent onEnable;
        public UnityEvent onDisable;

        public Text plantName;
        public Text plantRarity;
        public Text sellText;
        public Text compostText;
        public Slider growthStage;
        public Slider soilStatus;
        public Text plantLore;

        public void CompostPlant() {
            FindObjectOfType<Currency>().AddCompost(_plant.compostValue);
            //TODO Destroy(_plantParentObject);
        }

        void Awake() {
            MessageBroker.Instance().SubscribeTo<PlantCloseUpMessage>(UpdateItem);
            gameObject.SetActive(false);
        }

        void OnEnable() {
            onEnable.Invoke();
        }

        void OnDisable() {
            onDisable.Invoke();
        }

        void OnDestroy() {
            MessageBroker.Instance().UnSubscribeFrom<PlantCloseUpMessage>(UpdateItem);
        }

        void UpdateValues() {
            plantName.text = _plant.name;
            plantLore.text = _plant.itemLore;
            plantRarity.text = _plant.rarity.ToString();
            sellText.text = _plant.sellValue.ToString();
            compostText.text = _plant.compostValue.ToString();
            growthStage.value = (int) _plant.CurrentGrowthStage + 1;
            //TODO soilStatus.value = 
            plantLore.text = _plant.itemLore;
        }

        void UpdateItem(PlantCloseUpMessage m) {
            _plant = m.plant;
            _plantParentObject = m.plantParentObject;
            Debug.Log("Updating _item " + m.plant);
            UpdateValues();
        }
    }
}
