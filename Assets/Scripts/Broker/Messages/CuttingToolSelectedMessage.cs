
using UnityEngine;

namespace Broker.Messages {
    public class CuttingToolSelectedMessage
    {
        public bool setBool;
        public CuttingToolSelectedMessage(bool value) {
            setBool = value;
            Debug.Log("TEST");
        }
    }
}
