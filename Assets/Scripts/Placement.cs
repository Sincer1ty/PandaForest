using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

// �ǹ� ��ġ �� ����
public class Placement : MonoBehaviour
{
    [SerializeField]
    private GameObject MainCamera;
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

    public GameObject floatingUI; // �÷��� UI 
    private RectTransform rectFloating;  // �÷��� UI Rect
    public GameObject blocker; // Ŭ�� �̺�Ʈ�� ������ UI ������Ʈ

    public GameObject canvas2;
    private RectTransform rt;

    Vector2 localPos; // ��ȯ�� canvas�� ��ǥ

    public void Start()
    {
        StructureData = new();

        // ĵ���� ��ǥ 
        rt = canvas2.transform as RectTransform;
        rectFloating = floatingUI.GetComponent<RectTransform>();
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
        ScreenCenter = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y);
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

        // floatingUI Ȱ��ȭ 
        floatingUI.SetActive(true);
        // blocker.SetActive(true);

        // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
        var screenPos = Camera.main.WorldToScreenPoint(ScreenCenter);

        // ��ũ�� ��ǥ�� canvas�������� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, screenPos, null, out localPos);
        rectFloating.localPosition = localPos;
    }


    // ���� ��, ��ġ���� ���� 
    public bool EditStructure(Vector3 Position, int ID)
    {
        // ���� ���� Ȯ�� 
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return false;
        }

        // Ÿ�ϸ� �� ��ǥ�� ��������
        Vector3Int EditPosition = tilemap.WorldToCell(Position);
        print("EditPosition = "+EditPosition);

        // ��ġ ���� ���� Ȯ��
        bool placementValidity = CheckPlacementValidity(EditPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            return false;
        }
        print("��ġ �����մϴ�.");

        // ���ο� ��ġ�� �̵�������, ���� ��ġ ���� ���� 
        Vector3Int originGridPosition = tilemap.WorldToCell(DragSystem.originPosition);
        Vector2Int objectSize = database.objectsData[selectedObjectIndex].Size;

        StructureData.RemoveObjectAt(originGridPosition, objectSize);

        // ��ġ ������ ���� 
        StructureData.AddObjectAt(EditPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID );

        return true;
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
