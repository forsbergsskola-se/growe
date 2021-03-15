using UnityEngine;
using UnityEngine.EventSystems;

public class GridObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    public Vector2Int Size;
    
    Vector3 dragStartPosition;

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

    public void OnBeginDrag(PointerEventData eventData) {
        this.dragStartPosition = this.transform.localPosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        var grid = GetComponentInParent<Grid>();
        if (!grid.TryMoveObject(this, this.dragStartPosition, this.transform.localPosition)) {
            this.transform.localPosition = this.dragStartPosition;
        }
    }
}