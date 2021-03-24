using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Broker;
using Broker.Messages;
using JSON;
using Newtonsoft.Json;
using Saving;
using UnityEngine;

namespace InventoryAndStore {

    public class Currency : MonoBehaviour {
        public int maxCompostValue = 15;
        public int fertilizerAmountFromFilledCompost = 1;
        private CurrencyData _data;
        private InventoryData _inventoryData;
        private AuctionData _auctionData;
<<<<<<< Updated upstream
=======
        private AuctionData _auctionData;
>>>>>>> main
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
                MessageBroker.Instance().Send(new SoftCurrencyUpdateMessage(_data.SoftCurrency));
                MessageBroker.Instance().Send(new AuctionUpdateMessage(_auctionData.Item));
                MessageBroker.Instance().Send(new InventoryUpdateMessage(_inventoryData.Inventory));
                MessageBroker.Instance().Send(new FertilizerUpdateMessage(_data.Fertilizer));
                MessageBroker.Instance().Send(new CompostUpdateMessage(_data.Compost));
<<<<<<< Updated upstream
                MessageBroker.Instance().Send(new SoftCurrencyUpdateMessage(_data.softCurrency));
                MessageBroker.Instance().Send(new FertilizerUpdateMessage(_data.fertilizer));
                MessageBroker.Instance().Send(new CompostUpdateMessage(_data.compost));
            }
            else
            {
                Debug.LogWarning("Couldn't load data" + this);
=======
                MessageBroker.Instance().Send(new SoftCurrencyUpdateMessage(_data.SoftCurrency));
                MessageBroker.Instance().Send(new AuctionUpdateMessage(_auctionData.Item));
                MessageBroker.Instance().Send(new FertilizerUpdateMessage(_data.Fertilizer));
                MessageBroker.Instance().Send(new CompostUpdateMessage(_data.Compost));
>>>>>>> main
            }
        }

        public void AddSoftCurrency(float amount) {
            if (!_hasLoaded) return;
            _data.SoftCurrency += amount;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new SoftCurrencyUpdateMessage(_data.SoftCurrency));
        }
        public void AddItemForAuction(ItemSO item) {
            if (!_hasLoaded) return;
            _auctionData.Item += JsonConvert.SerializeObject(ConvertSO.SOToClass(item), Formatting.Indented);;
            _saveManager.UploadToAuction(_auctionData);
            MessageBroker.Instance().Send(new AuctionUpdateMessage(_auctionData.Item));
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
            _data.Fertilizer += amount;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new FertilizerUpdateMessage(_data.Fertilizer));
        }

        public void AddCompost(int amount) {
            if (!_hasLoaded) return;
            _data.Compost += amount;

            if (_data.Compost >= maxCompostValue) {
                MessageBroker.Instance().Send(new CompostBarFilledMessage());
                Debug.Log("Compost filled, adding fertilizer " + this);
                AddFertilizer(fertilizerAmountFromFilledCompost);
                var overflow = _data.Compost - maxCompostValue;
                _data.Compost = overflow;
            }

            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new CompostUpdateMessage(_data.Compost));
        }

        public bool TryRemoveSoftCurrency(float amount) {
            if (!_hasLoaded || amount > _data.SoftCurrency) return false;
            _data.SoftCurrency -= amount;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new SoftCurrencyUpdateMessage(_data.SoftCurrency));
            return true;
        }

        public bool TryRemoveFertilizer(int amount) {
            if (!_hasLoaded || amount > _data.Fertilizer) return false;
            _data.Fertilizer -= amount;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new FertilizerUpdateMessage(_data.Fertilizer));
            return true;
        }

        public bool TryRemoveCompost(int amount) {
            if (!_hasLoaded || amount > _data.Compost) return false;
            _data.Compost -= amount;
            _saveManager.SaveCurrency(_data);
            MessageBroker.Instance().Send(new CompostUpdateMessage(_data.Compost));
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
