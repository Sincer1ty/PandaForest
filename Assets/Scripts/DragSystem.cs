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

        // Ÿ�ϸ� ����
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
        Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
        transform.position = tilemap.GetCellCenterWorld(cellPosition);

        // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        
        // ��ũ�� ��ǥ�� canvas�������� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, screenPos, null, out localPos);
        rectFloating.localPosition = localPos;

    }

    // �巡�� ������ �� ���� (��ġ ����)
    void OnMouseUp()
    {
        print("�ǹ� ��ġ : "+ transform.position);
    }

}
