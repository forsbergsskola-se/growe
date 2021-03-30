using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    public UnityEvent OnMoveToRoutineFinished;
    private const int UILayer = 5;
    
    // Settings
    [Header("Pinch zoom settings")]
    [SerializeField] private float MouseZoomSpeed = 15.0f;
    [SerializeField] private float TouchZoomSpeed = 0.1f;
    [SerializeField, Range(0f, 179.9f)] private float ZoomMinBound = 0.1f;
    [SerializeField, Range(0f, 179.9f)] private float ZoomMaxBound = 179.9f;
    
    [Header("Tap zoom settings")]
    [SerializeField, Tooltip("How long it takes for move to to reach its target position. For some reason this value is not exact but should at least affect how long it takes"), Range(0.001f, 2f)] 
    private float moveDuration = 0.6f; 
    [SerializeField, Tooltip("Zoom distance on plant click"), Range(0.1f, 179.9f)] private float targetCamSize = 3.65f;

    //references
    private Camera cam;
    private EventSystem eventSystem;

    //variables 
    Vector3 mouseOnScrollOriginPosition = Vector3.zero;
    Vector3 cameraOnScrollOriginPosition = Vector3.zero;
    private float cameraZ;
    
    private bool moveRoutineActive;
    private Vector2 moveXYTarget;
    private float moveCamSizeVelocity = 0.0f;     // variable used by Mathf.Smoothdamp
    private Vector2 moveXYVelocity = Vector2.zero; // variable used by Vector3.Smoothdamp

    private bool touchDragActive = false;
    
    List<RaycastResult> cachedList = new List<RaycastResult>();

    void OnEnable() {
        cam = Camera.main;
        cameraZ = cam.transform.position.z;
        eventSystem = GetComponent<EventSystem>();
        if (eventSystem == null)
            Debug.LogWarning("eventSystem not found on CameraMovement");
    }
    
    void Update()
    {
        if (Input.touchSupported)
            HandleMobileInput();
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
            if (!IsPointerOverUIObject())
            {
                moveRoutineActive = false;
                touchDragActive = true;
                mouseOnScrollOriginPosition = Input.mousePosition;
                cameraOnScrollOriginPosition = transform.position;                
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (touchDragActive && !IsPointerOverUIObject())
                TouchDrag(Input.mousePosition);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            moveRoutineActive = false;
            Zoom(scroll, MouseZoomSpeed);
        }

        if (Input.GetMouseButtonUp(0))
            touchDragActive = false;
    }

    private void HandleMobileInput()
    {
        if (moveRoutineActive && Input.touchCount > 0)
        {
            if (!IsPointerOverUIObject())
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    moveRoutineActive = false;                
            }
        }

        if (Input.touchCount == 2)
        {
            if (IsPointerOverUIObject())
                return;
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
                touchDragActive = true;
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
    }

    private void MoveRoutine()
    {
        Vector2 result = Vector2.SmoothDamp(transform.position, moveXYTarget, ref moveXYVelocity, moveDuration, float.MaxValue, Time.fixedDeltaTime);
        transform.position = new Vector3(result.x, result.y, this.cameraZ); 
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, this.targetCamSize, ref moveCamSizeVelocity, moveDuration, float.MaxValue, Time.fixedDeltaTime);
        
        if (Mathf.Abs(cam.orthographicSize - targetCamSize) < 0.1f ) 
        {
            moveRoutineActive = false;
            OnMoveToRoutineFinished.Invoke();
        }
    }

    private bool IsPointerOverUIObject()
    {
        Vector2 touchPos = Input.mousePosition;
        var eventDataCurrentPosition = new PointerEventData(EventSystem.current) {position = touchPos};
        EventSystem.current.RaycastAll(eventDataCurrentPosition, cachedList);
        foreach (var hit in cachedList)
        {
            if (hit.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }
    
    // Marcs suggested method
    //         //Add UI check
    // Sol 1 - Create events on menu open / close. Deactivate activate this script on event trigger
    // Sol 2 - 
            
    // static bool IsPointerOverUIObject(Vector2 touchPosition, System.Collections.Generic.List<RaycastResult> cachedList) {
    //     if(EventSystem.current == null)
    //         return false;
    //     var eventDataCurrentPosition = new PointerEventData(EventSystem.current) {position = touchPosition};
    //     EventSystem.current.RaycastAll(eventDataCurrentPosition, cachedList);
    //     return cachedList.Count > 0;
    // }
    
}
