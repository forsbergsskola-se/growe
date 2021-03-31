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
        feedback.GetComponent<FeedbackBehaviour>().SetValues(Color.green, "+" + gained);
    }
    public void ValueFeedbackDecrease(float lost)
    {
        var feedback = Instantiate(valueFeedbackObj, parent);
        feedback.GetComponent<FeedbackBehaviour>().SetValues(Color.red, "-" + lost);
    }
}
