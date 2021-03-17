using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class seedBagOpen : MonoBehaviour
{
    public Seedbag Seedbag;
    

    public void openSeedbag()
    {
      //  if()
        Seedbag.Open(amount: 3);
      //  Inventory.Inventory.Remove();
    }
}