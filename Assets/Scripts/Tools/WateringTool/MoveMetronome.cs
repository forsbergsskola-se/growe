
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tools.WateringTool
{
    public class MoveMetronome : MonoBehaviour
    {
        public Transform rotateAroundPoint;
        public float speed = 1;
        public int result, rotateAt;
        public List<int[]> ranges = new List<int[]> 
        {
            new int[]{ 50, 85, 3},
            new int[]{ 17, 50, 2},
            new int[]{ -17, 17, 1},
            new int[]{ -50, -17, 2},
            new int[]{ -85, -50, 3},

        };
        private float _currentAngle = 0;

        public enum Direction { Right = -1, Left = 1 };

        public Direction direction = Direction.Left;

        private void Start()
        {
            transform.GetChild(0).GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        }

        private void Update()
        {
            _currentAngle += speed * (int)GetDirection(_currentAngle, rotateAt) * Time.deltaTime;
            rotateAroundPoint.rotation = Quaternion.Euler(new Vector3(0, 0, _currentAngle));
        }


        private Direction GetDirection(float currentAngle, int rotateAt)
        {
            if (currentAngle + rotateAt > rotateAt * 2) return direction = Direction.Right;
            if (currentAngle + rotateAt < 0) return direction = Direction.Left;
            return direction = direction;
        }

        public void StopMetronome()
        {
            foreach (var range in ranges)
            {
                speed = 0;
                if (_currentAngle >= range[0] && _currentAngle <= range[1])
                {
                    result = range[2];
                    break;
                }
            }
        }
    }
}
