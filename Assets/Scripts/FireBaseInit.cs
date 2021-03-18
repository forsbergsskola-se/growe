using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;

using UnityEngine;



public class FireBaseInit : MonoBehaviour {
    FireBaseAuthentication auth;

    void Awake() {
        auth = GetComponent<FireBaseAuthentication>();


        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {

            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            StartCoroutine(auth.SigninAnonymously());
        });
    }
  

    //WE CAN NOW LOG FIREBASE EVENTS AFTER THIS START METHOD(or inside after set-analytics-collect(true))^^
 
}