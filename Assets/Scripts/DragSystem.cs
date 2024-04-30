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
        // �׸���
        GameObject gridParent = GameObject.Find("GridParent");
        GameObject gridobject = gridParent.transform.GetChild(0).gameObject; // ù��° �ڽ�
        grid = gridobject.GetComponent<Grid>();

        // ��ġ
        GameObject BuildingSystem = GameObject.Find("BuildingSystem");
        GameObject placementobject = BuildingSystem.transform.GetChild(1).gameObject; //�ι�° �ڽ�
        placement = placementobject.GetComponent<Placement>();

    }

    // ���콺 �巡�� (��ġ ����)
    void OnMouseDrag()
    {
        // ��������ΰ�?
        
        // �������� ���콺 ����ٴϵ���
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // �׸��� �� ��ǥ�� ��������
        gridPosition = grid.WorldToCell(objPosition);
        transform.position = grid.GetCellCenterWorld(gridPosition);
        //transform.position = gridPosition;


    }

    // �巡�� ������ �� ���� (��ġ ����)
    void OnMouseUp()
    {
        // ��������ΰ�?

        Vector3 objPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        // �׸��� �� ��ǥ�� ��������
        gridPosition = grid.WorldToCell(objPosition);
        
        if (gameObject.tag == "Id_1")
        {
            selectedObjectIndex = 1;
            
        }

        // �ǹ��� �̵���Ű�� MoveObject �޼��� ȣ�� (������ġ, ���ο���ġ, ������)
        // griddata.MoveObject(gridPos, gridPosition, selectedObjectIndex);

       
        placement.StartPlacement(1, gridPosition, false);

        // buildingState.OnAction(gridPosition);


        // Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        // ���콺 ��ġ�� �׸��� �� ��ǥ�� ��������
        // Vector3Int gridPosition = grid.WorldToCell(mousePosition);

    }

}
