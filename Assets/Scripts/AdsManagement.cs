using System.Collections;
using System.Collections.Generic;
using Inventory_and_Store;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class AdsManagement : MonoBehaviour, IUnityAdsListener {
    
    [SerializeField] public float value;
    [SerializeField] public float seconds;
    public Text noadtext;
    
    #if UNITY_IOS
        private string gameId = "4052122";
        string mySurfacingId_Interstitial = "Interstitial_iOS";
        string mySurfacingId_Reward = "Rewarded_iOS";
    #elif UNITY_ANDROID
        private string gameId = "4052123";
        string mySurfacingId_Reward = "Rewarded_Android";
        string mySurfacingId_Interstitial = "Interstitial_Android";
    #endif
    bool testMode = true;
    private Button myButton;

    void Start ()
    {
        myButton = GetComponent<Button>();
        myButton.interactable = Advertisement.IsReady(mySurfacingId_Reward);
        if (myButton) myButton.onClick.AddListener (ShowRewardedVideo);
        Advertisement.AddListener (this);
        Advertisement.Initialize(gameId, testMode);
    }

    //designers wanted rewarded video only
    public void ShowInterstitialAd() {
        if (Advertisement.IsReady(mySurfacingId_Interstitial)) {
            Advertisement.Show(mySurfacingId_Interstitial);
        } 
        else {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
    
    public void ShowRewardedVideo() {
        if (Advertisement.IsReady(mySurfacingId_Reward) && seconds >= 300) {
            noadtext.text = "";
            myButton.interactable = true;
            Advertisement.Show(mySurfacingId_Reward);
            seconds = 0;
        } 
        else
        {
            noadtext.text = "Ads available every 5 minutes only!";
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }
    
    public void OnUnityAdsReady (string surfacingId) {
        // If the ready Ad Unit or legacy Placement is rewarded, activate the button: 
        if (surfacingId == mySurfacingId_Reward) {  
            //no legacy
        }
    }

    public void OnUnityAdsDidFinish (string surfacingId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            Currency reward = gameObject.AddComponent<Currency>();
            reward.AddSoftCurrency(value);
            Debug.Log ("20 amount of A-coins.");
            Debug.Log (value);
        } else if (showResult == ShowResult.Skipped) {
            // No reward for you!
            Debug.Log ("Next time, don't skip! ;)");
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void Update()
    {
        seconds += Time.deltaTime;
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string surfacingId) {
        // Optional actions to take when the end-users triggers an ad.
    }
    
    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy() {
        Advertisement.RemoveListener(this);
    }
}
