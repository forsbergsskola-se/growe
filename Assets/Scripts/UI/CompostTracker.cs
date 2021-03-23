using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class CompostTracker : MonoBehaviour {
        public Slider leftSlider;
        public Slider rightSlider;
        
        //TODO use CompostBarFilledMessage if we want to add some feedback to the Compost bar being filled

        void Start() {
            var maxValue = FindObjectOfType<Currency>().maxCompostValue;
            SetSlidersMinMaxValues(maxValue);
        }
        
        void OnEnable() {
            MessageBroker.Instance().SubscribeTo<CompostUpdateMessage>(UpdateCompostAmount);
        }

        void OnDisable() {
            MessageBroker.Instance().UnSubscribeFrom<CompostUpdateMessage>(UpdateCompostAmount);
        }
        
        void UpdateCompostAmount(CompostUpdateMessage m) {
            leftSlider.value = m.amount;
            rightSlider.value = m.amount;
        }

        void SetSlidersMinMaxValues(int maxValue) {
            leftSlider.minValue = 0;
            rightSlider.minValue = 0;
            leftSlider.maxValue = maxValue;
            rightSlider.maxValue = maxValue;
        }
    }
}
