using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ǹ� ��ġ �� ����
public class Placement : MonoBehaviour
{
    [SerializeField]
    private GameObject Camera;
    private Vector3 ScreenCenter;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;

    private Vector3Int lastDetectedPosition = Vector3Int.zero; // ���������� ������ �׸��� ��ġ

    // private GridData floorData, furnitureData;
    private GridData StructureData;

    private List<GameObject> placedGameObjects = new();

    bool isFirstBuild;

    // ��ġ ���ɿ��� ����� ǥ��
    private SpriteRenderer previewRenderer;

    public void Start()
    {
        // StopPlacement();
        // floorData = new();
        // furnitureData = new();
        StructureData = new();

        // previewRenderer = cellIndicator.GetComponent<SpriteRenderer>();
    }

    /*
    // ���õ� ������Ʈ�� ���� �ǹ� ��ġ ���¸� �����ϰ� �Է� �̺�Ʈ�� ���
    public void StartPlacement(int ID)
    {
        // StopPlacement();

        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        // gridVisualization.SetActive(true);
        // cellIndicator.SetActive(true);
        // inputManager.OnClicked += PlaceStructure;
        // inputManager.OnExit += StopPlacement;
    }
    */

    // ��ư Ŭ���� �߾ӿ� ���� : ID �޾ƿ���
    public void PlaceStructure(int ID)
    {
        // ���� ���� Ȯ�� 
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        // ȭ�� �߾� ��ǥ ��������
        ScreenCenter = new Vector3(Camera.transform.position.x, Camera.transform.position.y);
        Debug.Log(ScreenCenter);
        // �׸��� �� ��ǥ�� ��������
        Vector3Int gridPosition = grid.WorldToCell(ScreenCenter);

        //Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        //Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // ��ġ ���� ���� Ȯ��
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            // source.PlayOneShot(wrongPlacementClip);
            print("��ġ�� �� �����ϴ�.");
            return;
        }
        print("��ġ���� �մϴ�.");
        // source.PlayOneShot(correctPlacementClip);


        // �߾ӿ� ��ġ(����)
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);
        placedGameObjects.Add(newObject);

        // ���� ���⼭ �ٴ�,�ǹ� �����߾���
        GridData selectedData = StructureData;
        // ��ġ ������ ���� 
        selectedData.AddObjectAt(gridPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID,
            placedGameObjects.Count - 1);

        // preview.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    // �ǹ� �̵� (�巡�׷� �̵��� ��..?)
    private void MoveStructure()
    {
        /* 
        // UI ��� ���� ���콺 �����Ͱ� ������ �޼��带 ���� ?
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        */

    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        // ���� ���⼭ �ٴ�,�ǹ� �����߾���
        GridData selectedData = StructureData;

        return selectedData.CanPlaceObejctAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        // gridVisualization.SetActive(false);
        // cellIndicator.SetActive(false);
        // inputManager.OnClicked -= PlaceStructure;
        // inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;
        //Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        //Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // ������ �� �ٲ��ִ� �κ�
        //bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        //previewRenderer.material.color = placementValidity ? Color.white : Color.red;


        // mouseIndicator.transform.position = mousePosition;
        // cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

}
