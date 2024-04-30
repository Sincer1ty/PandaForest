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
    Vector3Int gridPos;

    private GridData griddata;

    int selectedObjectIndex;

    IBuildingState buildingState;

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
        //transform.position = gridPosition;


    }

    // 드래그 놓았을 때 실행 (터치 가능)
    void OnMouseUp()
    {
        // 편집모드인가?

        Vector3 objPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        // 그리드 셀 좌표로 가져오기
        gridPosition = grid.WorldToCell(objPosition);
        
        if (gameObject.tag == "Id_1")
        {
            selectedObjectIndex = 1;
            
        }

        // 건물을 이동시키는 MoveObject 메서드 호출 (이전위치, 새로운위치, 사이즈)
        // griddata.MoveObject(gridPos, gridPosition, selectedObjectIndex);

       
        placement.StartPlacement(1, gridPosition, false);

        // buildingState.OnAction(gridPosition);


        // Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        // 마우스 위치를 그리드 셀 좌표로 가져오기
        // Vector3Int gridPosition = grid.WorldToCell(mousePosition);

    }

}
