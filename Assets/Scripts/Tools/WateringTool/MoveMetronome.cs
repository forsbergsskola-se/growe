
using System;
using UnityEngine;

namespace Tools.WateringTool
{
    public class MoveMetronome : MonoBehaviour
    {
        public Transform rotateAroundPoint;
        public GameObject metronome;
        public Quaternion angle;
        public int speed;
        public int reward;
        public bool direction;
        

        public Vector3 RotatePointAroundPivot(Vector3 point , Vector3 pivot, Quaternion angles){
            return angles * (point - pivot) + pivot;
        }

        private void Start()
        {
            //transform.position = _finalPosition.position; 
        }

        public void Update()
        {
            
            var test = RotatePointAroundPivot(metronome.transform.position,rotateAroundPoint.position, 
                angle);

            metronome.transform.rotation *= Quaternion.AngleAxis(speed, test);

            Debug.Log(test);
        }
    }
}
