using System.Collections;
using Firebase.Auth;

using UnityEngine;
using UnityEngine.Events;

public class FireBaseAuthentication : MonoBehaviour {

    // public UnityEvent fetchedUser;

    FirebaseAuth _auth;
    FirebaseUser _currentUser;
  
 

    public string GetUserId() {
        return _currentUser.UserId;
    }
    
    public IEnumerator SigninAnonymously() {
        _auth = FirebaseAuth.DefaultInstance;
        var registerTask = _auth.SignInAnonymouslyAsync();
        
        yield return new WaitUntil(()=>registerTask.IsCompleted);
        
        _currentUser = registerTask.Result;
        // fetchedUser?.Invoke();
    }
}