using System.Collections.Generic;
using JSON;

namespace Broker.Messages {
    public class InventoryUpdateMessage {
        public readonly List<ItemClass> Inventory;

        public InventoryUpdateMessage(List<ItemClass> Inventory) {
            this.Inventory = Inventory;
        }
    }
}