using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 건물 배치 및 제거
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
    // private GameObject gridVisualization; // 그리드 시각화

    /*
    [SerializeField]
    private AudioClip correctPlacementClip, wrongPlacementClip;
    [SerializeField]
    private AudioSource source;
    */

    private Vector3Int lastDetectedPosition = Vector3Int.zero; // 마지막으로 감지된 그리드 위치

    //[SerializeField]
    //private ObjectPlacer objectPlacer; // 오브젝트 배치에 사용

    IBuildingState buildingState; // 현재 건물 배치 상태 

    /*
    [SerializeField]
    private SoundFeedback soundFeedback;
    */
    // 바닥설치와 가구설치 구분
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

        // 바닥/구조물 구분
        floorData = new();
        furnitureData = new();

        PanelAnim = EditUI.GetComponent<Animator>();
    }


    // 버튼클릭 이벤트 함수
    public void FirstPlacement(int ID)
    {
        // 화면 중앙 좌표 가져오기
        // 화면 중앙이 타일 밖 => 설치 불가능 구현 필요 !
        ScreenCenter = new Vector3(Camera.transform.position.x, Camera.transform.position.y);
        Debug.Log(ScreenCenter);
        // 그리드 셀 좌표로 가져오기
        Vector3Int gridPos = grid.WorldToCell(ScreenCenter);

        isFirstBuild = true;
        StartPlacement(ID, gridPos, isFirstBuild);

        // 편집UI Down
        if (!editUIManager.isEditUIDown)
        {
            PanelAnim.SetBool("isDown", true);
            editUIManager.isEditUIDown = true;
            print(editUIManager.isEditUIDown);
        }
    }

    // 선택된 오브젝트에 대한 건물 배치 상태를 설정하고 입력 이벤트를 등록
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

    // 건물 배치 
    // 마우스 위치를 기준으로 그리드 위치를 계산하여 건물 배치 상태의 OnAction 메서드 호출
    public void PlaceStructure(Vector3Int gridPosition, bool isfirstbuild)
    {
        
        // 설치 가능 여부 확인
        /*
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
            return;
        */
        // 첫 설치면 생성해라.
        if(isfirstbuild)
        {
            // 설치
            GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
            newObject.transform.position = grid.CellToWorld(gridPosition);
            placedGameObjects.Add(newObject);

           // buildingState.OnAction(gridPos);
        }
        
        // 바닥/건물 구조물 구분
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
           floorData :
           furnitureData;

        // 설치정보 저장
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

        // 이 위치는 이미 배치되어있음
        // 이미 배치되어있을 때 => 설치 위치를 주변 다른 위치로 바꿔주어야함
        // preview.UpdatePosition(grid.CellToWorld(gridPosition), false);

    }

    /*
    // 설치 가능 여부
    public bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        // ID가 0 => 바닥 설치
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
            floorData :
            furnitureData;

        return selectedData.CanPlaceObejctAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }
    */

    // 건물 배치 또는 제거를 중지
    // 현재 상태를 종료하고 입력 이벤트를 해제
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

    // 마우스 위치가 변경될 때 건물 배치 상태를 업데이트 => OnMouseDrag
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
