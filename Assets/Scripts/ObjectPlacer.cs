using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������Ʈ�� ��ġ�ϰ� �����ϴ� ��� ���
public class ObjectPlacer : MonoBehaviour
{
    // ��ġ�� ���� ������Ʈ�� ����
    [SerializeField]
    private List<GameObject> placedGameObjects = new();

    // �������� �־��� ��ġ�� ��ġ
    // ��ġ�� ������Ʈ�� ����Ʈ�� �߰�. ��ġ�� ������Ʈ �ε��� ��ȯ
    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;
        placedGameObjects.Add(newObject);
        return placedGameObjects.Count - 1;
    }

    // �־��� �ε����� �ش��ϴ� ���ӿ�����Ʈ ����
    // ��ȿ���� �ʴٸ� �ƹ��͵� ��������
    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObjects.Count <= gameObjectIndex
            || placedGameObjects[gameObjectIndex] == null)
            return;
        Destroy(placedGameObjects[gameObjectIndex]);
        placedGameObjects[gameObjectIndex] = null;
    }
}
