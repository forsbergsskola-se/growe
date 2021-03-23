using System.Collections;
using Broker;
using Broker.Messages;
using Saving;
using UnityEngine;

namespace InventoryAndStore {

    public class Currency : MonoBehaviour {
        public int maxCompostValue = 15;
        public int fertilizerAmountFromFilledCompost = 1;
        private CurrencyData _data;
        private SaveManager _saveManager;
        private bool _hasLoaded;

        private IEnumerator Start() {
            _saveManager = FindObjectOfType<SaveManager>();
            yield return new WaitForSeconds(1f);
            var dataTask = _saveManager.LoadCurrency();
            yield return new WaitUntil(() => dataTask.IsCompleted);
            _hasLoaded = true;
            var data = dataTask.Result;
            if (data.HasValue) {
                _data = data.Value;
                MessageBroker.Instance().Send(new SoftCurrencyUpdateMessage(_data.softCurrency));
                MessageBroker.Instance().Send(new FertilizerUpdateMessage(_data.fertilizer));
                MessageBroker.Instance().Send(new CompostUpdateMessage(_data.compost));
            }
        }

        public void AddSoftCurrency(float amount) {
            if (!_hasLoaded) return;
            _data.softCurrency += amount;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new SoftCurrencyUpdateMessage(_data.softCurrency));
        }
        
        public void AddFertilizer(int amount) {
            if (!_hasLoaded) return;
            _data.fertilizer += amount;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new FertilizerUpdateMessage(_data.fertilizer));
        }
        
        public void AddCompost(int amount) {
            if (!_hasLoaded) return;
            _data.compost += amount;
            
            if (_data.compost >= maxCompostValue) {
                //TODO send event that compost is filled
                Debug.Log("Compost filled, adding fertilizer " + this);
                AddFertilizer(fertilizerAmountFromFilledCompost);
                var overflow = _data.compost - maxCompostValue;
                _data.compost = overflow;
            }

            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new CompostUpdateMessage(_data.compost));
        }

        public bool TryRemoveSoftCurrency(float amount) {
            if (!_hasLoaded || amount > _data.softCurrency) return false;
            _data.softCurrency -= amount;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new SoftCurrencyUpdateMessage(_data.softCurrency));
            return true;
        }
        
        public bool TryRemoveFertilizer(int amount) {
            if (!_hasLoaded || amount > _data.fertilizer) return false;
            _data.fertilizer -= amount;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new FertilizerUpdateMessage(_data.fertilizer));
            return true;
        }
        
        public bool TryRemoveCompost(int amount) {
            if (!_hasLoaded || amount > _data.compost) return false;
            _data.compost -= amount;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new CompostUpdateMessage(_data.compost));
            return true;
        }
        
        /// <summary>
        /// Method used for testing
        /// </summary>
        /// <param name="amount"></param>
        public void RemoveCurrency(int amount) {
            TryRemoveFertilizer(amount);
        }
    }
}