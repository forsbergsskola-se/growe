using UnityEngine;

namespace Inventory
{
        [CreateAssetMenu(fileName = "New item", menuName = "Inventory/item")]
        public class ItemSO : ScriptableObject
        {
                public Sprite icon;
                public ItemType itemType;
                public Seedbag Seedbag;
                public enum ItemType { Seed, Plant, Cutting, Seedbag }
                public Rarity rarity;
                public enum Rarity { Survivor, Mediocre, Diva }
                public int maxAmount, compostValue, sellValue;
                public bool isShiny, hasLifeTime;
                public float lifeTimeHoursInInventory, survivability;
                public Vector2 sizeDimensions;
                public string itemLore;
        }
}
