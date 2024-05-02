using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSystem : MonoBehaviour
{
    float distance = 10;

    private Grid grid;

    Vector3Int gridPosition;

    private void Start()
    {
        // �׸���
        GameObject gridobject = GameObject.Find("Grid");
        grid = gridobject.GetComponent<Grid>();


    }

    // ���콺 �巡�� (��ġ ����)
    void OnMouseDrag()
    {
        // �������� ���콺 ����ٴϵ���
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        gridPosition = grid.WorldToCell(objPosition); // �׸��弿 ��ǥ�� ��ȯ
        transform.position = grid.GetCellCenterWorld(gridPosition);
    }

    // �巡�� ������ �� ���� (��ġ ����)
    void OnMouseUp()
    {
        Vector3 objPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        gridPosition = grid.WorldToCell(objPosition); // �׸��弿 ��ǥ�� ��ȯ

        print(transform.position);
    }

}
