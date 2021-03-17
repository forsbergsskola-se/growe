using UnityEngine;
using UnityEngine.EventSystems;

public class GridObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler {

    public Vector2Int Size;
    Vector3 dragStartPosition;
    private bool isDragging;
    

    void Start() {
        var grid = GetComponentInParent<Grid>();
        grid.AddObject(this, this.transform.localPosition);
    }
    
    public void OnDrag(PointerEventData eventData){
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo)) {
            this.transform.position = hitInfo.point;
            this.transform.localPosition = Vector3Int.FloorToInt(this.transform.localPosition);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.isDragging = true;
        this.dragStartPosition = this.transform.localPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.isDragging = false;
        var grid = GetComponentInParent<Grid>();
        if (!grid.TryMoveObject(this, this.dragStartPosition, this.transform.localPosition)) {
            this.transform.localPosition = this.dragStartPosition;
        }
    }

    //TODO doesn't register with android input
    public void OnPointerUp(PointerEventData eventData) //TODO TEST what happens when holding one finger and tap with the other. 
    {
        if (!isDragging)
        {
            Debug.Log("Tap! Zooom the thing. Also there are som TODO's here come check 'em out");
            Camera.main.GetComponent<CameraMovement>().StartMoveRoutine(transform.position); //TODO change call method. Where is ruben when I need a pub sub?
        }
    }

    public void OnPointerDown(PointerEventData eventData) { } // not implemented. Required by OnPointerUp.
}