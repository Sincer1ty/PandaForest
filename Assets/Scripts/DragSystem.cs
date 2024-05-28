using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

// 건물 프리팹에 들어가는 스크립트 
public class DragSystem : MonoBehaviour
{
    float distance = 10;

    private Grid grid;
    private Tilemap tilemap;
    private GameObject floatingUI;
    GameObject canvas2;
    public RectTransform rt;
    EditUIManager editUIManager;

    GameObject currentObj;

    public static Vector3 originPosition ;
    public static string originTag;

    Vector2 localPos; // 변환된 canvas내 좌표
    private RectTransform rectFloating;

    GameObject blocker; // 클릭 이벤트를 차단할 UI 오브젝트


    private void Start()
    {
        // 그리드 컴포넌트 
        grid = GameObject.Find("Grid").GetComponent<Grid>();

        // 타일맵 컴포넌트 
        tilemap = grid.transform.GetChild(0).GetComponent<Tilemap>();
        
        // 플로팅 UI 
        canvas2 = GameObject.Find("Canvas2");
        floatingUI = canvas2.transform.GetChild(1).gameObject;

        // 캔버스 좌표 
        rt = canvas2.transform as RectTransform;
        rectFloating = floatingUI.gameObject.GetComponent<RectTransform>();

        // EditUIManager 스크립트 
        editUIManager = GameObject.Find("EditUIManager").GetComponent<EditUIManager>();

        // 클릭 이벤트를 차단할 UI 오브젝트
        blocker = editUIManager.Blocker;

    }


    private void Update()
    {
        // 취소 클릭 시 : 원래 위치로 돌아감 
        if (editUIManager.isFloatCancel) 
        {
            // 현재 이동중인 오브젝트 태그로 구분 
            currentObj = GameObject.FindWithTag("Building");
            print("이전 위치로 돌아갑니다.  " + originPosition);
            currentObj.transform.position = originPosition;

            currentObj.tag = originTag; // 원래 태그로 돌려놓기 
            editUIManager.isFloatCancel = false;
        }

        // 완료 클릭 시 : ID 넘겨주기
        if (editUIManager.isFloatOK)
        {
            // 현재 이동중인 오브젝트 태그로 구분 
            currentObj = GameObject.FindWithTag("Building");
            currentObj.tag = originTag; // 원래 태그로 돌려놓기 


            bool canPlace = editUIManager.GetInfo(currentObj.transform.position, currentObj.tag); // 정보 넘겨주기 

            if (!canPlace)
            {
                print("설치할 수 없습니다. 원래 자리로 돌아갑니다.");
                currentObj.transform.position = originPosition;
            }

            editUIManager.isFloatOK = false;


        }
    }

    // 마우스 또는 터치 했을 때 
    void OnMouseDown()
    {
        // 특정 UI 오브젝트가 활성화되어 있는지 확인
        if (blocker.activeSelf)
        {
            // 마우스 포인터가 UI 요소 위에 있는지 확인
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // 포인터가 UI 요소 위에 있으므로 클릭 이벤트를 차단합니다.
                return;
            }
        }

        originTag = gameObject.tag;
        gameObject.tag = "Building";
        
        // 클릭한 오브젝트가 "Building" 태그를 가진 경우만 상호작용
        if (gameObject.CompareTag("Building"))
        {
            floatingUI.SetActive(true);
            blocker.SetActive(true);

            currentObj = GameObject.FindWithTag("Building");
            originPosition = currentObj.transform.position;

            print("기존 위치 : " + originPosition);
        }
    }

    // 마우스 드래그 (터치 가능)
    void OnMouseDrag()
    {
        // 구조물이 마우스 따라다니도록

        // 현재 오브젝트가 "Building" 태그를 가진 경우만 드래그 가능
        if (gameObject.CompareTag("Building"))
        {
            // 구조물이 마우스 따라다니도록
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
            Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
            transform.position = tilemap.GetCellCenterWorld(cellPosition);

            // 월드 좌표를 스크린 좌표로 변환
            var screenPos = Camera.main.WorldToScreenPoint(transform.position);

            // 스크린 좌표를 canvas내에서의 좌표로 변환
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, screenPos, null, out localPos);
            rectFloating.localPosition = localPos;
        }

    }

    // 드래그 놓았을 때 실행 (터치 가능)
    void OnMouseUp()
    {

    }
}
