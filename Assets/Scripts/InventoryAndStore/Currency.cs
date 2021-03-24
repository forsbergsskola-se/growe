using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Broker;
using Broker.Messages;
using Saving;
using UnityEngine;

namespace InventoryAndStore {

    public class Currency : MonoBehaviour {
        public int maxCompostValue = 15;
        public int fertilizerAmountFromFilledCompost = 1;
        private CurrencyData _data;
<<<<<<< Updated upstream
=======
        private InventoryData _inventoryData;
        private AuctionData _auctionData;
>>>>>>> Stashed changes
        private SaveManager _saveManager;
        private bool _hasLoaded;

        private IEnumerator Start() {
            _saveManager = FindObjectOfType<SaveManager>();
            
            yield return new WaitForSeconds(1f);
            var dataTask = _saveManager.LoadCurrency();
            yield return new WaitUntil(() => dataTask.IsCompleted); 
            var dataTaskInventory = _saveManager.LoadInventory();
            yield return new WaitUntil(() => dataTaskInventory.IsCompleted);
            
            _hasLoaded = true;
            var data = dataTask.Result;
            if (data.HasValue && dataTaskInventory.Result.HasValue) {
                
                Debug.Log(dataTaskInventory.Result.Value.Inventory);

                _inventoryData = dataTaskInventory.Result.Value;
                _data = data.Value;
<<<<<<< Updated upstream
                MessageBroker.Instance().Send(new SoftCurrencyUpdateMessage(_data.softCurrency));
                MessageBroker.Instance().Send(new FertilizerUpdateMessage(_data.fertilizer));
                MessageBroker.Instance().Send(new CompostUpdateMessage(_data.compost));
=======
                MessageBroker.Instance().Send(new SoftCurrencyUpdateMessage(_data.SoftCurrency));
                MessageBroker.Instance().Send(new AuctionUpdateMessage(_auctionData.Item));
                MessageBroker.Instance().Send(new InventoryUpdateMessage(_inventoryData.Inventory));
                MessageBroker.Instance().Send(new FertilizerUpdateMessage(_data.Fertilizer));
                MessageBroker.Instance().Send(new CompostUpdateMessage(_data.Compost));
>>>>>>> Stashed changes
            }
            else
            {
                Debug.LogWarning("Couldn't load data" + this);
            }
        }

        public void AddSoftCurrency(float amount) {
            if (!_hasLoaded) return;
            _data.softCurrency += amount;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new SoftCurrencyUpdateMessage(_data.softCurrency));
        }
        public void FireBaseSetUserInventory(Inventory inventory) {
            if (!_hasLoaded) return;
            List<ItemClass> items = inventory.items.Select(item => ConvertSO.SOToClass(item)).ToList();
            _inventoryData.Inventory = items;
            _saveManager.UploadUserInventory(_inventoryData);
        }
        public void FireBaseGetUserInventory() {
            if (!_hasLoaded) return;
            //_saveManager.UploadUserInventory(_inventoryData);
            MessageBroker.Instance().Send(new InventoryUpdateMessage(_inventoryData.Inventory));
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
                MessageBroker.Instance().Send(new CompostBarFilledMessage());
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