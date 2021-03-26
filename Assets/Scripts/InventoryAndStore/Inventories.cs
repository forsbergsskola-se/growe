using Broker;
using Broker.Messages;
using UnityEngine;

namespace InventoryAndStore
{
    public class Inventories : MonoBehaviour
    {
        public Inventory playerInventory, storeInventory;

        public static Inventories Instance;

        private void Awake()
        {
            MessageBroker.Instance().SubscribeTo<InventoryUpdateMessage>(UpdateInventory);
            Instance = this;
        }

        void UpdateInventory(InventoryUpdateMessage firebaseData)
        {
            playerInventory.Add(firebaseData.Inventory);
            MessageBroker.Instance().UnSubscribeFrom<InventoryUpdateMessage>(UpdateInventory);
        }
    }
}
