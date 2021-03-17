using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Settings
    float MouseZoomSpeed = 15.0f;
    float TouchZoomSpeed = 0.1f;
    float ZoomMinBound = 0.1f;
    float ZoomMaxBound = 179.9f;
    [Header("MoveRoutine Settings")]
    [SerializeField, Tooltip("How long it takes for move to to reach its target position"), Range(0.001f, 2f)] 
    private float moveDuration = 0.3F;
    [SerializeField, Tooltip("Zoom distance on plant click"), Range(0.1f, 179.9f)] private float targetCamSize = 3.65f; 
    
    //references
    Camera cam;
    
    //variables 
    Vector3 mouseOnScrollOriginPosition = Vector3.zero;
    Vector3 cameraOnScrollOriginPosition = Vector3.zero;
    private float cameraZ;
    
    private bool moveRoutineActive;
    private float moveTimer;
    private Vector2 moveXYTarget;
    private float moveCamSizeVelocity = 0.0f;     // variable used by Mathf.Smoothdamp
    private Vector2 moveXYVelocity = Vector2.zero; // variable used by Vector3.Smoothdamp

    void OnEnable() {
        cam = Camera.main;
        cameraZ = cam.transform.position.z;
    }
    
    void Update()
    {
        if (Input.touchSupported)
            HandleAndroidInput();
        else
            HandlePcInput();

        ConstrainOrthographicSize();
    }

    private void FixedUpdate()
    {
        if (moveRoutineActive)
            MoveRoutine();
    }

    private void HandlePcInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveRoutineActive = false;
            mouseOnScrollOriginPosition = Input.mousePosition;
            cameraOnScrollOriginPosition = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            TouchDrag(Input.mousePosition);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            moveRoutineActive = false;
            Zoom(scroll, MouseZoomSpeed);
        }
    }

    private void HandleAndroidInput()
    {
        if (moveRoutineActive && Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                moveRoutineActive = false;
        }

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

    private void ConstrainOrthographicSize()
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

    public void StartMoveRoutine(Vector3 targetWorldPos)
    {
        // a new routine has started before the previous finished
        if (moveRoutineActive) 
        {
            this.moveCamSizeVelocity = 0;
            this.moveXYVelocity = Vector3.zero;            
        }
        
        // camera is orthogonal. Better not move the z to ensure everything stays view.
        // instead the zoom level is represented by orthographicSize. target is set in targetCamSize 
        this.moveXYTarget = (Vector2) targetWorldPos; 
        moveRoutineActive = true;
        moveTimer = Time.time + moveDuration;
    }

    private void MoveRoutine()
    {
        Vector2 result = Vector2.SmoothDamp(transform.position, moveXYTarget, ref moveXYVelocity, moveDuration);
        transform.position = new Vector3(result.x, result.y, this.cameraZ); 
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, this.targetCamSize, ref moveCamSizeVelocity, moveDuration);
        
        if (Time.deltaTime >= moveTimer)
            moveRoutineActive = false;
    }
}
