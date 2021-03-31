using Broker;
using Broker.Messages;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WorldGrid
{
    public class GridMoveObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler,
        IPointerDownHandler {
        public Vector2Int Size;
        Vector3 dragStartPosition;
        private bool isDragging;
        private bool toolSelected;
        private CameraMovement cameraMovement;
        public bool notMoveable;
        public bool isOnGrid;
        private Grid grid;
        private Vector3 _previousCameraPosition;
        
        void Start() {
            grid = GetComponentInParent<Grid>();

            cameraMovement = Camera.main.GetComponent<CameraMovement>();
            if (!isOnGrid)
                return;
            grid.AddObject(this, this.transform.localPosition);
            
            MessageBroker.Instance().SubscribeTo<ToolSelectedMessage>(UpdateToolSelected);
            MessageBroker.Instance().SubscribeTo<CancelSelectedToolMessage>(UpdateToolSelected);
        }

        public void OnDrag(PointerEventData eventData) {
            if (notMoveable) return;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 previousPos = transform.position;
            if (Physics.Raycast(ray, out var hitInfo, 100f, LayerMask.GetMask("IsometricGrid"))) {
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
            if (!isDragging && !notMoveable && !toolSelected) {
                cameraMovement.StartMoveRoutine(transform
                    .position);
                var plant = GetComponent<GridPlant>().plant;
                MessageBroker.Instance().Send(new PreviousCameraSizeMessage(Camera.main.orthographicSize));
                MessageBroker.Instance().Send(new PlantCloseUpMessage(plant, this));
            }
        }

        void UpdateToolSelected(ToolSelectedMessage m) {
            toolSelected = m.toolSelected;
        }
        void UpdateToolSelected(CancelSelectedToolMessage m) {
            toolSelected = false;
        }

        public void DestroyGridObj() {
            Destroy(gameObject);
        }

        public void OnPointerDown(PointerEventData eventData) {
        } // not implemented. Required by OnPointerUp.

        private void OnDestroy()
        {
            if (isOnGrid) {
                grid.RemoveObject(this, Vector2Int.FloorToInt(transform.localPosition));
            }
        }
    }
}