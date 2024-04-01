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

    // 바닥설치와 가구설치 구분
    private GridData floorData, furnitureData;

    private List<GameObject> placedGameObjects = new();

    // [SerializeField]
    // private PreviewSystem preview;

    // private Vector3Int lastDetectedPosition = Vector3Int.zero;

    private void Start()
    {
        // 바닥/구조물 구분
        floorData = new();
        furnitureData = new();

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

    // 구조물 설치 
     public void PlaceStructure(Vector3Int gridPosition)
    {
        // 설치 가능 여부 확인
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
            return;

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
        // 이미 배치되어있을 때 => 설치 위치를 주변 다른 위치로 바꿔주어야함
        // preview.UpdatePosition(grid.CellToWorld(gridPosition), false);

    }

    // 설치 가능 여부
    public bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        // ID가 0 => 바닥 설치
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
            floorData :
            furnitureData;

        return selectedData.CanPlaceObejctAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

}
