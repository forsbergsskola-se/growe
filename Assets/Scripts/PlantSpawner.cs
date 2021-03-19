using System;
using Inventory_and_Store;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    [Tooltip("The location to spawn the plant")]public Transform spawnPoint;
    public GameObject pottedPlantPlaceholder;  // replace with a gridobject of type potted plant
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
        GameObject instance = Instantiate(pottedPlantPlaceholder, spawnPoint);
        //instance.GetComponent<GridObject>();
        // TODO pass Item reference to instance
    }

    public void SpawnPlant() // TODO temporary method wrapper
    {
        SpawnPlant(new Item());
    } 
}
