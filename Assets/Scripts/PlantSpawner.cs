using InventoryAndStore;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    //references 
    public GameObject pottedPlantPlaceholder;  // replace with a gridobject of type potted plant
    private Grid grid;
    private Transform gridTransform;

    private void OnEnable()
    {
        grid = FindObjectOfType<Grid>();
        if (grid == null)
            Debug.LogWarning("grid not found", this);
        else
            gridTransform = grid.transform;
    }

    // ensure caller checks if a potted plant is selected
    public void SpawnPlant(ItemSO item)
    {
        GameObject instance = Instantiate(pottedPlantPlaceholder, grid.transform.position, gridTransform.rotation, gridTransform );
        GridObject gridObject = instance.GetComponent<GridObject>();
        
        Vector2Int itemDimensions = Vector2Int.FloorToInt(item.sizeDimensions);
        gridObject.Size = itemDimensions;
        instance.transform.localScale = new Vector3(itemDimensions.x, itemDimensions.y, 1.0f);

        Vector3Int pos = Vector3Int.zero;
        pos.x = (int) (grid.width * 0.5);
        pos.y = (int) (grid.height * 0.5);
        instance.transform.localPosition = pos;
        instance.GetComponentInChildren<GridItem>().Init(item);
    }
}
