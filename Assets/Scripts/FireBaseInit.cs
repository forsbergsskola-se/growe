using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FireBaseInit : MonoBehaviour {
    FireBaseAnonymousAuthentication authAnonymous;
    
    void Awake() {  // FirebaseAuthentication was initialized
        authAnonymous = GetComponent<FireBaseAnonymousAuthentication>();
        
    }
    public void AnonymousLogIn()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {

            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            StartCoroutine(authAnonymous.SigninAnonymously());
        });
    }
    //WE CAN NOW LOG FIREBASE EVENTS AFTER THIS START METHOD(or inside after set-analytics-collect(true))^^
}