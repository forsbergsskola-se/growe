using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CuttingTool : MonoBehaviour {
    [HideInInspector] public bool isCutting;
    
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

        if (Physics.Raycast(ray, out var hit)) {
            //Debug.Log(hit.collider.gameObject.name);
            //Debug.Log("did hit");
                    
            //TODO: REPLACE ABOVE WITH:
            //If we hit plant, getcomponent<SO>() and check for GrowthStage
            //
            //Chance for plant to die when cut.
            //Add a count to the Plant, if the count reaches 4 cuts, the plant die.
        }
        isCutting = false;
    }

    void MobileCut() {
        throw new NotImplementedException();   
    }
}
