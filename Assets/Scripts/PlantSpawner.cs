using Inventory_and_Store;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    public GameObject pottedPlantPlaceholder;  // replace with a gridobject of type potted plant
    public Vector3 startOffsetFromGrid;
    private Grid grid;

    private void OnEnable()
    {
        grid = FindObjectOfType<Grid>();
        if (grid == null)
            Debug.LogWarning("grid not found", this);
    }

    // ensure caller checks if a potted plant is selected
    public void SpawnPlant(Item item)
    {
        // TODO add a check that the item is a potted plant or change the parameter type.
        GameObject instance = Instantiate(pottedPlantPlaceholder, grid.transform.position + startOffsetFromGrid, grid.transform.rotation, grid.transform );
        Vector3Int pos = new Vector3Int();
        pos.x = (int) (grid.width * 0.5);
        pos.y = (int) (grid.height * 0.5);
        instance.transform.localPosition = pos;
        // TODO pass Item reference to instance
    }

    public void SpawnPlant() // TODO temporary method wrapper
    {
        SpawnPlant(new Item());
    } 
}
