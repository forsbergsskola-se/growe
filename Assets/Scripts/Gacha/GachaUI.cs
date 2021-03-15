using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;

public class GachaUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Text droppedItem;
    public GameObject Panel;

    [Header("Test Seedbag Data")]
    public Seedbag Seedbag;

    void Awake()
    {
        Panel.SetActive(false);
    }

    public void ActivatePanel()
    {
        Panel.SetActive(true);
    }

    public void TestGacha()
    {
        droppedItem.text = "You got the " + Seedbag.Open();
    }
}