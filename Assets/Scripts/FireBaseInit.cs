using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;

using UnityEngine;



public class FireBaseInit : MonoBehaviour {
    FireBaseAuthentication auth;

    //It was awake 
    //I modified it as void to use in "play as guest button"
    public void PlayAsGuest() {
        auth = GetComponent<FireBaseAuthentication>();


        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {

            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            StartCoroutine(auth.SigninAnonymously());
        });
    }
  

    //WE CAN NOW LOG FIREBASE EVENTS AFTER THIS START METHOD(or inside after set-analytics-collect(true))^^
 
}