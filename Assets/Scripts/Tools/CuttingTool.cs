using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class CuttingTool : MonoBehaviour {
    [HideInInspector] public bool isCutting;
    
    //Temporary values
    int growthStage = 5, survivability = 50, timesCutten = 0;

    public void Cancel() {
        //TODO: Add cancel button.
        //Should only set isCutting to false, should be called from the CancelBtn later.....       
    }
    
    void Update() {
        if (Input.touchSupported)
            MobileCut();
        else
            PcCut();
    }

    void PcCut() {
        if (!isCutting) return;
        //Debug.Log("Started cutting");
        if (!Input.GetMouseButtonDown(0)) return;
        //Debug.Log("Pressed mouse 0");
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out var hit) && growthStage >= 4) {
            //TODO WHEN ASSET IS DONE: Add a filter so we can only press the plant ALSO replace temp variables with the real ones:

            var shouldDie = Random.Range(0, 100);
            if (shouldDie >= survivability || timesCutten >= 4) {
                //Kill Plant
                Debug.Log($"Plant died : Rolled {shouldDie} out of 100 , Plant was cutten {timesCutten} times.");
            }
            else {
                growthStage = 3;
                timesCutten += 1;
                //TODO: Add cuttings to inventory here.
                
                Debug.Log($"Plant succesfully cutted, current Growth Stage = {growthStage} and Times plant was cutted = {timesCutten}");
            }
        }
        isCutting = false;
    }

    void MobileCut() {
        throw new NotImplementedException();   
    }
}
