using InventoryAndStore;

namespace Broker.Messages {
    public class AuctionUpdateMessage {
        public readonly string item;

        public AuctionUpdateMessage(string item) {
            this.item = item;
        }
    }
}