using System;
using System.Collections;
using System.Collections.Generic;
using InventoryAndStore;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ValueChangedFeedback : MonoBehaviour
{
    public static ValueChangedFeedback instance;
    public GameObject valueFeedbackObj;
    public Transform parent;

    private void Start()
    {
        instance = this;
    }

    public void ValueFeedbackAdd(float gained)
    {
        var feedback = Instantiate(valueFeedbackObj, parent);
        feedback.GetComponent<Text>().text = "+" + gained;
        feedback.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
    public void ValueFeedbackDecrease(float lost)
    {
        var feedback = Instantiate(valueFeedbackObj, parent);
        feedback.GetComponent<Text>().text = "-" + lost;
        feedback.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
}
