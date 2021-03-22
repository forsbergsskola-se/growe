using UnityEngine;

public class Cell : MonoBehaviour {
	public Vector2Int Position {
		set => this.transform.localPosition = new Vector2(value.x, value.y);
	}

	public GridObject GridObject;
}