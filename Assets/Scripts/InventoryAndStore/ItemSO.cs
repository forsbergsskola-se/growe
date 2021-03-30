using System;
using Gacha;
using UnityEngine;
using UnityEngine.Events;

namespace InventoryAndStore
{
    [CreateAssetMenu(fileName = "New item", menuName = "Inventory/item")]
    public class ItemSO : ScriptableObject
    {
        
        /*
         
         
         
         
         
                    TODO VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
                    Hello fellow programmer. If you ad a new field for the scriptable object could you mark it with TODO ADD TO ConvertSO
        
        
        
        
        
        */
        public Sprite icon;
        public Sprite cuttingIcon;
        public Sprite[] growthStageSprites = new Sprite[5];
        public TradeState tradeState;
        public enum TradeState { Sellable, Buyable, SoulBound }
        public ItemType itemType;
        public Seedbag seedbag;
        public enum ItemType { Seed, Plant, Cutting, Seedbag }
        public Rarity rarity = Rarity.Survivor;
        
        // 170 = one second from left to right(metronome)
        public enum Rarity { Survivor = 85, Mediocre = 170, Diva = 340 }
        public int maxAmount = 1;
        public int compostValue, sellValue, buyValue;
        public bool isShiny, hasLifeTime;
        public float lifeTimeHoursInInventory, survivability, dropChance;
        public Vector2 sizeDimensions;
        public string itemLore;

        public bool isNew;

        public enum GrowthStage { Seed, Cutting, Sapling, Growing, Mature}
        public float growthDuration; // exv 28800 sek
        private GrowthStage _currentGrowthStage;
        public GrowthStage CurrentGrowthStage
        {
            get => _currentGrowthStage;
            set
            {
                _currentGrowthStage = value;
                UpdateSpriteEvent?.Invoke();
            }
        }        [HideInInspector] public float currentGrowthProgress = 0;
                 [HideInInspector] public bool isFertilized;

        [HideInInspector] public int timesCut = 0;
        public Action UpdateSpriteEvent;
    }
}
