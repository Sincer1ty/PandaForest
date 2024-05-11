using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DragSystem : MonoBehaviour
{
    float distance = 10;

    private Grid grid;
    private Tilemap tilemap;
    private GameObject floatingUI;
    GameObject canvas2;
    public RectTransform rt;

    Vector3Int gridPosition;

    Vector2 mousePos; // ���콺 ��ǥ
    Vector2 localPos; // ��ȯ�� canvas�� ��ǥ
    private RectTransform rectFloating;

    // private Placement PlacementSystem;

    private void Start()
    {
        // �׸��� ������Ʈ 
        GameObject gridobject = GameObject.Find("Grid");
        grid = gridobject.GetComponent<Grid>();

        // Ÿ�ϸ� ������Ʈ 
        GameObject tileobject = gridobject.transform.GetChild(0).gameObject;
        tilemap = tileobject.GetComponent<Tilemap>();

        // �÷��� UI 
        canvas2 = GameObject.Find("Canvas2");
        floatingUI = canvas2.transform.GetChild(0).gameObject;

        // ĵ���� ��ǥ 
        rt = canvas2.transform as RectTransform;

        rectFloating = floatingUI.gameObject.GetComponent<RectTransform>();
    }

    // ���콺 �Ǵ� ��ġ ���� �� 
    void OnMouseDown()
    {
        print("���� ��ġ "+ transform.position);

        floatingUI.SetActive(true);
    }

    // ���콺 �巡�� (��ġ ����)
    void OnMouseDrag()
    {
        // �������� ���콺 ����ٴϵ���

        // �׸��� ����
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
        //gridPosition = grid.WorldToCell(mousePosition); // �׸��弿 ��ǥ�� ��ȯ
        //transform.position = grid.GetCellCenterWorld(gridPosition);

        // Ÿ�ϸ� ����
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
        Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
        transform.position = tilemap.GetCellCenterWorld(cellPosition);


        // ���콺 ��ǥ�� canvas�������� ��ǥ�� ��ȯ
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, mousePos, canvas2.worldCamera, out localPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, null, out localPos);

        rectFloating.anchoredPosition = localPos;
    }

    // �巡�� ������ �� ���� (��ġ ����)
    void OnMouseUp()
    {
        // Vector3 objPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        // gridPosition = grid.WorldToCell(objPosition); // �׸��弿 ��ǥ�� ��ȯ

        print(transform.position);

    }

}
