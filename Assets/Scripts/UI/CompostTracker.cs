using UnityEngine;
using UnityEngine.UI;

namespace UI {
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
        
            //TODO set _compostValue value to saved compost data
            UpdateSliders();
        }

        
        //TODO the logic in this method should be moved to Currency
        //TODO This method should only read a value from CompostUpdateMessage and display it
        public void AddCompost(int compostAmount) {
            compostAmount = Mathf.Clamp(compostAmount, minAddAmount, maxAddAmount);
            _compostValue += compostAmount;
        
            if (_compostValue > maxValue) {
                var overflow = _compostValue - maxValue;
                _compostValue = overflow;
            
                //TODO add event that tells listeners to increase fertilizer by 1
                //TODO OR use FindObjectOfType -> GameManager -> GetComponent -> Currency and use AddFertilizer(1) method
            }
            UpdateSliders();
        }

        void UpdateSliders() {
            leftSlider.value = _compostValue;
            rightSlider.value = _compostValue;
        }
    }
}
