using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FeedbackBehaviour : MonoBehaviour
{
    private RectTransform _rectTransform => GetComponent<RectTransform>();
    private Text _text => GetComponent<Text>();
    private Vector2 AddedVector2Value;
    private Color _textColor;

    private void OnEnable()
    {
        _textColor = _text.color;
        AddedVector2Value = _rectTransform.anchoredPosition
                            + new Vector2(Random.Range(-Screen.width, Screen.width) , 
                                Random.Range(Screen.height, Screen.height * 3)) * 0.1f;
        Invoke(nameof(Destroy), 4);
    }

    void Update()
    {
        _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, AddedVector2Value, 1 * Time.deltaTime);
        
        _textColor.a -= 1 * Time.deltaTime;
        _text.color = _textColor;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
