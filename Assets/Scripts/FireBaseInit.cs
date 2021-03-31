using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;

public class FireBaseInit : MonoBehaviour {
    FireBaseAnonymousAuthentication authAnonymous;
    FirebaseAuth _auth;
    
    void Awake() {  // FirebaseAuthentication was initialized
        authAnonymous = GetComponent<FireBaseAnonymousAuthentication>();
        AnonymousLogIn();

    }
    public void AnonymousLogIn()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {

            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            if (_auth.CurrentUser != null) return;
            StartCoroutine(authAnonymous.SigninAnonymously());
        });
    }
    
}