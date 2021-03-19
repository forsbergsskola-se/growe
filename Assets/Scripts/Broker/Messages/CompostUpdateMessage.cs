namespace Broker.Messages {
    public class CompostUpdateMessage {
        public readonly int amount;

        public CompostUpdateMessage(int amount) {
            this.amount = amount;
        }
    }
}