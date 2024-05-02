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
        // 그리드
        GameObject gridobject = GameObject.Find("Grid");
        grid = gridobject.GetComponent<Grid>();


    }

    // 마우스 드래그 (터치 가능)
    void OnMouseDrag()
    {
        // 구조물이 마우스 따라다니도록
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        gridPosition = grid.WorldToCell(objPosition); // 그리드셀 좌표로 변환
        transform.position = grid.GetCellCenterWorld(gridPosition);
    }

    // 드래그 놓았을 때 실행 (터치 가능)
    void OnMouseUp()
    {
        Vector3 objPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        gridPosition = grid.WorldToCell(objPosition); // 그리드셀 좌표로 변환

        print(transform.position);
    }

}
