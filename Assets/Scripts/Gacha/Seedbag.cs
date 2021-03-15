using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seedbag : MonoBehaviour
{
    public DropTable DropTable;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(DropTable.GetRandomItem().Item);
        }
    }
}