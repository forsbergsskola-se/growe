using System;
using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEditor.VersionControl;
using UnityEngine;

public class GridItem : MonoBehaviour {
    public ItemSO item;

    //Dry, Getting Dry, Moist, Watered, Over watered.
    private SoilStages currentSoilStage;

    private enum SoilStages {
        Dry = 0,
        GettingDry = 1,
        Moist = 2,
        Watered = 3,
        OverWatered = 4
    }

    public float soilStageTimerSeconds = 28800f;


    public void Init(ItemSO item) {
        this.item = item;
        this.item.soilStageTimer = soilStageTimerSeconds;
        currentSoilStage = SoilStages.Dry;
        UpdateSprite();
    }

    private void Start() {
        MessageBroker.Instance().SubscribeTo<TimePassedMessage>(TimePassed);
    }

    void TimePassed(TimePassedMessage m) {
        Debug.Log("TimePassed Method");
        item.soilStageTimer -= m.timePassed;
        if (item.soilStageTimer <= 0) {
            item.soilStageTimer = soilStageTimerSeconds;
            if (currentSoilStage == 0)
                return;
            else {
                currentSoilStage -= 1;
                UpdateSprite();
            }

            Debug.Log(currentSoilStage);
        }
    }

    void UpdateSprite() {
        //TODO Insert Update sprite according to growth and soil stage.
    }

    //TODO Hookup Watering mini game.
    public void ChangeSoilStage(int amount) {
        currentSoilStage += amount;
        item.soilStageTimer = soilStageTimerSeconds;
        if ((int) currentSoilStage > 5)
            currentSoilStage = SoilStages.OverWatered;
        UpdateSprite();
    }
}