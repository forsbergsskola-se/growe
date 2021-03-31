using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.UI;

public class AdsManagement : MonoBehaviour, IUnityAdsListener {
    [Tooltip("Text that shows error message when add is not ready")]
    public Text noAdText;
    [SerializeField, Tooltip("reward for watching ad")] 
    private float value = 20.0f;
    [SerializeField] private float adWaitTime = 300f;
    [SerializeField] private Button myButton;
    private float seconds = 0.0f;
    bool testMode = true;
    private Currency currency;
    
#if UNITY_IOS
        private string gameId = "4052122";
        string mySurfacingId_Interstitial = "Interstitial_iOS";
        string mySurfacingId_Reward = "Rewarded_iOS";
    #elif UNITY_ANDROID
        private string gameId = "4052123";
        string mySurfacingId_Reward = "Rewarded_Android";
        string mySurfacingId_Interstitial = "Interstitial_Android";
    #else
        private string gameId = "4052122";
        string mySurfacingId_Interstitial = "Interstitial_iOS";
        string mySurfacingId_Reward = "Rewarded_iOS";
    #endif

    void Start ()
    {
        currency = FindObjectOfType<Currency>();
        if (currency == null)
            Debug.Log("currency is null", this);
        if (noAdText == null)
            Debug.Log("no ad text is null", this);
        if (myButton == null)
            Debug.Log("advertisement button is null", this);
        myButton.interactable = Advertisement.IsReady(mySurfacingId_Reward);
        if (myButton) myButton.onClick.AddListener (ShowRewardedVideo);
        
        Advertisement.AddListener (this);
        Advertisement.Initialize(gameId, testMode);
        
        MessageBroker.Instance().SubscribeTo<TimePassedMessage>(TimePassed);
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
        if (seconds >= adWaitTime && Advertisement.IsReady(mySurfacingId_Reward)) {
            Debug.Log("Showing ad");
            noAdText.text = "";
            myButton.interactable = true;
            Advertisement.Show(mySurfacingId_Reward);
            seconds = 0;
        } 
        else if (seconds < adWaitTime)
        {
            noAdText.text = "Ads only available every 5 minutes.";
            Debug.Log("Gotta wait longer. current wait time: " + seconds + ", limit: " + adWaitTime);
        }
        else if (!Advertisement.IsReady(mySurfacingId_Reward))
        {
            Debug.Log("mySurfacingId_Reward is not ready");
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
            currency.AddSoftCurrency(value);
            Debug.Log ("awarded " + value + " amount of A-coins.");
        } else if (showResult == ShowResult.Skipped) {
            // No reward for you!
            Debug.Log ("Next time, don't skip! ;)");
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
    
    public void OnDestroy() {
        Advertisement.RemoveListener(this);
        MessageBroker.Instance().UnSubscribeFrom<TimePassedMessage>(TimePassed);
    }

    void TimePassed(TimePassedMessage m) {
        this.seconds += m.timePassed;
        myButton.interactable = Advertisement.IsReady(mySurfacingId_Reward) && seconds >= adWaitTime;
    }
}
