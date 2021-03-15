using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;

public class GachaUI : MonoBehaviour
{
    public Text droppedItem;
    public GameObject Panel;
    public Seedbag Seedbag;

    void Awake()
    {
        Panel.SetActive(false);
    }

    public void ActivatePanel()
    {
        Panel.SetActive(true);
    }


    void Update()
    {
    }
}