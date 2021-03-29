using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using InventoryAndStore;
using JetBrains.Annotations;
using Newtonsoft.Json;
using TimeManager;
using UnityEngine;

namespace Saving {
    public class SaveManager : MonoBehaviour {
        private string key;
        private string PLAYER_KEY
        {
            get => FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        }

        private FireBaseAnonymousAuthentication authAnonymous;

        private string AUCTION_KEY = "AUCTION_KEY";
        private FirebaseDatabase _database;
        //TODO: reference keys should be
        private void Start() {
            _database = FirebaseDatabase.GetInstance("https://growe-e7606-default-rtdb.europe-west1.firebasedatabase.app/");

            _database.SetPersistenceEnabled(false);
        }

        public void SaveCurrency(CurrencyData data) {
            _database.GetReference(PLAYER_KEY).Child("Currency").SetRawJsonValueAsync(JsonUtility.ToJson(data));
        }
        public void UploadToAuction(AuctionData data) {
            _database.GetReference(AUCTION_KEY).Child("AuctionHouse").Push().SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }
        public void UploadUserInventory(InventoryData data)
        {
            _database.GetReference(PLAYER_KEY).Child("Inventory").SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }


        public async Task<CurrencyData?> LoadCurrency() {
            var dataSnapshot = await _database.GetReference(PLAYER_KEY).Child("Currency").GetValueAsync();
            if (!dataSnapshot.Exists) {
                return null;
            }
            return JsonUtility.FromJson<CurrencyData>(dataSnapshot.GetRawJsonValue());
        }

        public async Task<InventoryData?> LoadInventory() {
            var dataSnapshot = await _database.GetReference(PLAYER_KEY).Child("Inventory").GetValueAsync();
            if (!dataSnapshot.Exists) return null;
            return JsonConvert.DeserializeObject<InventoryData>(dataSnapshot.GetRawJsonValue());
        }

        public async Task<bool> SaveExists() {
            var dataSnapshot = await _database.GetReference(PLAYER_KEY).GetValueAsync();
            return dataSnapshot.Exists;
        }

        public void EraseSave() {
            _database.GetReference(PLAYER_KEY).RemoveValueAsync();
        }

        public void SaveTime(TimeData time) {
            _database.GetReference(PLAYER_KEY).Child("time").SetRawJsonValueAsync(JsonUtility.ToJson(time));
        }

        public async Task<TimeData?> LoadTime() {
            DataSnapshot dataSnapshot = await _database.GetReference(PLAYER_KEY).Child("time").GetValueAsync();
            if (!dataSnapshot.Exists) {
                return null;
            }
            return JsonUtility.FromJson<TimeData>(dataSnapshot.GetRawJsonValue());
        }

        // SAVEAUCTION & LOADAUCTION SHOULD BE IMPLEMENTED IN AUCTION DATA
        public void SaveAuction(AuctionData auctionData) {
            _database.GetReference(PLAYER_KEY).Child("auction").SetRawJsonValueAsync(JsonUtility.ToJson(auctionData));
        }

        public async Task<AuctionData?> LoadAuction() {
            DataSnapshot dataSnapshot = await _database.GetReference(PLAYER_KEY).Child("auction").GetValueAsync();
            if (!dataSnapshot.Exists) {
                return null;
            }
            return JsonUtility.FromJson<AuctionData>(dataSnapshot.GetRawJsonValue());
        }
        
        public void SaveGrid(List<GridSaveInfo> gridItems) {
            _database.GetReference(PLAYER_KEY).Child("grid").SetRawJsonValueAsync(JsonConvert.SerializeObject(gridItems));
        }
        
        public async Task<List<GridSaveInfo>?> LoadGrid() 
        {
            var dataSnapshot = await _database.GetReference(PLAYER_KEY).Child("grid").GetValueAsync();
            if (!dataSnapshot.Exists) return null;
            return JsonConvert.DeserializeObject<List<GridSaveInfo>>(dataSnapshot.GetRawJsonValue());
        }
        
    }
}
