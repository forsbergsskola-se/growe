using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;   
using Firebase.Extensions;

public class FirebaseEmailAuthentication : MonoBehaviour
{
    [Header("Log In UI")] 
    [SerializeField] private InputField logInMail;
    [SerializeField] private InputField logInPassword;
    
    [Header("Sign In UI")] 
    [SerializeField] private InputField signInMail;
    [SerializeField] private InputField signInPassword;
    [SerializeField] private InputField signInPasswordCheck;

    FirebaseAuth auth;
    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    void Start()
    {
        auth.StateChanged += AuthStateChange; // If it is already signed in and logged in, directly goes to game scene
        if (auth.CurrentUser != null)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void AuthStateChange(object sender, EventArgs e)
    {
        if (auth.CurrentUser != null)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void SignIn()
    {
        if (SignInDataCheck ())
        {
            auth.CreateUserWithEmailAndPasswordAsync(signInMail.text, signInPassword.text).ContinueWithOnMainThread(
                task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.Log("Cancelled");
                        return;
                        ;
                    }

                    if (task.IsFaulted)
                    {
                        Debug.LogError("Creating user with mail & password encountered an error" + task.Exception);
                        return;
                    }

                    Firebase.Auth.FirebaseUser newUser = task.Result;
                    Debug.LogFormat("Firebase user created successfully : {0} {1}", newUser.DisplayName,
                        newUser.UserId);
                });
        }
        else
        {
            Debug.LogWarning("Wrong inputs!!!");
        }
    }

    bool SignInDataCheck() //warning for required input areas
    {
        if (string.IsNullOrEmpty(signInMail.text))
        {
            return false;
        }

        if (string.IsNullOrEmpty(signInPassword.text) || string.IsNullOrEmpty(signInPasswordCheck.text))
        {
            return false;
        }
        if (string.IsNullOrEmpty(signInPasswordCheck.text))
        {
            return false;
        }
        return true;
    }

    public void MemberLogIn()
    {
        if (SignInDataCheck())
        {
            auth.SignInWithEmailAndPasswordAsync(logInMail.text, logInPassword.text).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("Signing in with e-mail & password was cancelled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("Signing in with e-mail & password encountered an error." + task.Exception);
                    return;
                }

                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} {1}", newUser.DisplayName, newUser.UserId);
            });
        }
    }

    bool LogInDataControl()
    {
        if (string.IsNullOrEmpty(logInMail.text))
        {
            return false;
        }
        if (string.IsNullOrEmpty(logInPassword.text))
        {
            return false;
        }
        return true;
    }
}
