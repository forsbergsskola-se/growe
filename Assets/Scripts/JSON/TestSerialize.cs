using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using InventoryAndStore;
using Newtonsoft.Json;
using UnityEngine;

namespace JSON
{
    public class TestSerialize : MonoBehaviour
    {
        public string str = "";
        private static Currency Currency => FindObjectOfType<Currency>();
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                List<ItemClass> test = Inventories.Instance.playerInventory.items.Select(item => ConvertSO.SOToClass(item)).ToList();

                str = JsonConvert.SerializeObject(test, Formatting.Indented);
                var sr = File.CreateText(Application.dataPath + "/testJson.txt");
                sr.WriteLine (str);
                sr.Close();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                string text = File.ReadAllText(Application.dataPath + "/testJson.txt", Encoding.UTF8);
                List<ItemClass> newtest = new List<ItemClass>(JsonConvert.DeserializeObject<List<ItemClass>>(text));
                
                foreach (ItemClass itemClass in newtest) Inventories.Instance.playerInventory.Add(ConvertSO.ClassToSO(itemClass));
            }

            if (Input.GetKeyDown(KeyCode.D)) {
                if(Inventories.Instance.playerInventory.items[0] != null) 
                    Currency.AddItemForAuction(Inventories.Instance.playerInventory.items[0]);
            }
        }
    }
}
