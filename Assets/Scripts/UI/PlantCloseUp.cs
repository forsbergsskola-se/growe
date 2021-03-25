using System;
using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI {
    public class PlantCloseUp : MonoBehaviour {
        ItemSO _item;

        public UnityEvent onEnable;
        public UnityEvent onDisable;

        public Text plantName;
        public Text plantRarity;
        public Text sellText;
        public Text compostText;
        public Slider growthStage;
        public Slider soilStatus;
        public Text plantLore;
        
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
            plantName.text = _item.name;
            plantRarity.text = _item.rarity.ToString();
            sellText.text = _item.sellValue.ToString();
            compostText.text = _item.compostValue.ToString();
            growthStage.value = _item.growthStage;
            //TODO soilStatus.value = 
            plantLore.text = _item.itemLore;
        }

        void UpdateItem(PlantCloseUpMessage m) {
            _item = m.item;
            Debug.Log("Updating _item " + m.item);
            UpdateValues();
        }
    }
}
