using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class TimeManager : MonoBehaviour {
  private FirebaseFirestore store;
  private DateTime time;
  

  private IEnumerator Start() {
    Debug.Log(Timestamp.GetCurrentTimestamp());
    var timestamp = Timestamp.GetCurrentTimestamp();
    yield return new WaitForSeconds(10);
    var timestamp2 = Timestamp.GetCurrentTimestamp();
   


  }
}