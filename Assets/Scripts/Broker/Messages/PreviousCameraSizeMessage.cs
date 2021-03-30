using UnityEngine;

namespace Broker.Messages {
    public class PreviousCameraSizeMessage {
        public readonly float size;
        
        public PreviousCameraSizeMessage(float size) {
            this.size = size;
        }
    }
}