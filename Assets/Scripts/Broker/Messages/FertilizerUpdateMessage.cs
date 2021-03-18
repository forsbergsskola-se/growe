namespace Broker.Messages {
    public class FertilizerUpdateMessage {
        public readonly int amount;

        public FertilizerUpdateMessage(int amount) {
            this.amount = amount;
        }
    }
}