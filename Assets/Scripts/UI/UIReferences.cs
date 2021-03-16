using System;
using Inventory;
using UnityEngine;

namespace UI
{
    public class UIReferences : MonoBehaviour
    {
        public ItemInfoData itemInfoBox;
        public static UIReferences Instance;

        private void Start()
        {
            Instance = this;
        }
    }
}
