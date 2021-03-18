using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace InventoryAndStore {
    public class Currency : MonoBehaviour {
        [SerializeField] private int _currency => _data.currency;
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

        public void AddCurrency(int value) {
            if (!hasLoaded) return;
            _data.currency += value;
            saveManager.SaveCurrency(_data);
            Debug.Log(_data.currency);
        }

        public bool TryRemoveCurrency(int value) {
            if (!hasLoaded || value > _data.currency) return false;
            _data.currency -= value;
            saveManager.SaveCurrency(_data);
            Debug.Log(_data.currency);
            return true;
        }

        public void SpendMoney(int value) {
            TryRemoveCurrency(value);
        }
    }
}