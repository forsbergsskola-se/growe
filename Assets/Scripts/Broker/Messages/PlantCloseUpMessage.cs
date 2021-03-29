using InventoryAndStore;
using UnityEngine;

namespace Broker.Messages {
    public class PlantCloseUpMessage {
        public readonly ItemSO plant;
        public readonly GameObject plantParentObject;

        public PlantCloseUpMessage(ItemSO plant, GameObject plantParentObject) {
            this.plant = plant;
            this.plantParentObject = plantParentObject;
        }
    }
}