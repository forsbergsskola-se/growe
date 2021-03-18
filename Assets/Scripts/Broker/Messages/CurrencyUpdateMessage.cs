namespace Broker.Messages {
    public class CurrencyUpdateMessage {
        public readonly float currency;

        public CurrencyUpdateMessage(float currency) {
            this.currency = currency;
        }
    }
}