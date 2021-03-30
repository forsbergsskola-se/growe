using System;
using System.Collections.Generic;
using Gacha;
using InventoryAndStore;
using UnityEditor;
using UnityEngine;

namespace JSON
{
    public static class ConvertSO
    {
        public static ItemClass SOToClass(ItemSO itemSo)
        {
            ItemClass clone = new ItemClass {SeedBagDropTable = new List<ItemClass>()};

            if (itemSo.itemType == ItemSO.ItemType.Seedbag)
                foreach (var droppableItem in itemSo.seedbag.items.droppableItems) 
                    clone.SeedBagDropTable.Add(SOToClass(droppableItem));

            clone.IsFertilized = itemSo.isFertilized;
            clone.CurrentGrowthProgress = itemSo.currentGrowthProgress;
            clone.GrowthStage = itemSo.CurrentGrowthStage;
            clone.TradeState = itemSo.tradeState;
            clone.ItemType = itemSo.itemType;
            Debug.Log("rarity test SOToClass: " + (int)itemSo.rarity);
            clone.Rarity = itemSo.rarity;
            clone.MAXAmount = itemSo.maxAmount;
            clone.CompostValue = itemSo.compostValue;
            clone.SellValue = itemSo.sellValue;
            clone.BuyValue = itemSo.buyValue;
            clone.IsShiny = itemSo.isShiny;
            clone.HasLifeTime = itemSo.hasLifeTime;
            clone.LifeTimeHoursInInventory = itemSo.lifeTimeHoursInInventory;
            clone.Survivability = itemSo.survivability;
            clone.DropChance = itemSo.dropChance;
            clone.SizeDimensions = itemSo.sizeDimensions;
            clone.ItemLore = itemSo.itemLore;
            clone.Name = itemSo.name;
            clone.TimesCut = itemSo.timesCut;
            clone.CuttingIconPath = AssetDatabase.GetAssetPath(itemSo.cuttingIcon);
            clone.IconPath = AssetDatabase.GetAssetPath(itemSo.icon);
            if (itemSo.itemType == ItemSO.ItemType.Seedbag) return clone;
            for (int i = 0; i < itemSo.growthStageSprites.Length; i++)
                clone.GrowthStageSprites[i] = AssetDatabase.GetAssetPath(itemSo.growthStageSprites[i]);
            
            return clone;
        }
        public static ItemSO ClassToSO(ItemClass itemClass)
        {
            var clone = ScriptableObject.CreateInstance<ItemSO>();
            
            if (itemClass.ItemType == ItemSO.ItemType.Seedbag) {
                clone.seedbag = new Seedbag {items = ScriptableObject.CreateInstance<DropTable>()};
                clone.seedbag.items.droppableItems = new List<ItemSO>();

                foreach (var droppableItem in itemClass.SeedBagDropTable)
                    clone.seedbag.items.droppableItems.Add(ClassToSO(droppableItem));
            }
            
            clone.isFertilized = itemClass.IsFertilized;
            clone.currentGrowthProgress = itemClass.CurrentGrowthProgress;
            clone.CurrentGrowthStage = itemClass.GrowthStage;
            clone.tradeState = itemClass.TradeState;
            clone.itemType = itemClass.ItemType;
            Debug.Log("rarity test ClassToSO: " + (int)itemClass.Rarity);
            clone.rarity = itemClass.Rarity;
            clone.maxAmount = itemClass.MAXAmount;
            clone.compostValue = itemClass.CompostValue;
            clone.sellValue = itemClass.SellValue;
            clone.buyValue = itemClass.BuyValue;
            clone.isShiny = itemClass.IsShiny;
            clone.hasLifeTime = itemClass.HasLifeTime;
            clone.lifeTimeHoursInInventory = itemClass.LifeTimeHoursInInventory;
            clone.survivability = itemClass.Survivability;
            clone.dropChance = itemClass.DropChance;
            clone.sizeDimensions = itemClass.SizeDimensions;
            clone.itemLore = itemClass.ItemLore;
            clone.name = itemClass.Name;
            clone.timesCut = itemClass.TimesCut;
            clone.cuttingIcon = (Sprite)AssetDatabase.LoadAssetAtPath(itemClass.CuttingIconPath, typeof(Sprite));
            clone.icon = (Sprite)AssetDatabase.LoadAssetAtPath(itemClass.IconPath, typeof(Sprite));
            for (int i = 0; i < itemClass.GrowthStageSprites.Length; i++) 
                clone.growthStageSprites[i] = (Sprite)AssetDatabase.LoadAssetAtPath(itemClass.GrowthStageSprites[i], typeof(Sprite));
            

            return clone;
        }
    }

    public class ItemClass
    {
        public ItemSO.TradeState TradeState;
        public ItemSO.ItemType ItemType;
        public ItemSO.Rarity Rarity;
        public ItemSO.GrowthStage GrowthStage;
        public List<ItemClass> SeedBagDropTable;
        public int MAXAmount, CompostValue, SellValue, BuyValue, TimesCut;
        public bool IsShiny, HasLifeTime, IsFertilized;
        public float LifeTimeHoursInInventory, Survivability, DropChance,
            CurrentGrowthProgress;
        public Vector2 SizeDimensions;
        public string ItemLore, Name, IconPath, CuttingIconPath;
        public string[] GrowthStageSprites = new string[5];
    }
}