using System;
using Gacha;
using UnityEngine;

namespace InventoryAndStore
{
    [CreateAssetMenu(fileName = "New item", menuName = "Inventory/item")] 
    public class ItemSO : ScriptableObject
    {
        public Sprite icon;
        public TradeState tradeState;
        public enum TradeState { Sellable, Buyable, SoulBound } 
        public ItemType itemType; 
        public Seedbag seedbag;
        public enum ItemType { Seed, Plant, Cutting, Seedbag } 
        public Rarity rarity;
        public enum Rarity { Survivor, Mediocre, Diva }
        public int maxAmount, compostValue, sellValue, buyValue, timesCut, growthStage;
        public bool isShiny, hasLifeTime;
        public float lifeTimeHoursInInventory, survivability, dropChance;
        public Vector2 sizeDimensions;
        public string itemLore;
    }
}
