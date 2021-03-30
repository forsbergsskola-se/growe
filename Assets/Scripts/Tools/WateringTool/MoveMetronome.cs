using System;
using System.Collections;
using System.Collections.Generic;
using InventoryAndStore;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.WateringTool
{
    public class MoveMetronome : MonoBehaviour
    {
        
        public Transform metronomePoint;
        public GameObject disableButtons;
        public GameObject exitButton;
        public float speed;
        public int result, rotateAt;
        private readonly List<int[]> _ranges = new List<int[]> 
        {
            new []{ 50, 85, 3},
            new []{ 17, 50, 2},
            new []{ -17, 17, 1},
            new []{ -50, -17, 2},
            new []{ -85, -50, 3},

        };
        public float currentAngle = 0;
        public GridPlant selectedPlant;

        public enum Direction { Right = -1, Left = 1 };
        public Direction direction = Direction.Left;

        private void Start()
        {
            transform.GetChild(0).GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        }

        public void UISetActive(bool setActive) {
            metronomePoint.parent.gameObject.SetActive(setActive);
            disableButtons.SetActive(setActive);
            exitButton.SetActive(setActive);
        }

        public IEnumerator Rotate()
        {
            while (true)
            {
                currentAngle += speed * (int)GetDirection(currentAngle, rotateAt) * Time.deltaTime;
                metronomePoint.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
                Debug.Log("plant rarity int:  " + speed);
                Debug.Log("oops");
                if (speed <= 0) yield break;
                yield return null;
            }
            
        }


        private Direction GetDirection(float currentAngle, int rotateAt)
        {
            if (currentAngle + rotateAt > rotateAt * 2) return direction = Direction.Right;
            if (currentAngle + rotateAt < 0) return direction = Direction.Left;
            return direction = direction;
        }


        public void ExitMetronome()
        {
            UISetActive(false);
        }
        public void StopMetronome()
        {
            Debug.Log("SelectedPlant = " + selectedPlant);
            Debug.Log("Speed = " + speed);
            if (selectedPlant == null) return;
            speed = 0;
            foreach (var range in _ranges)
            {
                if (currentAngle >= range[0] && currentAngle <= range[1])
                {
                    var enumCount = Enum.GetNames(typeof(GridPlant.SoilStage)).Length;
                    
                    if ((int) selectedPlant.currentSoilStage + range[2] > enumCount)
                        selectedPlant.currentSoilStage = GridPlant.SoilStage.OverWatered;
                    else selectedPlant.currentSoilStage += range[2];
                    
                    selectedPlant = null;
                    break;
                }
            }
        }
    }
}
