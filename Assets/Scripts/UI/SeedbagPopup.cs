using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventoryAndStore;

public class SeedbagPopup : MonoBehaviour
{
    public GameObject popup;

    [Space]
    public Image[] itemSprites = new Image[3];
    public Text[] itemNames = new Text[3];

    public void UpdateItemDisplay(ItemSO[] items)
    {
        for (int i = 0; i < itemSprites.Length; i++)
        {
            itemSprites[i].sprite = items[i].icon;
            itemNames[i].text = items[i].name;
        }
    }

    public void ClosePopup()
    {
        popup.SetActive(false);
    }
}
