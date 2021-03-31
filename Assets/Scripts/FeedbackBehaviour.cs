using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FeedbackBehaviour : MonoBehaviour
{
    private RectTransform _rectTransform => GetComponent<RectTransform>();
    private Text _text => GetComponent<Text>();
    private Vector2 AddedVector2Value;
    private Color _textColor;
    private bool IsReady;

    public void SetValues(Color color, string text)
    {
        _rectTransform.anchoredPosition = Input.mousePosition;
        AddedVector2Value = _rectTransform.anchoredPosition
                            + new Vector2(Random.Range(-Screen.width, Screen.width) , 
                                Random.Range(Screen.height, Screen.height * 3)) * 0.1f;
        _text.color = color;
        _textColor = _text.color;
        _text.text = text;
        
        IsReady = true;
        Invoke(nameof(Destroy), 4);
    }

    void Update()
    {
        if (IsReady)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, AddedVector2Value, 1 * Time.deltaTime);
            _textColor.a -= 1 * Time.deltaTime;
            _text.color = _textColor;
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
