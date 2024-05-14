using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 건물 배치 및 제거
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

    private Vector3Int lastDetectedPosition = Vector3Int.zero; // 마지막으로 감지된 그리드 위치

    // private GridData floorData, furnitureData;
    private GridData StructureData;

    private List<GameObject> placedGameObjects = new();

    // 설치 가능여부 색깔로 표시
    private SpriteRenderer previewRenderer;

    [SerializeField]
    private EditUIManager UiManager;

    public void Start()
    {
        // StopPlacement();
        // floorData = new();
        // furnitureData = new();
        StructureData = new();
        // previewRenderer = cellIndicator.GetComponent<SpriteRenderer>();
    }

    /*
    // 선택된 오브젝트에 대한 건물 배치 상태를 설정하고 입력 이벤트를 등록
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

    // 버튼 클릭시 중앙에 생성 : ID 받아오기
    public void PlaceStructure(int ID)
    {
        // 존재 유무 확인 
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        // 화면 중앙 좌표 가져오기
        ScreenCenter = new Vector3(Camera.transform.position.x, Camera.transform.position.y);
        Debug.Log(ScreenCenter);

        // 타일맵 셀 좌표로 가져오기
        Vector3Int centerPosition = tilemap.WorldToCell(ScreenCenter);
        print(centerPosition);


        // 설치 가능 유무 확인
        bool placementValidity = CheckPlacementValidity(centerPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            // source.PlayOneShot(wrongPlacementClip);
            print("설치할 수 없습니다.");
            return;
        }
        print("설치가능 합니다.");
        // source.PlayOneShot(correctPlacementClip);


        // 중앙에 설치(생성)
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = tilemap.CellToWorld(centerPosition);
        placedGameObjects.Add(newObject);

        // 원래 여기서 바닥,건물 구분했었음
        GridData selectedData = StructureData;
        // 설치 데이터 전달 
        selectedData.AddObjectAt(centerPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID /*,
            placedGameObjects.Count - 1*/);

        // 편집 UI 내리기 
        UiManager.EditUIDown();

        // preview.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }


    // 편집 시, 위치정보 수정 
    public void EditStructure(Vector3 Position, int ID)
    {
        // 존재 유무 확인 
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        // 타일맵 셀 좌표로 가져오기
        Vector3Int EditPosition = tilemap.WorldToCell(Position);
        print("EditPosition = "+EditPosition);

        // 설치 가능 유무 확인
        bool placementValidity = CheckPlacementValidity(EditPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            print("설치할 수 없습니다.");
            return;
        }
        print("설치가능 합니다.");

        // 원래 여기서 바닥,건물 구분했었음
        GridData selectedData = StructureData;
        // 설치 데이터 전달 
        selectedData.AddObjectAt(EditPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID /*,
            placedGameObjects.Count - 1*/);

    }

    private void MoveStructure()
    {
        /* 
        // UI 요소 위에 마우스 포인터가 있으면 메서드를 종료 ?
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        */

    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        // 원래 여기서 바닥,건물 구분했었음
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

        // 프리뷰 색 바꿔주는 부분
        //bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        //previewRenderer.material.color = placementValidity ? Color.white : Color.red;


        // mouseIndicator.transform.position = mousePosition;
        // cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

}
