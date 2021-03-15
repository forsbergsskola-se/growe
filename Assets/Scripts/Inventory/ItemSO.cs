using UnityEngine;

namespace Inventory
{
        [CreateAssetMenu(fileName = "New item", menuName = "Inventory/item")]
        public class ItemSO : ScriptableObject
        {
                public string itemType;
                public Sprite icon;
                public Rarity rarity; // field
                public enum Rarity { Survivor, Mediocre, Diva, Mystery }
                
                public int maxAmount;
                public bool hasLifeTime;
                public float lifeTimeHoursInInventory;
                public Vector2 sizeDimensions;

                public string itemLore;
        }
}
