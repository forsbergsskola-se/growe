using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    private string key;
    private string PLAYER_KEY {
        get => "PLAYER_KEY";
        // FindObjectOfType<FireBaseAuthentication>().GetUserId();
    }
    private FirebaseDatabase _database;

    private void Start() {
            
        _database = FirebaseDatabase.GetInstance("https://growe-e7606-default-rtdb.europe-west1.firebasedatabase.app/");
            
    }

    public void SaveCurrency(CurrencyData data) {
        _database.GetReference(PLAYER_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(data));
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
}
