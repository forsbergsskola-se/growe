using System.Collections;
using Broker;
using Broker.Messages;
using Firebase.Firestore;
using Saving;
using UnityEngine;

namespace TimeManager
{
    public class TimeManager : MonoBehaviour {
        [Header("Settings")]
        [Tooltip("How often game updates are sent to all time dependent entities"), Range(1,60)]
        public float updateInterval = 5f;
        [Tooltip("For testing purposes. Higher values makes the time pass faster")] 
        [SerializeField] private float timeMultiplier = 1f;
    
        //variables
        private WaitForSeconds waitTime;
        private bool gameIsRunning = true;
        private TimeData time;
    
        //references
        private IMessageBroker broker;
        private SaveManager saveManager;
    
        private void OnEnable()
        {
            saveManager = FindObjectOfType<SaveManager>();
            waitTime = new WaitForSeconds(updateInterval);
            broker = MessageBroker.Instance();
            StartCoroutine(KeepTime());
        }
    
        private IEnumerator KeepTime()
        {
            long timestampOld;
            long timestampCurrent;
            
            yield return new WaitForSeconds(1f);
            var dataTask = saveManager.LoadTime();
            yield return new WaitUntil(() => dataTask.IsCompleted);
            TimeData? timeData = dataTask.Result;
            if (timeData.HasValue) 
                timestampOld = (long) timeData.Value.timeOffsetInSeconds;
            else
            {
                Debug.LogWarning("Unable to load time, setting old time to current time", this);
                timestampOld = Timestamp.GetCurrentTimestamp().ToDateTimeOffset().ToUnixTimeSeconds();
            }
            
            timestampCurrent = Timestamp.GetCurrentTimestamp().ToDateTimeOffset().ToUnixTimeSeconds();
            CalculateAndSendDeltaTime(timestampOld, timestampCurrent);
        
            while (gameIsRunning)
            {
                timestampOld = Timestamp.GetCurrentTimestamp().ToDateTimeOffset().ToUnixTimeSeconds();
                yield return waitTime;
                timestampCurrent = Timestamp.GetCurrentTimestamp().ToDateTimeOffset().ToUnixTimeSeconds();
                CalculateAndSendDeltaTime(timestampOld, timestampCurrent);
            }
        }
    
        private void OnDisable()
        {
            Debug.Log("Time manager disabled");
            time.timeOffsetInSeconds = Timestamp.GetCurrentTimestamp().ToDateTimeOffset().ToUnixTimeSeconds();
            saveManager.SaveTime(time);
            //temporary testing->
            TimeData testTime;
            testTime.timeOffsetInSeconds = 1337;
            saveManager.SaveTime2(testTime);
        }

        private void CalculateAndSendDeltaTime(long timestampOld, long timestampCurrent)
        {
            float deltaTime = (timestampCurrent - timestampOld) * timeMultiplier;
            Debug.Log((timestampCurrent - timestampOld) * timeMultiplier);
            broker.Send(new TimePassedMessage(deltaTime));
        }
    }
}