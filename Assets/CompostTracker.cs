using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CompostTracker : MonoBehaviour {
    public Slider leftSlider;
    public Slider rightSlider;
    public int maxValue = 15;
    int _compostValue;

    public void AddCompost(int amount) {
        _compostValue += amount;
        
        if (_compostValue > maxValue) {
            var overflow = _compostValue - maxValue;
            _compostValue = overflow;
        }
        
        UpdateSliders();
    }

    void UpdateSliders() {
        leftSlider.value = _compostValue;
        rightSlider.value = _compostValue;
    }
}
