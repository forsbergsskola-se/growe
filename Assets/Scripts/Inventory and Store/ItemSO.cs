using UnityEngine;

namespace Inventory_and_Store
{
        [CreateAssetMenu(fileName = "New item", menuName = "Inventory/item")]
        public class ItemSO : ScriptableObject
        {
                public Sprite icon;
                public TradeState tradeState;
                public enum TradeState { Sellable, Buyable, SoulBound }
                public ItemType itemType;
                public enum ItemType { Seed, Plant, Cutting }
                public Rarity rarity;
                public enum Rarity { Survivor, Mediocre, Diva }
                public int maxAmount, compostValue, sellValue, buyValue;
                public bool isShiny, hasLifeTime;
                public float lifeTimeHoursInInventory, survivability;
                public Vector2 sizeDimensions;
                public string itemLore;
        }
}
