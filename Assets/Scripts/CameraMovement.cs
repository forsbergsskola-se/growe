using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 mouseOnScrollOriginPosition = Vector3.zero;
    Vector3 cameraOnScrollOriginPosition = Vector3.zero;
    float z = 0.0f;

    float MouseZoomSpeed = 15.0f;
    float TouchZoomSpeed = 0.1f;
    float ZoomMinBound = 0.1f;
    float ZoomMaxBound = 179.9f;

    Camera cam;

    void Start() {
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.touchSupported)
            HandleAndroidInput();
        else
            HandlePcInput();

        ConstrainMaxMinZoom();
    }

    private void HandlePcInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseOnScrollOriginPosition = Input.mousePosition;
            cameraOnScrollOriginPosition = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            TouchDrag(Input.mousePosition);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Zoom(scroll, MouseZoomSpeed);
    }

    private void HandleAndroidInput()
    {
        if (Input.touchCount == 2)
        {
            Touch tZero = Input.GetTouch(0);
            Touch tOne = Input.GetTouch(1);

            if (tOne.phase == TouchPhase.Began)
            {
                mouseOnScrollOriginPosition = Input.mousePosition;
                cameraOnScrollOriginPosition = transform.position;
            }

            bool fingersMoveSameDir = 0.4 < Vector2.Dot(tZero.deltaPosition.normalized, tOne.deltaPosition.normalized);
            if (fingersMoveSameDir)
            {
                Vector2 averagePos = (tZero.position + tOne.position) * 0.5f;
                TouchDrag((Vector3) averagePos);
            }
            else
            {
                // Pinch to zoom
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                // get offset value
                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom(deltaDistance, TouchZoomSpeed);
            }
        }
    }

    private void ConstrainMaxMinZoom()
    {
        if (cam.fieldOfView < ZoomMinBound)
            cam.orthographicSize = 0.1f;
        else if (cam.fieldOfView > ZoomMaxBound)
            cam.orthographicSize = 179.9f;
    }

    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        cam.orthographicSize += deltaMagnitudeDiff * speed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, ZoomMinBound, ZoomMaxBound);
    }

    void TouchDrag(Vector3 currentMousePos)
    {
        Vector3 direction = cam.ScreenToWorldPoint(mouseOnScrollOriginPosition) - cam.ScreenToWorldPoint(currentMousePos);
        direction.z = 0f;
        Vector3 position = cameraOnScrollOriginPosition + direction;
        transform.position = position;
    }
}