using UnityEngine;

namespace Inventory
{
        [CreateAssetMenu(fileName = "New item", menuName = "Inventory/item")]
        public class ItemSO : ScriptableObject
        {
                public Sprite icon;
                public ItemType itemType;
                public enum ItemType { Seed, Plant, Cutting }
                public Rarity rarity; // field
                public enum Rarity { Survivor, Mediocre, Diva }
                public int maxAmount;
                public bool isShiny, hasLifeTime;
                public float lifeTimeHoursInInventory;
                public Vector2 sizeDimensions;
                public string itemLore;
        }
}
