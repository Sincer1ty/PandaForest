using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 그리드 기반 오브젝트 배치 관리
public class GridData : MonoBehaviour
{
    // 딕셔너리로 그리드 위치와 PlacementData 매핑
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    // 그리드 위치에 오브젝트 추가
    public void AddObjectAt(Vector3Int gridPosition,
                            Vector2Int objectSize,
                            int ID,
                            int placedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize); // 차지하는 위치 계산해서 리스트에 넣어두기
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex);
        foreach (var pos in positionToOccupy) // 차지하는 모든 위치의 데이터
        {
            if (placedObjects.ContainsKey(pos)) // 이미 차지하는 위치인가?
                throw new Exception($"Dictionary already contains this cell positiojn {pos}");
            placedObjects[pos] = data;
        }
    }

    // 오브젝트가 차지하는 모든 위치 계산
    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();

        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, y, 0)); // (x, 0, y)
            }
        }

        return returnVal;
    }

    // 오브젝트 설치 할 수 있는가? 
    public bool CanPlaceObejctAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
                return false;
        }
        return true;
    }

    // 주어진 그리드 위치에 대한 표현 인덱스 반환
    // 해당 위치에 오브젝트 없으면 -1 반환
    /*
    internal int GetRepresentationIndex(Vector3Int gridPosition)
    {
        if (placedObjects.ContainsKey(gridPosition) == false)
            return -1;
        return placedObjects[gridPosition].PlacedObjectIndex;
    }

    // 오브젝트 제거 
    internal void RemoveObjectAt(Vector3Int gridPosition)
    {
        foreach (var pos in placedObjects[gridPosition].occupiedPositions)
        {
            placedObjects.Remove(pos);
        }
    }
    */
}


// 오브젝트 배치 정보 저장 : 오브젝트가 차지하는 모든 위치를 저장하는 List , ID , 표현 인덱스 
public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; } // 
    public int PlacedObjectIndex { get; private set; } // 제거 시스템 만들 때 유용

    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        PlacedObjectIndex = placedObjectIndex;
    }
}