using System;
using System.Collections.Generic;
using System.Linq;
using InventoryAndStore;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gacha
{
    [CreateAssetMenu(fileName = "DropTable", menuName = "Drop Table", order = 1)]
    public class DropTable : ScriptableObject
    {
        [SerializeField] public List<ItemSO> droppableItems;
        
        public ItemSO GetRandomItem()
        {
            float total = droppableItems.Sum(drop => drop.dropChance);
            float roll = Random.Range(0, total);

            foreach (var item in droppableItems)
            {
                float shinyRoll = Random.Range(0, 100);
                if (item.dropChance >= roll)
                {
                    if (shinyRoll >= 98)
                    {
                        item.isShiny = true;
                        return item;
                    }
                    else
                    {
                        return item;
                    }
                }
                roll -= item.dropChance;
            }

            throw new SystemException();
        }
    }
}