using InventoryAndStore;
using UnityEngine;

namespace Broker.Messages {
    public class PlantCloseUpMessage {
        public readonly ItemSO plant;
        public readonly GridObject plantParentObject;

        public PlantCloseUpMessage(ItemSO plant, GridObject plantParentObject) {
            this.plant = plant;
            this.plantParentObject = plantParentObject;
        }
    }
}