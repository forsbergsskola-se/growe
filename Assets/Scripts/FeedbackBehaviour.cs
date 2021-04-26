using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FeedbackBehaviour : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Text _text;
    private Vector2 _addedVector2Value;
    private Color _textColor;

    public void SetValues(Color color, string text)
    {
        _rectTransform = GetComponent<RectTransform>();
        _text = GetComponent<Text>();
        _rectTransform.anchoredPosition = Input.mousePosition;
        _addedVector2Value = _rectTransform.anchoredPosition + new Vector2(Random.Range(-Screen.width, Screen.width), Random.Range(Screen.height, Screen.height * 3)) * 0.1f;
        _textColor = color;
        _text.text = text;
    }

    private void Update()
    {
        _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _addedVector2Value, 1 * Time.deltaTime);
        _textColor.a -= 1 * Time.deltaTime;
        _text.color = _textColor;
        
        if (_textColor.a <= 0) Destroy(gameObject);
    }
}
