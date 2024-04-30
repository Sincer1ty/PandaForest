using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �׸��� ��� ������Ʈ ��ġ ����
public class GridData 
{
    // ��ųʸ��� �׸��� ��ġ�� PlacementData ����
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    // �׸��� ��ġ�� ������Ʈ �߰�
    // �ش� ��ġ�� �ٸ� ������Ʈ �����ϴ� ��� ���� �߻�
    // ũ��-��ġ ��� -> �� ��ġ�� PlacementData ����
    public void AddObjectAt(Vector3Int gridPosition,
                            Vector2Int objectSize,
                            int ID/*,
                            int placedObjectIndex*/)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, ID/*, placedObjectIndex*/);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
                throw new Exception($"Dictionary already contains this cell positiojn {pos}");
            placedObjects[pos] = data;
        }
    }

    // �̵�
    /*
    public void MoveObject(Vector3Int oldPosition, Vector3Int newPosition, Vector2Int objectSize)
    {
        if (!CanPlaceObejctAt(newPosition, objectSize))
            return; // �̵��� ��ġ�� �̹� �ٸ� ��ü�� ������ �̵����� ����

        PlacementData data = placedObjects[oldPosition];
        List<Vector3Int> newOccupiedPositions = CalculatePositions(newPosition, objectSize);

        // ���� ��ġ�� �����͸� ����
        foreach (var pos in data.occupiedPositions)
        {
            placedObjects.Remove(pos);
        }

        // ���ο� ��ġ�� ������ �߰�
        foreach (var pos in newOccupiedPositions)
        {
            placedObjects[pos] = data;
        }

        // ���� ��ġ�� ���ο� ��ġ�� ������Ʈ
        data.occupiedPositions = newOccupiedPositions;
    }
    */
    
    // ������Ʈ�� �����ϴ� ��� ��ġ ���
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

    // ������Ʈ ��ġ �� �� �ִ°�? 
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

    // �־��� �׸��� ��ġ�� ���� ǥ�� �ε��� ��ȯ
    // �ش� ��ġ�� ������Ʈ ������ -1 ��ȯ
    internal int GetRepresentationIndex(Vector3Int gridPosition)
    {
        if (placedObjects.ContainsKey(gridPosition) == false)
            return -1;
        return placedObjects[gridPosition].PlacedObjectIndex;
    }

    // ������Ʈ ���� 
    internal void RemoveObjectAt(Vector3Int gridPosition)
    {
        foreach (var pos in placedObjects[gridPosition].occupiedPositions)
        {
            placedObjects.Remove(pos);
        }
    }
}

// ������Ʈ ��ġ ���� ����
// ������Ʈ�� �����ϴ� ��� ��ġ�� �����ϴ� List , ID , ǥ�� �ε��� 
public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; } // 
    public int PlacedObjectIndex { get; private set; } // ���� �ý��� ���� �� ����

    public PlacementData(List<Vector3Int> occupiedPositions, int iD/*, int placedObjectIndex*/)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        // PlacedObjectIndex = placedObjectIndex;
    }
}