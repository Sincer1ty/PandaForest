using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;

    // 바닥설치와 가구설치 구분? 
    private GridData floorData, furnitureData;

    // 설치 가능여부 색깔로 표시
    private SpriteRenderer previewRenderer;

    private List<GameObject> placedGameObjects = new();

    // [SerializeField]
    // private GameObject gridVisualization;

    private void Start()
    {
        StopPlacement();
        // 바닥 / 구조물 구분
        floorData = new();
        furnitureData = new();

        previewRenderer = cellIndicator.GetComponent<SpriteRenderer>();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if(selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        // gridVisualization.SetActive(true); // 일단 안만듦
        cellIndicator.SetActive(true); 
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if(placementValidity == false)
            return;
        
        // 설치 사운드 넣으려면 여기에 .Play()

        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);
        placedGameObjects.Add(newObject);
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
           floorData :
           furnitureData;
        selectedData.AddObjectAt(gridPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID,
            placedGameObjects.Count - 1);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        // ID가 0 => 바닥
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
            floorData : 
            furnitureData;

            return selectedData.CanPlaceObejctAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        // gridVisualization.SetActive(false); // 일단 안만듦
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    

    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        // 설치 불가능한 영역이면 빨간색
        previewRenderer.color = placementValidity ? Color.white : Color.red; 

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);

    }
}
