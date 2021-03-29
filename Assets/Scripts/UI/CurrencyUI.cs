using Broker;
using Broker.Messages;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class CurrencyUI : MonoBehaviour {
        public Text softCurrencyText;
        public Text fertilizerText;

        private void OnEnable() {
            MessageBroker.Instance().SubscribeTo<SoftCurrencyUpdateMessage>(UpdateSoftCurrencyText);
            MessageBroker.Instance().SubscribeTo<FertilizerUpdateMessage>(UpdateFertilizerText);
        }

        void UpdateSoftCurrencyText(SoftCurrencyUpdateMessage m) {
            softCurrencyText.text = $"{m.amount}";
        }
        
        void UpdateFertilizerText(FertilizerUpdateMessage m) {
            fertilizerText.text = $"{m.amount}";
        }
        
        private void OnDisable() {
            MessageBroker.Instance().UnSubscribeFrom<SoftCurrencyUpdateMessage>(UpdateSoftCurrencyText);
            MessageBroker.Instance().UnSubscribeFrom<FertilizerUpdateMessage>(UpdateFertilizerText);
        }
    }
}