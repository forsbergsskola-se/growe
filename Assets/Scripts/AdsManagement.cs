using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManagement : MonoBehaviour, IUnityAdsListener {
    
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

    void Start () {
        Advertisement.AddListener (this);
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowInterstitialAd() {
        if (Advertisement.IsReady(mySurfacingId_Interstitial)) {
            Advertisement.Show(mySurfacingId_Interstitial);
        } 
        else {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
    
    public void ShowRewardedVideo() {
        if (Advertisement.IsReady(mySurfacingId_Reward)) {
            Advertisement.Show(mySurfacingId_Reward);
        } 
        else {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }
    
    public void OnUnityAdsReady (string surfacingId) {
        // If the ready Ad Unit or legacy Placement is rewarded, activate the button: 
        if (surfacingId == mySurfacingId_Reward) {  
            //todo enable rewards
        }
    }

    public void OnUnityAdsDidFinish (string surfacingId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            // Reward the user for watching the ad to completion.
            Debug.Log ("X amount of A-coins.");
        } else if (showResult == ShowResult.Skipped) {
            // Do not reward the user for skipping the ad.
            Debug.Log ("Next time, don't skip to get X amount of A-coins! ;)");
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
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
