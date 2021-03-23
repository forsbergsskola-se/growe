using System.Collections.Generic;
using System.Data.SqlClient;
using InventoryAndStore;
using UnityEditor;
using UnityEngine;

namespace JSON
{
    public static class ConvertSO
    {
        public static ItemClass SOToClass(ItemSO itemSo)
        {
            ItemClass clone = new ItemClass();
            
            clone.SeedBagDropTable = new List<ItemClass>();
            if (itemSo.itemType == ItemSO.ItemType.Seedbag)
                foreach (var droppableItem in itemSo.seedbag.Items.droppableItems) 
                    clone.SeedBagDropTable.Add(SOToClass(droppableItem));

            clone.Name = itemSo.name;
            clone.IconPath = AssetDatabase.GetAssetPath(itemSo.icon);
            clone.TradeState = itemSo.tradeState;
            clone.ItemType = itemSo.itemType;
            clone.Rarity = itemSo.rarity;
            clone.MAXAmount = itemSo.maxAmount;
            clone.CompostValue = itemSo.compostValue;
            clone.SellValue = itemSo.sellValue;
            clone.BuyValue = itemSo.buyValue;
            clone.IsShiny = itemSo.isShiny;
            clone.HasLifeTime = itemSo.hasLifeTime;
            clone.LifeTimeHoursInInventory = itemSo.lifeTimeHoursInInventory;
            clone.Survivability = itemSo.survivability;
            clone.SizeDimensions = itemSo.sizeDimensions;
            clone.ItemLore = itemSo.itemLore;
            return clone;
        }
        public static ItemSO ClassToSO(ItemClass itemClass)
        {
            var clone = ScriptableObject.CreateInstance<ItemSO>();
            
            
            clone.seedbag.Items.droppableItems = new List<ItemSO>();
            if (clone.seedbag.Items.droppableItems != null)
            {
                Debug.Log("ListCreated");
            }
            
            if (itemClass.ItemType == ItemSO.ItemType.Seedbag)
                foreach (var droppableItem in itemClass.SeedBagDropTable) 
                    clone.seedbag.Items.droppableItems.Add(ClassToSO(droppableItem));
            
            clone.name = itemClass.Name;
            clone.icon = (Sprite)AssetDatabase.LoadAssetAtPath(itemClass.IconPath, typeof(Sprite));
            clone.tradeState = itemClass.TradeState;
            clone.itemType = itemClass.ItemType;
            clone.rarity = itemClass.Rarity;
            clone.maxAmount = itemClass.MAXAmount;
            clone.compostValue = itemClass.CompostValue;
            clone.sellValue = itemClass.SellValue;
            clone.buyValue = itemClass.BuyValue;
            clone.isShiny = itemClass.IsShiny;
            clone.hasLifeTime = itemClass.HasLifeTime;
            clone.lifeTimeHoursInInventory = itemClass.LifeTimeHoursInInventory;
            clone.survivability = itemClass.Survivability;
            clone.sizeDimensions = itemClass.SizeDimensions;
            clone.itemLore = itemClass.ItemLore;
            return clone;
        }
    }

    public class ItemClass
    {
        public ItemSO.TradeState TradeState;
        public ItemSO.ItemType ItemType;
        public ItemSO.Rarity Rarity;
        public List<ItemClass> SeedBagDropTable;
        public int MAXAmount, CompostValue, SellValue, BuyValue;
        public bool IsShiny, HasLifeTime;
        public float LifeTimeHoursInInventory, Survivability;
        public Vector2 SizeDimensions;
        public string ItemLore, Name, IconPath;
    }
}