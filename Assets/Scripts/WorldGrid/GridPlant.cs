using System;
using Broker;
using Broker.Messages;
using InventoryAndStore;
using UnityEngine;

namespace WorldGrid {
    public class GridPlant : MonoBehaviour {
    
        [Header("Settings")]
        [Tooltip("Controls how long soil stages last")]
        public float soilStageDuration = 28800f;
    
        // variables
        public float soilStageProgress;
        public SoilStage currentSoilStage;
        //references
        public ItemSO plant;
        public SpriteRenderer plantSpriteRenderer;
        private Grid grid;
    
        public enum SoilStage { Dry, GettingDry, Moist, Watered, OverWatered }

        public void Init(ItemSO plant, Grid grid) {
            this.plant = plant;
            this.grid = grid;
            //MessageBroker.Instance().SubscribeTo<UpdateSpriteMessage>(UpdateSprite);
            plant.UpdateSpriteEvent += UpdateSprite;
            switch (this.plant.itemType)
            { 
                case ItemSO.ItemType.Seed:
                    plant.CurrentGrowthStage = ItemSO.GrowthStage.Seed;
                    break;
                case ItemSO.ItemType.Cutting:
                    plant.CurrentGrowthStage = ItemSO.GrowthStage.Cutting;
                    break;
            }
            plant.itemType = ItemSO.ItemType.Plant;
        
            soilStageProgress = soilStageDuration;
            currentSoilStage = SoilStage.Dry;
            UpdateSprite();
        }

        public void InitFromSave(ItemSO plant, Grid grid)
        {
            this.plant = plant;
            this.grid = grid;
            plant.UpdateSpriteEvent += UpdateSprite;
            UpdateSprite();
        }

        private void Start() {
            MessageBroker.Instance().SubscribeTo<TimePassedMessage>(TimePassed);
        }
    
        void TimePassed(TimePassedMessage m) {
            float deltaTime = m.timePassed;
            UpdateSoilStage(deltaTime);
            UpdateGrowthStage(deltaTime);
        }

        private void UpdateGrowthStage(float deltaTime)
        {
            float fertilizationBonus = plant.isFertilized ? 2.0f : 1.0f; 
            switch (currentSoilStage)
            {
                case SoilStage.Dry:
                    return;
                case SoilStage.GettingDry:
                    plant.currentGrowthProgress += deltaTime * 0.5f * fertilizationBonus;
                    break;
                case SoilStage.Moist:
                    plant.currentGrowthProgress += deltaTime * fertilizationBonus;
                    break;
                case SoilStage.Watered:
                    plant.currentGrowthProgress += deltaTime * 0.8f * fertilizationBonus;
                    break;
                case SoilStage.OverWatered:
                    return;
            }

            if (plant.currentGrowthProgress >= plant.growthDuration)
            {
                if (plant.CurrentGrowthStage != ItemSO.GrowthStage.Mature)
                {
                    plant.CurrentGrowthStage += 1;
                    plant.isFertilized = false;
                    UpdateSprite();
                    SaveGrid();
                }
                plant.currentGrowthProgress = 0;
            }
        }

        private void UpdateSoilStage(float deltaTime)
        {
            soilStageProgress -= deltaTime;
            if (soilStageProgress <= 0)
            {
                soilStageProgress = soilStageDuration;
                if (currentSoilStage == 0)
                    return;
            
                currentSoilStage -= 1;
                UpdateSprite();
                SaveGrid();
            }
        }

        void UpdateSprite()
        {
            plantSpriteRenderer.sprite = plant.growthStageSprites[(int)plant.CurrentGrowthStage];
        }
    
        public void ChangeSoilStage(int amount) {
            if ((int) currentSoilStage + amount > Enum.GetValues(typeof(SoilStage)).Length)
                currentSoilStage = SoilStage.OverWatered;
            else currentSoilStage += amount;
        
            soilStageProgress = soilStageDuration;
            UpdateSprite();
            SaveGrid();
        }

        private void OnDisable()
        {
            plant.UpdateSpriteEvent -= UpdateSprite;
            MessageBroker.Instance().UnSubscribeFrom<TimePassedMessage>(TimePassed);
        }

        private void SaveGrid()
        {
            grid.SaveGridDataToDatabase();
        }
    }
}
