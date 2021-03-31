using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Broker;
using Broker.Messages;
using JSON;
using Saving;
using UnityEngine;

namespace WorldGrid
{
    public interface IGrid {
        void AddObject(GridMoveObject gridObject, Vector3 gridPosition);

        /// <summary>
        /// Try move an object from one place to another
        /// </summary>
        /// <returns>True, if moving was successful</returns>
        bool TryMoveObject(GridMoveObject gridObject, Vector3 fromGridPosition, Vector3 toGridPosition);

        /// <summary>
        /// Moves on Object to the target position.
        /// It does not validate, whether there's something blocking the position.
        /// </summary>
        void MoveObject(GridMoveObject gridObject, Vector3 oldGridPosition, Vector3 gridPosition);
    }

    public class Grid : MonoBehaviour, IGrid {
        public Dictionary<Vector2Int, GridMoveObject> itemsOnGrid = new Dictionary<Vector2Int, GridMoveObject>();
        public Cell[] cells;
        public int width;
        public int height;
        public Cell cellPrefab;
        public GridMoveObject gridObjectPrefab;

        private float waitForDatabaseDelay = 2f; //TODO shorten this value to make plants populate on grid faster. Test to ensure enough time has passed to connect to db.
        private float minDelayBetweenSaves = .5f;
        private float lastSaveTime = 1.5f;

        private float lastAutoSave = 60f; 
        private float autoSaveDuration = 60f; 

        public void AddObject(GridMoveObject gridObject, Vector3 gridPosition) {
            AddObject(gridObject, Vector2Int.FloorToInt(gridPosition));
            SaveGridDataToDatabase();
        }
        
        public void RemoveObject(GridMoveObject gridObject, Vector2Int fromPosition)
        {
            RemoveObjectInternal(gridObject, fromPosition);
            SaveGridDataToDatabase();
        }

        public bool TryMoveObject(GridMoveObject gridObject, Vector3 fromGridPosition, Vector3 toGridPosition) {
            return TryMoveObject(gridObject, Vector2Int.FloorToInt(fromGridPosition),
                Vector2Int.FloorToInt(toGridPosition));
        }

        public void MoveObject(GridMoveObject gridObject, Vector3 oldGridPosition, Vector3 gridPosition) {
            MoveObject(gridObject, Vector2Int.FloorToInt(oldGridPosition), Vector2Int.FloorToInt(gridPosition));
        }

        void Awake()
        {
            lastSaveTime = waitForDatabaseDelay - minDelayBetweenSaves +.5f;
            SpawnGridCells();
            
        }

        private void Start() {
            StartCoroutine(LoadGridDataFromDatabase());
        }

        private void FixedUpdate()
        {
            if (Time.time > lastAutoSave + autoSaveDuration)
            {
                lastAutoSave = Time.time;
                Debug.Log("auto save calling save method");
                SaveGridDataToDatabase();
            }
        }

        private IEnumerator LoadGridDataFromDatabase() {
            yield return new WaitForSeconds(waitForDatabaseDelay); 
            var dataTask = FindObjectOfType<SaveManager>().LoadGrid();
            yield return new WaitUntil(() => dataTask.IsCompleted);
            var savedData = dataTask.Result;
            if (savedData == null || savedData.Count == 0) {
                Debug.Log("grid data not found on server. If no plant has ever been planted on the grid then this is expected", this);
                yield break;
            }

            foreach (var gridSaveInfo in savedData) {
                GridMoveObject gridObjectInstance = Instantiate(gridObjectPrefab, this.transform);
                GridPlant plantRef = gridObjectInstance.GetComponent<GridPlant>();
                plantRef.plant = ConvertSO.ClassToSO(gridSaveInfo.item);
                plantRef.currentSoilStage = gridSaveInfo.soilStage;
                plantRef.soilStageProgress = gridSaveInfo.soilStageProgress;
                Vector2Int loc = new Vector2Int(gridSaveInfo.x, gridSaveInfo.y);
                gridObjectInstance.transform.localPosition = new Vector3Int(loc.x, loc.y, 0);
                gridObjectInstance.isOnGrid = true;
                Vector2Int itemDimensions = Vector2Int.FloorToInt(plantRef.plant.sizeDimensions);
                gridObjectInstance.Size = itemDimensions;
                gridObjectInstance.transform.localScale = new Vector3(itemDimensions.x, itemDimensions.y, 1.0f);
                plantRef.InitFromSave(plantRef.plant, this);
            }
        }

        void SpawnGridCells() {
            this.cells = new Cell[this.width * this.height];
            for (var x = 0; x < this.width; x++) {
                for (var y = 0; y < this.height; y++) {
                    var cell = Instantiate(this.cellPrefab, this.transform);
                    // not needed here, but this would reset the local rotation:
                    // cell.transform.localRotation = Quaternion.identity;
                    cell.Position = new Vector2Int(x, y);
                    SetCell(x, y, cell);
                }
            }
        }

        Cell GetCell(int x, int y) {
            return this.cells[y * this.width + x];
        }
        
        void SetCell(int x, int y, Cell cell) {
            this.cells[y * this.width + x] = cell;
        }

        IEnumerable<Cell> GetCellsInRect(Vector2Int position, Vector2Int size) {
            for (var x = position.x; x < position.x + size.x; x++) {
                for (var y = position.y; y < position.y + size.y; y++) {
                    yield return GetCell(x, y);
                }
            }
        }

        void MoveObject(GridMoveObject gridObject, Vector2Int fromPosition, Vector2Int toPosition) {
            RemoveObjectInternal(gridObject, fromPosition);
            AddObject(gridObject, toPosition);
        }

        bool IsFree(Vector2Int position, Vector2Int size) {
            return GetCellsInRect(position, size)
                .All(cell => cell.GridObject == null);
        }

        bool TryMoveObject(GridMoveObject gridObject, Vector2Int fromPosition, Vector2Int toPosition) {
            if (toPosition.x < 0 || toPosition.y < 0) return false;

            if (gridObject.isOnGrid)
                RemoveObjectInternal(gridObject, fromPosition);

            if (IsFree(toPosition, gridObject.Size)) {
                gridObject.isOnGrid = true;
                AddObject(gridObject, toPosition);
                SaveGridDataToDatabase();
                return true;
            } else {
                AddObject(gridObject, fromPosition);
                return false;
            }
        }

        private void RemoveObjectInternal(GridMoveObject gridObject, Vector2Int fromPosition) {
            itemsOnGrid.Remove(fromPosition);
            foreach (var cell in GetCellsInRect(fromPosition, gridObject.Size)) {
                cell.GridObject = null;
            }
        }

        private void AddObject(GridMoveObject gridObject, Vector2Int toPosition) {
            itemsOnGrid.Add(toPosition, gridObject);
            foreach (var cell in GetCellsInRect(toPosition, gridObject.Size)) {
                cell.GridObject = gridObject;
            }
        }

        private void OnApplicationQuit()
        {
            SaveGridDataToDatabase();
        }

        public void SaveGridDataToDatabase()
        {
            Debug.Log("Save method called");
            // wait for min delay between each save. 
            if (lastSaveTime + minDelayBetweenSaves > Time.time)
                return;
            
            Debug.Log("Saving to database");
            lastSaveTime = Time.time;

            List<GridSaveInfo> gridSaveInfoList = new List<GridSaveInfo>();
            foreach (var pair in itemsOnGrid)
            {
                if (pair.Value.notMoveable)
                    continue;
                GridSaveInfo gridSaveInfo;
                GridPlant gridPlant = pair.Value.GetComponent<GridPlant>();
                gridSaveInfo.item = ConvertSO.SOToClass(gridPlant.plant);
                gridSaveInfo.soilStage = gridPlant.currentSoilStage;
                gridSaveInfo.soilStageProgress = gridPlant.soilStageProgress;
                gridSaveInfo.x = pair.Key.x;
                gridSaveInfo.y = pair.Key.y;
                gridSaveInfoList.Add(gridSaveInfo);
            }

            FindObjectOfType<SaveManager>().SaveGrid(gridSaveInfoList);
        }
    }

    public struct GridSaveInfo {
        public ItemClass item;
        public GridPlant.SoilStage soilStage;
        public float soilStageProgress;
        public int x;
        public int y;
    }
}