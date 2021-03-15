using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seedbag : MonoBehaviour
{
    public int price;
    public GachaUI GachaUI;


    public DropTable DropTable;

    private void Start()
    {
    }

    public void gachaRoll()
    {
        GachaUI.droppedItem.text = DropTable.GetRandomItem().Item;
    }
}