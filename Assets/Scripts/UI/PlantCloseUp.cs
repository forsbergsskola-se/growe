using System;
using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WorldGrid;

namespace UI {
    public class PlantCloseUp : MonoBehaviour {
        ItemSO _plant;
        GridMoveObject _gridMoveObject;
        float _previousCameraSize;

        public UnityEvent onEnable;
        public UnityEvent onDisable;

        public Text plantName;
        public Slider plantRarity;
        public Text sellText;
        public Text compostText;
        public Text survivabilityText;
        public Slider growthStage;
        public Slider soilStatus;
        public Text plantLore;

        public void CompostPlant() {
            FindObjectOfType<Currency>().AddCompost(_plant.compostValue);
            _gridMoveObject.DestroyGridObj();
        }

        public void SellPlant() {
            FindObjectOfType<Currency>().AddSoftCurrency(_plant.sellValue);
            _gridMoveObject.DestroyGridObj();
        }

        public void ZoomOut() {
            FindObjectOfType<CameraMovement>().ZoomOut(_previousCameraSize);
        }

        void UpdatePreviousCameraPosition(PreviousCameraSizeMessage m) {
            _previousCameraSize = m.size;
        }

        void Awake() {
            MessageBroker.Instance().SubscribeTo<PlantCloseUpMessage>(UpdateItem);
            MessageBroker.Instance().SubscribeTo<PreviousCameraSizeMessage>(UpdatePreviousCameraPosition);
            gameObject.SetActive(false);
        }

        void OnEnable() {
            onEnable.Invoke();
        }

        void OnDisable() {
            onDisable.Invoke();
        }

        void OnDestroy() {
            MessageBroker.Instance().UnSubscribeFrom<PreviousCameraSizeMessage>(UpdatePreviousCameraPosition);
            MessageBroker.Instance().UnSubscribeFrom<PlantCloseUpMessage>(UpdateItem);
        }

        void UpdateValues() {
            plantName.text = _plant.name;
            plantLore.text = _plant.itemLore;
            plantRarity.value = Array.IndexOf(Enum.GetValues(_plant.rarity.GetType()), _plant.rarity) + 1;
            sellText.text = _plant.sellValue.ToString();
            compostText.text = _plant.compostValue.ToString();
            survivabilityText.text = _plant.timesCut.ToString();
            growthStage.value = (int) _plant.CurrentGrowthStage + 1;
            soilStatus.value = (int) _gridMoveObject.GetComponent<GridPlant>().currentSoilStage + 1;
            plantLore.text = _plant.itemLore;
        }

        void UpdateItem(PlantCloseUpMessage m) {
            _plant = m.plant;
            _gridMoveObject = m.plantParentObject;
            UpdateValues();
        }
    }
}
