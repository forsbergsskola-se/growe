using System.Collections;
using Broker;
using Broker.Messages;
using Saving;
using UnityEngine;

namespace Inventory_and_Store {

    public class Currency : MonoBehaviour {
        private CurrencyData _data;
        private SaveManager _saveManager;
        private bool _hasLoaded;


        public CurrencyData Data => _data;


        private IEnumerator Start() {
            _saveManager = FindObjectOfType<SaveManager>();
            yield return new WaitForSeconds(1f);
            var dataTask = _saveManager.LoadCurrency();
            yield return new WaitUntil(() => dataTask.IsCompleted);
            _hasLoaded = true;
            var data = dataTask.Result;
            if (data.HasValue) {
                _data.currency = data.Value.currency;
            }
        }

        public void AddCurrency(float value) {
            if (!_hasLoaded) return;
            _data.currency += value;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new CurrencyUpdateMessage(_data.currency));
        }

        public bool TryRemoveCurrency(float value) {
            if (!_hasLoaded || value > _data.currency) return false;
            _data.currency -= value;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new CurrencyUpdateMessage(_data.currency));
            return true;
        }

        public void SpendMoney(float value) {
            TryRemoveCurrency(value);
        }
    }
}