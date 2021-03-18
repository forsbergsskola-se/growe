using System.Collections;
using Broker;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory_and_Store {
    public class CurrencyUpdateMessage {
        public float currency;

        public CurrencyUpdateMessage(float currency) {
            this.currency = currency;
        }
    }
    public class Currency : MonoBehaviour {
        [SerializeField] private float _currency => _data.currency;
        [SerializeField] private CurrencyData _data;
        private SaveManager saveManager;
        public UnityEvent currencyUpdate;
        private bool hasLoaded;


        public CurrencyData Data => _data;


        private IEnumerator Start() {
            saveManager = FindObjectOfType<SaveManager>();
            yield return new WaitForSeconds(1f);
            var dataTask = saveManager.LoadCurrency();
            yield return new WaitUntil(() => dataTask.IsCompleted);
            hasLoaded = true;
            var data = dataTask.Result;
            if (data.HasValue) {
                _data.currency = data.Value.currency;
            }
        }

        public void AddCurrency(float value) {
            if (!hasLoaded) return;
            _data.currency += value;
            saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new CurrencyUpdateMessage(_data.currency));
          
        }

        public bool TryRemoveCurrency(float value) {
            if (!hasLoaded || value > _data.currency) return false;
            _data.currency -= value;
            saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new CurrencyUpdateMessage(_data.currency));
            return true;
        }

        public void SpendMoney(float value) {
            TryRemoveCurrency(value);
        }
    }
}