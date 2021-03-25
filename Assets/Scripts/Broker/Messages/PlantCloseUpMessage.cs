using InventoryAndStore;

namespace Broker.Messages {
    public class PlantCloseUpMessage {
        public readonly ItemSO item;

        public PlantCloseUpMessage(ItemSO item) {
            this.item = item;
        }
    }
}