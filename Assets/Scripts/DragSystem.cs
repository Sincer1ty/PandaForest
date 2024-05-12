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

    Vector2 mousePos; // 마우스 좌표
    Vector2 localPos; // 변환된 canvas내 좌표
    private RectTransform rectFloating;

    // private Placement PlacementSystem;

    private void Start()
    {
        // 그리드 컴포넌트 
        GameObject gridobject = GameObject.Find("Grid");
        grid = gridobject.GetComponent<Grid>();

        // 타일맵 컴포넌트 
        GameObject tileobject = gridobject.transform.GetChild(0).gameObject;
        tilemap = tileobject.GetComponent<Tilemap>();

        // 플로팅 UI 
        canvas2 = GameObject.Find("Canvas2");
        floatingUI = canvas2.transform.GetChild(0).gameObject;

        // 캔버스 좌표 
        rt = canvas2.transform as RectTransform;

        rectFloating = floatingUI.gameObject.GetComponent<RectTransform>();
    }

    // 마우스 또는 터치 했을 때 
    void OnMouseDown()
    {
        print("기존 위치 "+ transform.position);

        floatingUI.SetActive(true);
    }

    // 마우스 드래그 (터치 가능)
    void OnMouseDrag()
    {
        // 구조물이 마우스 따라다니도록

        // 타일맵 기준
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
        Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
        transform.position = tilemap.GetCellCenterWorld(cellPosition);

        // 월드 좌표를 스크린 좌표로 변환
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        
        // 스크린 좌표를 canvas내에서의 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, screenPos, null, out localPos);
        rectFloating.localPosition = localPos;

    }

    // 드래그 놓았을 때 실행 (터치 가능)
    void OnMouseUp()
    {
        print("건물 위치 : "+ transform.position);
    }

}
