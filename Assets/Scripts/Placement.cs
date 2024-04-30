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

    // [SerializeField]
    // private GameObject gridVisualization; // �׸��� �ð�ȭ

    /*
    [SerializeField]
    private AudioClip correctPlacementClip, wrongPlacementClip;
    [SerializeField]
    private AudioSource source;
    */

    private Vector3Int lastDetectedPosition = Vector3Int.zero; // ���������� ������ �׸��� ��ġ

    //[SerializeField]
    //private ObjectPlacer objectPlacer; // ������Ʈ ��ġ�� ���

    IBuildingState buildingState; // ���� �ǹ� ��ġ ���� 

    /*
    [SerializeField]
    private SoundFeedback soundFeedback;
    */
    // �ٴڼ�ġ�� ������ġ ����
    private GridData floorData, furnitureData;

    private List<GameObject> placedGameObjects = new();

    bool isFirstBuild;

    [SerializeField]
    private PreviewSystem preview;

    public SpriteRenderer previewRenderer;

    // private Vector3Int lastDetectedPosition = Vector3Int.zero;

    [SerializeField]
    private EditUIManager editUIManager;

    public GameObject EditUI;
    Animator PanelAnim;

    private void Start()
    {
        //gridVisualization.SetActive(false);

        // �ٴ�/������ ����
        floorData = new();
        furnitureData = new();

        PanelAnim = EditUI.GetComponent<Animator>();
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

        isFirstBuild = true;
        StartPlacement(ID, gridPos, isFirstBuild);

        // ����UI Down
        if (!editUIManager.isEditUIDown)
        {
            PanelAnim.SetBool("isDown", true);
            editUIManager.isEditUIDown = true;
            print(editUIManager.isEditUIDown);
        }
    }

    // ���õ� ������Ʈ�� ���� �ǹ� ��ġ ���¸� �����ϰ� �Է� �̺�Ʈ�� ���
    public void StartPlacement(int ID, Vector3Int gridPosition, bool isfirstbuild)
    {
        // StopPlacement();
        
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        

       // gridVisualization.SetActive(true);
       
        buildingState = new PlacementState(ID,
                                           grid,
                                           // preview,
                                           database,
                                           floorData,
                                           furnitureData
                                           /*,objectPlacer*/
                                           //, soundFeedback
                                           );
        
        //inputManager.OnClicked += PlaceStructure;
        //inputManager.OnExit += StopPlacement;

        PlaceStructure(gridPosition, isfirstbuild);
    }

    public void StartRemoving()
    {
        //StopPlacement();
        //gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, /*preview,*/ floorData, furnitureData/*, objectPlacer /*, soundFeedback*/ );
        // inputManager.OnClicked += PlaceStructure;
        // inputManager.OnExit += StopPlacement;
    }

    // �ǹ� ��ġ 
    // ���콺 ��ġ�� �������� �׸��� ��ġ�� ����Ͽ� �ǹ� ��ġ ������ OnAction �޼��� ȣ��
    public void PlaceStructure(Vector3Int gridPosition, bool isfirstbuild)
    {
        
        // ��ġ ���� ���� Ȯ��
        /*
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
            return;
        */
        // ù ��ġ�� �����ض�.
        if(isfirstbuild)
        {
            // ��ġ
            GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
            newObject.transform.position = grid.CellToWorld(gridPosition);
            placedGameObjects.Add(newObject);

           // buildingState.OnAction(gridPos);
        }
        
        // �ٴ�/�ǹ� ������ ����
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
           floorData :
           furnitureData;

        // ��ġ���� ����
        /*
        selectedData.AddObjectAt(gridPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID,
            placedGameObjects.Count - 1);
        */
        /*
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        */
        // Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3Int gridPos = grid.WorldToCell(mousePosition);

        buildingState.OnAction(gridPos);

        // �� ��ġ�� �̹� ��ġ�Ǿ�����
        // �̹� ��ġ�Ǿ����� �� => ��ġ ��ġ�� �ֺ� �ٸ� ��ġ�� �ٲ��־����
        // preview.UpdatePosition(grid.CellToWorld(gridPosition), false);

    }

    /*
    // ��ġ ���� ����
    public bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        // ID�� 0 => �ٴ� ��ġ
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
            floorData :
            furnitureData;

        return selectedData.CanPlaceObejctAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }
    */

    // �ǹ� ��ġ �Ǵ� ���Ÿ� ����
    // ���� ���¸� �����ϰ� �Է� �̺�Ʈ�� ����
    private void StopPlacement()
    {
        // soundFeedback.PlaySound(SoundType.Click);
        if (buildingState == null)
            return;
        //gridVisualization.SetActive(false);
        buildingState.EndState();
        // inputManager.OnClicked -= PlaceStructure;
        // inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }

    // ���콺 ��ġ�� ����� �� �ǹ� ��ġ ���¸� ������Ʈ => OnMouseDrag
    private void Update()
    {
        if (buildingState == null)
            return;
        // Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }

    }

}
