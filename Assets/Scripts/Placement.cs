using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // �ٴڼ�ġ�� ������ġ ����
    private GridData floorData, furnitureData;

    private List<GameObject> placedGameObjects = new();

    // [SerializeField]
    // private PreviewSystem preview;

    // private Vector3Int lastDetectedPosition = Vector3Int.zero;

    private void Start()
    {
        // �ٴ�/������ ����
        floorData = new();
        furnitureData = new();

    }


    // ��ưŬ�� �̺�Ʈ �Լ�
    public void FirstPlacement(int ID)
    {
        // ȭ�� �߾� ��ǥ ��������
        // ȭ�� �߾��� Ÿ�� �� => ��ġ �Ұ��� ���� �ʿ� !
        ScreenCenter = new Vector3(Camera.transform.position.x, Camera.transform.position.y);
        Debug.Log(ScreenCenter);
        // �׸��� �� ��ǥ�� ��������
        Vector3Int gridPos = grid.WorldToCell(ScreenCenter);

        StartPlacement(ID, gridPos);
    }


    public void StartPlacement(int ID, Vector3Int gridPosition)
    {
        // StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        PlaceStructure(gridPosition);
    }

    // ������ ��ġ 
     public void PlaceStructure(Vector3Int gridPosition)
    {
        // ��ġ ���� ���� Ȯ��
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
            return;

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
        // �̹� ��ġ�Ǿ����� �� => ��ġ ��ġ�� �ֺ� �ٸ� ��ġ�� �ٲ��־����
        // preview.UpdatePosition(grid.CellToWorld(gridPosition), false);

    }

    // ��ġ ���� ����
    public bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        // ID�� 0 => �ٴ� ��ġ
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
            floorData :
            furnitureData;

        return selectedData.CanPlaceObejctAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

}
