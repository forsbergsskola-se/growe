using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IGrid {
	void AddObject(GridObject gridObject, Vector3 gridPosition);

	/// <summary>
	/// Try move an object from one place to another
	/// </summary>
	/// <returns>True, if moving was successful</returns>
	bool TryMoveObject(GridObject gridObject, Vector3 fromGridPosition, Vector3 toGridPosition);

	/// <summary>
	/// Moves on Object to the target position.
	/// It does not validate, whether there's something blocking the position.
	/// </summary>
	void MoveObject(GridObject gridObject, Vector3 oldGridPosition, Vector3 gridPosition);
}

public class Grid : MonoBehaviour, IGrid {
	public Cell[] cells;
	public int width;
	public int height;
	public Cell cellPrefab;
	
	public void AddObject(GridObject gridObject, Vector3 gridPosition) {
		AddObject(gridObject, Vector2Int.FloorToInt(gridPosition));
	}
	
	public bool TryMoveObject(GridObject gridObject, Vector3 fromGridPosition, Vector3 toGridPosition) {
		return TryMoveObject(gridObject, Vector2Int.FloorToInt(fromGridPosition), Vector2Int.FloorToInt(toGridPosition));
	}

	public void MoveObject(GridObject gridObject, Vector3 oldGridPosition, Vector3 gridPosition) {
		MoveObject(gridObject, Vector2Int.FloorToInt(oldGridPosition), Vector2Int.FloorToInt(gridPosition));
	}

	void Awake() {
		SpawnGridCells();
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
		for (var x = position.x; x < position.x + size.x; x ++) {
			for (var y = position.y; y < position.y + size.y; y ++) {
				yield return GetCell(x, y);
			}
		}
	}

	void MoveObject(GridObject gridObject, Vector2Int fromPosition, Vector2Int toPosition) {
		RemoveObject(gridObject, fromPosition);
		AddObject(gridObject, toPosition);
	}

	bool IsFree(Vector2Int position, Vector2Int size) {
		return GetCellsInRect(position, size)
			.All(cell => cell.GridObject == null);
	}

	bool TryMoveObject(GridObject gridObject, Vector2Int fromPosition, Vector2Int toPosition) {
		RemoveObject(gridObject, fromPosition);
		if (IsFree(toPosition, gridObject.Size)) {
			AddObject(gridObject, toPosition);
			return true;
		} else {
			AddObject(gridObject, fromPosition);
			return false;
		}
	}

	void RemoveObject(GridObject gridObject, Vector2Int fromPosition) {
		foreach (var cell in GetCellsInRect(fromPosition, gridObject.Size)) {
			cell.GridObject = null;
		}
	}

	void AddObject(GridObject gridObject, Vector2Int toPosition) {
		foreach (var cell in GetCellsInRect(toPosition, gridObject.Size)) {
			cell.GridObject = gridObject;
		}
	}
}