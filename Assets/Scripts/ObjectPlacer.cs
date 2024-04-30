using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 오브젝트를 배치하고 제거하는 기능 담당
public class ObjectPlacer : MonoBehaviour
{
    // 배치된 게임 오브젝트들 저장
    [SerializeField]
    private List<GameObject> placedGameObjects = new();

    // 프리팹을 주어진 위치에 배치
    // 배치된 오브젝트를 리스트에 추가. 배치된 오브젝트 인덱스 반환
    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;
        placedGameObjects.Add(newObject);
        return placedGameObjects.Count - 1;
    }

    // 주어진 인덱스에 해당하는 게임오브젝트 제거
    // 유효하지 않다면 아무것도 하지말기
    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObjects.Count <= gameObjectIndex
            || placedGameObjects[gameObjectIndex] == null)
            return;
        Destroy(placedGameObjects[gameObjectIndex]);
        placedGameObjects[gameObjectIndex] = null;
    }
}
