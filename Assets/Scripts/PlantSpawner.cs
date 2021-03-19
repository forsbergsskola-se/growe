using Inventory_and_Store;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    public GameObject pottedPlantPlaceholder;  // replace with a gridobject of type potted plant
    public Vector3 startOffsetFromGrid;
    private Transform gridTransform;

    private void OnEnable()
    {
        gridTransform = FindObjectOfType<Grid>().gameObject.transform;
        if (gridTransform == null)
            Debug.LogWarning("grid not found", this);
    }

    // ensure caller checks if a potted plant is selected
    public void SpawnPlant(Item item)
    {
        // TODO add a check that the item is a potted plant or change the parameter type.
        GameObject instance = Instantiate(pottedPlantPlaceholder, gridTransform.position + startOffsetFromGrid, gridTransform.rotation, gridTransform );
        //instance.GetComponent<GridObject>();
        // TODO pass Item reference to instance
    }

    public void SpawnPlant() // TODO temporary method wrapper
    {
        SpawnPlant(new Item());
    } 
}
