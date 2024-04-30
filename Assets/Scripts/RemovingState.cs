using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 건물 제거 상태에 필요한 동작 정의
public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;
    Grid grid;
    PreviewSystem previewSystem;
    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;
    // SoundFeedback soundFeedback;

    public RemovingState(Grid grid,
                         /*PreviewSystem previewSystem,*/
                         GridData floorData,
                         GridData furnitureData
                         // , ObjectPlacer objectPlacer
                         /*, SoundFeedback soundFeedback*/)
    {
        this.grid = grid;
        // this.previewSystem = previewSystem;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;
        // this.soundFeedback = soundFeedback;
        previewSystem.StartShowingRemovePreview();
    }

    // 미리보기 종료
    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    // 오브젝트 제거
    public void OnAction(Vector3Int gridPosition)
    {
        GridData selectedData = null;
        if (furnitureData.CanPlaceObejctAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = furnitureData;
        }
        else if (floorData.CanPlaceObejctAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = floorData;
        }

        if (selectedData == null)
        {
            //sound
            // soundFeedback.PlaySound(SoundType.wrongPlacement);
        }
        else
        {
            // soundFeedback.PlaySound(SoundType.Remove);
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
                return;
            selectedData.RemoveObjectAt(gridPosition);
            objectPlacer.RemoveObjectAt(gameObjectIndex);
        }
        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition, CheckIfSelectionIsValid(gridPosition));
    }

    // 선택이 유효한지 확인
    private bool CheckIfSelectionIsValid(Vector3Int gridPosition)
    {
        return !(furnitureData.CanPlaceObejctAt(gridPosition, Vector2Int.one) &&
            floorData.CanPlaceObejctAt(gridPosition, Vector2Int.one));
    }

    // 상태 업데이트
    // 미리보기 시스템의 위치 업데이트
    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckIfSelectionIsValid(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
    }
}
