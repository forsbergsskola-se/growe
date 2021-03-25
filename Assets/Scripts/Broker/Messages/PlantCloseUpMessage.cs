using InventoryAndStore;

namespace Broker.Messages {
    public class PlantCloseUpMessage {
        public readonly ItemSO plant;

        public PlantCloseUpMessage(ItemSO plant) {
            this.plant = plant;
        }
    }
}