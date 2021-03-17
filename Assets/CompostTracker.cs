using UnityEngine;
using UnityEngine.UI;

public class CompostTracker : MonoBehaviour {
    public Slider leftSlider;
    public Slider rightSlider;
    public int maxValue = 15;
    public int maxAddAmount = 3;
    public int minAddAmount = 2;
    int _compostValue;

    void Start() {
        leftSlider.minValue = 0;
        rightSlider.minValue = 0;
        leftSlider.maxValue = maxValue;
        rightSlider.maxValue = maxValue;
    }

    public void AddCompost(int compostAmount) {
        compostAmount = Mathf.Clamp(compostAmount, minAddAmount, maxAddAmount);
        _compostValue += compostAmount;
        
        if (_compostValue > maxValue) {
            var overflow = _compostValue - maxValue;
            _compostValue = overflow;
            
            //TODO add fertilizer event or something + animation
        }
        UpdateSliders();
    }

    void UpdateSliders() {
        leftSlider.value = _compostValue;
        rightSlider.value = _compostValue;
    }
}
