using InventoryAndStore;
using UnityEngine;
using WorldGrid;

namespace Broker.Messages {
    public class PlantCloseUpMessage {
        public readonly ItemSO plant;
        public readonly GridMoveObject plantParentObject;

        public PlantCloseUpMessage(ItemSO plant, GridMoveObject plantParentObject) {
            this.plant = plant;
            this.plantParentObject = plantParentObject;
        }
    }
}