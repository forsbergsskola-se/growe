using Broker;
using Broker.Messages;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class CompostTracker : MonoBehaviour {
        public Slider leftSlider;
        public Slider rightSlider;
        int _compostAmount;

        void Start() {
            leftSlider.minValue = 0;
            rightSlider.minValue = 0;
            leftSlider.maxValue = 15;
            rightSlider.maxValue = 15;
            //TODO get maxCompostValue from Currency
        }

        void OnEnable() {
            MessageBroker.Instance().SubscribeTo<CompostUpdateMessage>(UpdateCompostAmount);
        }

        void OnDisable() {
            MessageBroker.Instance().UnSubscribeFrom<CompostUpdateMessage>(UpdateCompostAmount);
        }
        
        void UpdateCompostAmount(CompostUpdateMessage m) {
            _compostAmount = m.amount;
            UpdateSliders();
        }

        //TODO add event that tells listeners to increase fertilizer by 1
        //TODO OR use FindObjectOfType -> GameManager -> GetComponent -> Currency and use AddFertilizer(1) method

        void UpdateSliders() {
            leftSlider.value = _compostAmount;
            rightSlider.value = _compostAmount;
        }
    }
}
