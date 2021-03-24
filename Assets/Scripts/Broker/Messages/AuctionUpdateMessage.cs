namespace Broker.Messages {
    public class AuctionUpdateMessage {
        private readonly string _item;

        public AuctionUpdateMessage(string item) {
            this._item = item;
        }
    }
}