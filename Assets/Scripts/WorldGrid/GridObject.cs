using Broker;
using Broker.Messages;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler,
    IPointerDownHandler {
    public Vector2Int Size;
    Vector3 dragStartPosition;
    private bool isDragging;
    private CameraMovement cameraMovement;
    public bool notMoveable;
    public bool isOnGrid;
    public SpriteRenderer sprite;

    void Start() {
        var grid = GetComponentInParent<Grid>();

        cameraMovement = Camera.main.GetComponent<CameraMovement>();
        if (!isOnGrid)
            return;
        grid.AddObject(this, this.transform.localPosition);
    }

    public void OnDrag(PointerEventData eventData) {
        if (notMoveable) return;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 previousPos = transform.position;
        if (Physics.Raycast(ray, out var hitInfo)) {
            this.transform.position = hitInfo.point;
            this.transform.localPosition = Vector3Int.FloorToInt(this.transform.localPosition);
            if (transform.localPosition.x + Size.x > transform.parent.transform.GetComponent<Grid>().width || transform.localPosition.x < 0) {
                transform.position = previousPos;
            }
            if (transform.localPosition.y + Size.y > transform.parent.transform.GetComponent<Grid>().height || transform.localPosition.y < 0) {
                transform.position = previousPos;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (notMoveable) return;
        this.isDragging = true;
        this.dragStartPosition = this.transform.localPosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (notMoveable) return;
        this.isDragging = false;
        var grid = GetComponentInParent<Grid>();
        if (!grid.TryMoveObject(this, this.dragStartPosition, this.transform.localPosition)) {
            this.transform.localPosition = this.dragStartPosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (!isDragging && !notMoveable) {
            Debug.Log("Tap! Zooom the thing. Also there are som TODO's here come check 'em out");
            cameraMovement.StartMoveRoutine(transform
                .position); 
            var item = GetComponentInChildren<GridItem>().item;
            MessageBroker.Instance().Send(new PlantCloseUpMessage(item));
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
    } // not implemented. Required by OnPointerUp.
}