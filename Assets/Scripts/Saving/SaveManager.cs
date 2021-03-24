using System.Threading.Tasks;
using Firebase.Database;
using InventoryAndStore;
using TimeManager;
using UnityEngine;

namespace Saving {
    public class SaveManager : MonoBehaviour {
        private string key;
        private string PLAYER_KEY {
            get => "PLAYER_KEY";
        }

        private string AUCTION_KEY = "AUCTION_KEY";
        private FirebaseDatabase _database;

        private void Start() {
            _database = FirebaseDatabase.GetInstance("https://growe-e7606-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public void SaveCurrency(CurrencyData data) {
            _database.GetReference(PLAYER_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(data));
        }
        public void UploadToAuction(AuctionData data) {
            _database.GetReference(AUCTION_KEY).Child("AuctionHouse").Push().SetRawJsonValueAsync(JsonUtility.ToJson(data));
        }
        
        
        public async Task<CurrencyData?> LoadCurrency() {
            var dataSnapshot = await _database.GetReference(PLAYER_KEY).GetValueAsync();
            if (!dataSnapshot.Exists) {
                return null;
            }
            return JsonUtility.FromJson<CurrencyData>(dataSnapshot.GetRawJsonValue());
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
        
        public void SaveTime2(TimeData time) {
            _database.GetReference(PLAYER_KEY).Child("time2").SetRawJsonValueAsync(JsonUtility.ToJson(time));
        }
        
        public async Task<TimeData?> LoadTime() {
            DataSnapshot dataSnapshot = await _database.GetReference(PLAYER_KEY).Child("time").GetValueAsync();
            if (!dataSnapshot.Exists) {
                return null;
            }
            return JsonUtility.FromJson<TimeData?>(dataSnapshot.GetRawJsonValue());
        }
    }
}
