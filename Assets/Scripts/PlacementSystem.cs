using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject Camera;
    private Vector3 ScreenCenter;

    [SerializeField]
    private GameObject mouseIndicator; //, cellIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;

    // �ٴڼ�ġ�� ������ġ ����
    private GridData floorData, furnitureData;

    // ��ġ ���ɿ��� ����� ǥ��
    // private SpriteRenderer previewRenderer;

    private List<GameObject> placedGameObjects = new();

    // [SerializeField]
    // private GameObject gridVisualization;

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;


    private void Start()
    {
        StopPlacement();
        // �ٴ� / ������ ����
        floorData = new();
        furnitureData = new();

        // previewRenderer = cellIndicator.GetComponent<SpriteRenderer>();
    }

    // �κ� Ŭ�� �̺�Ʈ
    public void StartPlacement(int ID)
    {
        // StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if(selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        // gridVisualization.SetActive(true); // �ϴ� �ȸ���

        // cellIndicator.SetActive(true); // ����

        // ������ �����ֱ�
        /*
        preview.StartShowingPlacementPreview(
            database.objectsData[selectedObjectIndex].Prefab,
            database.objectsData[selectedObjectIndex].Size);
        */

        // ��ġ�Ϸ��� Ŭ��
        inputManager.OnClicked += PlaceStructure;
        // ��ġ ������
        inputManager.OnExit += StopPlacement;
    }

    // ��ġ ����
    // ȭ�� �� ��� ��ġ��
    private void PlaceStructure()
    {
        /*
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        */
        // --- �߰� ---
        // ȭ�� �߾� ��ǥ ��������
        ScreenCenter = new Vector3(Camera.transform.position.x, Camera.transform.position.y);
        Debug.Log(ScreenCenter);
        // �׸��� �� ��ǥ�� ��������
        Vector3Int gridPosition = grid.WorldToCell(ScreenCenter);

        //Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        // ���콺 ��ġ�� �׸��� �� ��ǥ�� ��������
        //Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // ��ġ ���� ���� Ȯ��
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if(placementValidity == false)
            return;
        
        // ��ġ ���� �������� ���⿡ .Play()

        // ��ġ
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition); 
        placedGameObjects.Add(newObject);

        // �ٴ�/�ǹ� ������ ����
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
           floorData :
           furnitureData;

        // ��ġ���� ����
        selectedData.AddObjectAt(gridPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID,
            placedGameObjects.Count - 1);

        // �� ��ġ�� �̹� ��ġ�Ǿ�����
        preview.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    // ��ġ ���� ����
    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        // ID�� 0 => �ٴ�
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
            floorData : 
            furnitureData;

            return selectedData.CanPlaceObejctAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        // gridVisualization.SetActive(false); // �ϴ� �ȸ���
        // cellIndicator.SetActive(false); //����
        preview.StopShowingPreview();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
    }

    

    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if(lastDetectedPosition != gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            // ��ġ �Ұ����� �����̸� ������
            // previewRenderer.color = placementValidity ? Color.white : Color.red; 

            mouseIndicator.transform.position = mousePosition;
            //cellIndicator.transform.position = grid.CellToWorld(gridPosition);
            preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
        }
                
    }
}
