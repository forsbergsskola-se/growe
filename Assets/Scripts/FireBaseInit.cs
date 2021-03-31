using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;

public class FireBaseInit : MonoBehaviour {
    FireBaseAnonymousAuthentication _authAnonymous;

    void Start() {  
        _authAnonymous = GetComponent<FireBaseAnonymousAuthentication>();
        Debug.Log("Init");
        AnonymousLogIn();

    }
    public void AnonymousLogIn()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {

            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            StartCoroutine(_authAnonymous.SigninAnonymously());
        });
    }
    
}