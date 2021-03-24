namespace Broker.Messages
{
    public class TimePassedMessage
    {
        public readonly float timePassed;

        public TimePassedMessage(float timePassed) {
            this.timePassed = timePassed;
        }
    }
}
