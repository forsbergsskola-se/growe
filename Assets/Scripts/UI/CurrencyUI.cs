using Broker;
using Broker.Messages;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class CurrencyUI : MonoBehaviour {
        public Text currencyText;

        private void Start() {
            MessageBroker.Instance().SubscribeTo<CurrencyUpdateMessage>(UpdateText);
        }

        void UpdateText(CurrencyUpdateMessage m) {
            currencyText.text = $"{m.currency}";
        }

        private void OnDisable() {
            MessageBroker.Instance().UnSubscribeFrom<CurrencyUpdateMessage>(UpdateText);
        }
    }
}