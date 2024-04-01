using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSystem : MonoBehaviour
{
    float distance = 10;

    private Grid grid;
    private Placement placement;
    Vector3Int gridPosition;

    private void Start()
    {
        // 그리드
        GameObject gridParent = GameObject.Find("GridParent");
        GameObject gridobject = gridParent.transform.GetChild(0).gameObject; // 첫번째 자식
        grid = gridobject.GetComponent<Grid>();

        // 설치
        GameObject BuildingSystem = GameObject.Find("BuildingSystem");
        GameObject placementobject = BuildingSystem.transform.GetChild(1).gameObject; //두번째 자식
        placement = placementobject.GetComponent<Placement>();

    }

    // 마우스 드래그 (터치 가능)
    void OnMouseDrag()
    {
        // 편집모드인가?
        
        // 구조물이 마우스 따라다니도록
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 그리드 셀 좌표로 가져오기
        gridPosition = grid.WorldToCell(objPosition);
        transform.position = grid.GetCellCenterWorld(gridPosition);

    }

    // 드래그 놓았을 때 실행 (터치 가능)
    void OnMouseUp()
    {
        // 편집모드인가?

        // 태그로 ID를 구분하기..?
        // placement.StartPlacement();

        // [ 설치 ]
        // 이미 설치되어 있으면 원래 위치로 돌아감
        // 설치 가능하면 
        // -> 1) 건물이 건설되어 있음으로 설정
        // -> 2) 원래 위치에 아무것도 건설되어 있지 않게 되었으니..
        // ->    타일 원래 위치를 건설될 수 있게 비워두기



        // Placement의 StartPlacement(int ID, Vector3Int gridPosition)

        // Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        // 마우스 위치를 그리드 셀 좌표로 가져오기
        // Vector3Int gridPosition = grid.WorldToCell(mousePosition);

    }

}
