using UnityEngine;
using WorldGrid;

public class Cell : MonoBehaviour {
	public Vector2Int Position {
		set => this.transform.localPosition = new Vector2(value.x, value.y);
	}

	public GridMoveObject GridObject;
}