using System.Collections;
using Firebase;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Auth;   

public class FireBaseEmailAuthentication : MonoBehaviour
{
    [Header("Log In UI")] 
    [SerializeField] private InputField logInMail;
    [SerializeField] private InputField logInPassword;
    [SerializeField] private Text warningLogInText;
    [SerializeField] private Text confirmLogInText;
    
    [Header("Sign In UI")] 
    [SerializeField] private InputField signUpUsername;
    [SerializeField] private InputField signUpMail;
    [SerializeField] private InputField signUpPassword;
    [SerializeField] private InputField signUpPasswordCheck;
    [SerializeField] private Text warningSignUpText;

    [Header("Firebase")] 
    public DependencyStatus dependencyStatus;
    FirebaseAuth auth;
    FirebaseUser user;
    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies" + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
    }

    public void LogInButton()
    {
        StartCoroutine(LogIn(logInMail.text, logInPassword.text));
    }
    public void SignUpButton()
    {
        StartCoroutine(SignUp(signUpMail.text, signUpPassword.text,signUpUsername.text));
    }

    private IEnumerator LogIn(string _email, string _password)
    {
        var logInTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(predicate: () => logInTask.IsCompleted);
        if (logInTask.Exception != null)
        {
            Debug.LogWarning(message:$"Failed to log in task with {logInTask.Exception}");
            FirebaseException firebaseEx = logInTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError) firebaseEx.ErrorCode;

            string message = "Log In failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing e-mail";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid e-mail";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLogInText.text = message;
        }
        else
        {
            user = logInTask.Result;
            Debug.LogFormat("User signed in successfully: {0} {1}",user.DisplayName,user.Email);
            warningLogInText.text = "";
            confirmLogInText.text = "Logged In";
            SceneManager.LoadScene("MainMenu");
        }
    }

    private IEnumerator SignUp(string _email, string _password, string _username)
    {
        if (_username =="")
        {
            warningSignUpText.text = "Missing username";
        }
        else if (signUpPassword.text !=signUpPasswordCheck.text)
        {
            warningSignUpText.text = "Password does not match";
        }
        else
        {
            var signUpTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            yield return new WaitUntil(predicate: () => signUpTask.IsCompleted);
            if (signUpTask.Exception != null)
            {
                Debug.LogWarning(message:$"Failed to sign up task with {signUpTask.Exception}");
                FirebaseException firebaseEx = signUpTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError) firebaseEx.ErrorCode;

                string message = "Log in failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing e-mail";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "E-mail is already in use";
                        break;
                }
                warningSignUpText.text = message;
            }
            else
            {
                user = signUpTask.Result;
                if (user != null)
                {
                    UserProfile profile = new UserProfile {DisplayName = _username};
                    var profileTask = user.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => profileTask.IsCompleted);
                    if (profileTask.Exception != null)
                    {
                        Debug.LogWarning(message:$"Failed to log in task with {profileTask.Exception}");
                        FirebaseException firebaseEx = profileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError) firebaseEx.ErrorCode;
                        warningSignUpText.text = "Username set failed!";
                    }
                    else
                    {
                        warningLogInText.text = "";
                    }
                }
            }
        }
    }
    //TODO: it will be called correctly, I could not make it work exactly.
    public string GetEmailUserId() {
        return user.UserId;
    }
}
