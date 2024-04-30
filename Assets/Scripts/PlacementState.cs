using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

// 건물 배치 상태에 필요한 동작 정의
public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectsDatabaseSO database;
    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;
    // SoundFeedback soundFeedback;

    // 건물 배치 상태의 생성자
    // 주어진 ID에 해당하는 오브젝트를 찾고, 미리보기 시작
    public PlacementState(int iD,
                          Grid grid,
                          // PreviewSystem previewSystem,
                          ObjectsDatabaseSO database,
                          GridData floorData,
                          GridData furnitureData
                          // ,ObjectPlacer objectPlacer
                          //, SoundFeedback soundFeedback
                          )
    {
        ID = iD;
        this.grid = grid;
        // this.previewSystem = previewSystem;
        this.database = database;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        // this.objectPlacer = objectPlacer;
        // this.soundFeedback = soundFeedback;

        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        /*
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(
                database.objectsData[selectedObjectIndex].Prefab,
                database.objectsData[selectedObjectIndex].Size);
        }
        else
            throw new System.Exception($"No object with ID {iD}");
        */
    }

    // 미리보기 시스템 종료
    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    // 배치의 유효성을 확인하고
    // 오브젝트를 배치하고
    // 배치된 오브젝트를 데이터에 추가
    public void OnAction(Vector3Int gridPosition)
    {

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            // soundFeedback.PlaySound(SoundType.wrongPlacement);
           
            Debug.Log("여기에 설치 못함");
            return;
        }
        // soundFeedback.PlaySound(SoundType.Place);
        /*
        int index = objectPlacer.PlaceObject(database.objectsData[selectedObjectIndex].Prefab,
             grid.CellToWorld(gridPosition));
        */
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
            floorData :
            furnitureData;
        selectedData.AddObjectAt(gridPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID /*,
            index*/);

        // previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    // 배치 가능한지 확인
    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
            floorData :
            furnitureData;

        return selectedData.CanPlaceObejctAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    // 미리보기 시스템의 위치 업데이트, 배치 유효성 확인
    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        // previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }
}