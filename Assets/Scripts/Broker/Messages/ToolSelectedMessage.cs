using UnityEngine;

namespace Broker.Messages {
    public class ToolSelectedMessage {
        public readonly bool toolSelected;

        public ToolSelectedMessage(bool toolSelected) {
            this.toolSelected = toolSelected;
            Debug.Log("toolSelected: " + this.toolSelected + " " + this);
        }
    }
}