using Broker;
using Broker.Messages;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class CurrencyUI : MonoBehaviour {
        public Text currencyText;

        private void Start() {
            MessageBroker.Instance().SubscribeTo<SoftCurrencyUpdateMessage>(UpdateText);
        }

        void UpdateText(SoftCurrencyUpdateMessage m) {
            currencyText.text = $"{m.amount}";
        }

        private void OnDisable() {
            MessageBroker.Instance().UnSubscribeFrom<SoftCurrencyUpdateMessage>(UpdateText);
        }
    }
}