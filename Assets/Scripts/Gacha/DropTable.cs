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
                ItemSO clone = Instantiate(item);
                clone.isNew = true;
                float shinyRoll = Random.Range(0, 100);
                if (clone.dropChance >= roll)
                {
                    if (shinyRoll >= 98)
                    {
                        clone.isShiny = true;
                        return clone;
                    }
                }

                roll -= clone.dropChance;
            }

            throw new SystemException();
        }
    }
}