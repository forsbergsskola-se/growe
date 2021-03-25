using System.Collections.Generic;
using JSON;

namespace Broker.Messages {
    public class InventoryUpdateMessage {
        private readonly List<ItemClass> _inventory;

        public InventoryUpdateMessage(List<ItemClass> inventory) {
            _inventory = inventory;
        }
    }
}