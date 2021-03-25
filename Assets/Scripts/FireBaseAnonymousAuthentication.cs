using System.Collections;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireBaseAnonymousAuthentication : MonoBehaviour {

    // public UnityEvent fetchedUser;

    FirebaseAuth _auth;
    FirebaseUser _currentUser;
    public string GetAnonymousUserId() {
        return _currentUser.UserId;
    }
    public IEnumerator SigninAnonymously() {
        _auth = FirebaseAuth.DefaultInstance;
        var registerTask = _auth.SignInAnonymouslyAsync();
        
        yield return new WaitUntil(()=>registerTask.IsCompleted);
        
        _currentUser = registerTask.Result;

        SceneManager.LoadScene("MainMenu"); // to close initial scene
        // fetchedUser?.Invoke();
    }

    //TODO: sign out button should be created in main menu
    //TODO: this can be created in Email authentication too
    public void SignOutAnonymousButton()
    {
        _auth.SignOut();
    }
}