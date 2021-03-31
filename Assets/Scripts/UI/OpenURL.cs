using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{


    public void Open()
    {
        Debug.Log("Open");
        Application.OpenURL(Application.persistentDataPath+ "/index.html");
    }
}
