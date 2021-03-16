using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 hit_position = Vector3.zero;
    Vector3 current_position = Vector3.zero;
    Vector3 camera_position = Vector3.zero;
    float z = 0.0f;
    
    float MouseZoomSpeed = 15.0f;
    float TouchZoomSpeed = 0.1f;
    float ZoomMinBound = 0.1f;
    float ZoomMaxBound = 179.9f;
    
    Camera cam;

    void Start() {
        cam = Camera.main;
    }
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            hit_position = Input.mousePosition;
            camera_position = transform.position;
        }
        if(Input.GetMouseButton(0)){
            current_position = Input.mousePosition;
            TouchDrag();        
        }
        
        if (Input.touchSupported)
        {
            // Pinch to zoom
            if (Input.touchCount == 2)
            {
                // get current touch positions
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);

                if (tOne.phase == TouchPhase.Began)
                {
                    hit_position = Input.mousePosition;
                    camera_position = transform.position;
                }

                // get touch position from the previous frame
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;
                
                bool scrollMove = 0.4 < Vector2.Dot(tZero.deltaPosition.normalized, tOne.deltaPosition.normalized);
                if (scrollMove)
                {
                    Vector2 averagePos = (tZero.deltaPosition + tOne.deltaPosition) * 0.5f;
                    current_position = (Vector3) averagePos;
                }
                else
                {
                    float oldTouchDistance = Vector2.Distance (tZeroPrevious, tOnePrevious);
                    float currentTouchDistance = Vector2.Distance (tZero.position, tOne.position);

                    // get offset value
                    float deltaDistance = oldTouchDistance - currentTouchDistance;
                    Zoom (deltaDistance, TouchZoomSpeed);   
                }
            }
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, MouseZoomSpeed);
        }

        if(cam.fieldOfView < ZoomMinBound) 
        {
            cam.orthographicSize = 0.1f;
        }
        else if(cam.fieldOfView > ZoomMaxBound ) 
        {
            cam.orthographicSize = 179.9f;
        }
    }
    
    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        cam.orthographicSize += deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, ZoomMinBound, ZoomMaxBound);
    }

    void TouchDrag()
    {
        current_position.z = hit_position.z = camera_position.y;
        Vector3 direction = cam.ScreenToWorldPoint(current_position) - cam.ScreenToWorldPoint(hit_position);
        direction = direction * -1;
        Vector3 position = camera_position + direction;
        transform.position = position;
    }

}
