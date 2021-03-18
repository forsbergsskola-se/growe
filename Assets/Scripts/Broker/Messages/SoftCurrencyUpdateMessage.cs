namespace Broker.Messages {
    public class SoftCurrencyUpdateMessage {
        public readonly float amount;

        public SoftCurrencyUpdateMessage(float amount) {
            this.amount = amount;
        }
    }
}