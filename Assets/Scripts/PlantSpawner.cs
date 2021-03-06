using System;
using InventoryAndStore;
using UnityEngine;
using WorldGrid;
using Grid = WorldGrid.Grid;

public class PlantSpawner : MonoBehaviour {
    //references 
    public GameObject pottedPlantPlaceholder; // replace with a gridobject of type potted plant
    private Grid grid;
    private Transform gridTransform;
    private GridMoveObject heldObject;

    private void OnEnable() {
        grid = FindObjectOfType<Grid>();
        if (grid == null)
            Debug.LogWarning("grid not found", this);
        else
            gridTransform = grid.transform;
    }

    // ensure caller checks if a potted plant is selected
    public void SpawnPlant(ItemSO item, Inventory playerInventory) {
        if (heldObject == null || heldObject.isOnGrid) {
            InstantiatePlant(item);
        } else {
            DestroyAndAddPlantToInventory(playerInventory);
            InstantiatePlant(item);
        }
    }

    private void InstantiatePlant(ItemSO item) {
        GameObject instance = Instantiate(pottedPlantPlaceholder, grid.transform.position, gridTransform.rotation,
            gridTransform);
        heldObject = instance.GetComponent<GridMoveObject>();

        Vector2Int itemDimensions = Vector2Int.FloorToInt(item.sizeDimensions);
        heldObject.Size = itemDimensions;
        instance.transform.localScale = new Vector3(itemDimensions.x, itemDimensions.y, 1.0f);

        Vector3Int pos = Vector3Int.zero;
        pos.x = (int) (grid.width * 0.5);
        pos.y = (int) (grid.height * 0.5);
        instance.transform.localPosition = pos;
        instance.GetComponent<GridPlant>().Init(item, grid);
    }

    private void DestroyAndAddPlantToInventory(Inventory playerInventory) {
        var gridItem = heldObject.GetComponent<GridPlant>();
        Destroy(heldObject.gameObject);
        heldObject = null;
        playerInventory.Add(gridItem.plant);
    }

    private void OnDestroy()
    {
        //TODO handle held object on quit.
    }
}