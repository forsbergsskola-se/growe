using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI {
    public class PlantCloseUp : MonoBehaviour {
        ItemSO _plant;
        GridObject _plantParentObject;
        GridPlant _gridPlant;
        float _previousCameraSize;

        public UnityEvent onEnable;
        public UnityEvent onDisable;

        public Text plantName;
        public Slider plantRarity;
        public Text sellText;
        public Text compostText;
        public Slider growthStage;
        public Slider soilStatus;
        public Text plantLore;

        public void CompostPlant() {
            FindObjectOfType<Currency>().AddCompost(_plant.compostValue);
            _plantParentObject.DestroyGridObj();
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
            _gridPlant = _plantParentObject.GetComponentInChildren<GridPlant>();
            plantName.text = _plant.name;
            plantLore.text = _plant.itemLore;
            plantRarity.value = (int) _plant.rarity + 1;
            sellText.text = _plant.sellValue.ToString();
            compostText.text = _plant.compostValue.ToString();
            growthStage.value = (int) _plant.CurrentGrowthStage + 1;
            soilStatus.value = (int) _gridPlant.currentSoilStage + 1;
            plantLore.text = _plant.itemLore;
        }

        void UpdateItem(PlantCloseUpMessage m) {
            _plant = m.plant;
            _plantParentObject = m.plantParentObject;
            UpdateValues();
        }
    }
}
