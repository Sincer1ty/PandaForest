using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// �ǹ� ��ġ �� ����
public class Placement : MonoBehaviour
{
    [SerializeField]
    private GameObject Camera;
    private Vector3 ScreenCenter;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;

    private Vector3Int lastDetectedPosition = Vector3Int.zero; // ���������� ������ �׸��� ��ġ

    private GridData StructureData;

    private List<GameObject> placedGameObjects = new();

    // ��ġ ���ɿ��� ����� ǥ��
    private SpriteRenderer previewRenderer;

    [SerializeField]
    private EditUIManager UiManager;

    public void Start()
    {
        StructureData = new();
        
    }

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

        // Ÿ�ϸ� �� ��ǥ�� ��������
        Vector3Int centerPosition = tilemap.WorldToCell(ScreenCenter);
        print(centerPosition);


        // ��ġ ���� ���� Ȯ��
        bool placementValidity = CheckPlacementValidity(centerPosition, selectedObjectIndex);
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
        newObject.transform.position = tilemap.CellToWorld(centerPosition);
        placedGameObjects.Add(newObject);

        // ���� ���⼭ �ٴ�,�ǹ� �����߾���
        GridData selectedData = StructureData;
        // ��ġ ������ ���� 
        selectedData.AddObjectAt(centerPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID );

        // ���� UI ������ 
        UiManager.EditUIDown();

    }


    // ���� ��, ��ġ���� ���� 
    public void EditStructure(Vector3 Position, int ID)
    {
        // ���� ���� Ȯ�� 
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        // Ÿ�ϸ� �� ��ǥ�� ��������
        Vector3Int EditPosition = tilemap.WorldToCell(Position);
        print("EditPosition = "+EditPosition);

        // ��ġ ���� ���� Ȯ��
        bool placementValidity = CheckPlacementValidity(EditPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            print("��ġ�� �� �����ϴ�.");
            return;
        }
        print("��ġ���� �մϴ�.");

        // ���� ���⼭ �ٴ�,�ǹ� �����߾���
        GridData selectedData = StructureData;
        // ��ġ ������ ���� 
        selectedData.AddObjectAt(EditPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID );

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
        
    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;
       
    }

}
