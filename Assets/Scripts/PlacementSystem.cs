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

    // 바닥설치와 가구설치 구분
    private GridData floorData, furnitureData;

    // 설치 가능여부 색깔로 표시
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
        // 바닥 / 구조물 구분
        floorData = new();
        furnitureData = new();

        // previewRenderer = cellIndicator.GetComponent<SpriteRenderer>();
    }

    // 인벤 클릭 이벤트
    public void StartPlacement(int ID)
    {
        // StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if(selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        // gridVisualization.SetActive(true); // 일단 안만듦

        // cellIndicator.SetActive(true); // 삭제

        // 프리뷰 보여주기
        /*
        preview.StartShowingPlacementPreview(
            database.objectsData[selectedObjectIndex].Prefab,
            database.objectsData[selectedObjectIndex].Size);
        */

        // 설치하려고 클릭
        inputManager.OnClicked += PlaceStructure;
        // 설치 나가기
        inputManager.OnExit += StopPlacement;
    }

    // 설치 진행
    // 화면 상 가운데 설치됨
    private void PlaceStructure()
    {
        /*
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        */
        // --- 추가 ---
        // 화면 중앙 좌표 가져오기
        ScreenCenter = new Vector3(Camera.transform.position.x, Camera.transform.position.y);
        Debug.Log(ScreenCenter);
        // 그리드 셀 좌표로 가져오기
        Vector3Int gridPosition = grid.WorldToCell(ScreenCenter);

        //Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        // 마우스 위치를 그리드 셀 좌표로 가져오기
        //Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // 설치 가능 여부 확인
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if(placementValidity == false)
            return;
        
        // 설치 사운드 넣으려면 여기에 .Play()

        // 설치
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition); 
        placedGameObjects.Add(newObject);

        // 바닥/건물 구조물 구분
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
           floorData :
           furnitureData;

        // 설치정보 저장
        selectedData.AddObjectAt(gridPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID,
            placedGameObjects.Count - 1);

        // 이 위치는 이미 배치되어있음
        preview.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    // 배치 가능 여부
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
        // cellIndicator.SetActive(false); //제거
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
            // 설치 불가능한 영역이면 빨간색
            // previewRenderer.color = placementValidity ? Color.white : Color.red; 

            mouseIndicator.transform.position = mousePosition;
            //cellIndicator.transform.position = grid.CellToWorld(gridPosition);
            preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
        }
                
    }
}
