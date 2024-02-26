using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Drag : MonoBehaviour 
{ 
    float distance = 10;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    Vector3 OriginPosition;

    Transform OriginTransform;

    [SerializeField]
    static private bool isOnPlacementButton = false;  // 편집 버튼 눌렀는지 체크
        

    private void Awake()
    {
        // "Maincamera" 태그를 가지고 있는 오브젝트 담색 후 Camera 컴포넌트 정보 전달
        // Gameobject.FindGameobjectwithTag("Maincamera").GetComponent<Camera>(); 와 동일
        mainCamera = Camera.main;
        
    }

    // 원래 위치 저장하는 다른 방법 생각해봐야 할듯 ㅜ
    void Update()
    {
        // 구조물의 원래 위치 가져오기
        if (Input.GetMouseButtonDown(0) && isOnPlacementButton == true)
        {
            OriginPosition = transform.position;

            
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                        
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // 광선에 부딪힌 오브젝트의 태그가 "Tile"이면 => 타일 오브젝트를 가져와야하기 때문
                if (hit.transform.CompareTag("Tile"))
                {
                    OriginTransform = hit.transform;
                   
                }
            }
        }
    }

    // 편집모드 버튼
    public void RedayToPlacement()
    {
        // 편집모드 활성화 <-> 비활성화
        isOnPlacementButton = !isOnPlacementButton;
        
    }


    // 마우스 드래그
    void OnMouseDrag() 
    {
        if (isOnPlacementButton == false) 
        {            
            return;
        }
        
        // 구조물이 마우스 따라다니도록
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // transform.position = objPosition;

        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // 광선에 부딪힌 오브젝트의 태그가 "Tile"이면
            if (hit.transform.CompareTag("Tile"))
            {
                transform.position = hit.transform.position + Vector3.back;
            }
        }

    }
    

    // 드래그 놓았을 때 실행
    void OnMouseUp()
    {
        if (isOnPlacementButton == false)
        {
            return;
        }

        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // 2D 모니터를 통해 3D 월드의 오브젝트를 마우스로 선택하는 방법
        // 광선에 부딪히는 오브젝트를 검출해서 hit에 저장
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // 광선에 부딪힌 오브젝트의 태그가 "Tile"이면
            if (hit.transform.CompareTag("Tile"))
            {
                PutStructure(hit.transform); 
            }
            else
            {
                // 원래 자리로 돌아가기
                transform.position = OriginPosition;
            }

        }
        
    }

    public void PutStructure(Transform tileTransform)
    {
        // 해당 타일을 가져옴
        Structure structure = tileTransform.GetComponent<Structure>();
        

        // 건설되어 있을 때
        if (structure.IsBuildStructure == true)
        {
            // 원래 자리로 돌아가기
            transform.position = OriginPosition;
        }

        // 현재 타일 위치에 건설되어있지 않을 때
        if (structure.IsBuildStructure == false)
        {
            // 건물이 건설되어 있음으로 설정
            structure.IsBuildStructure = true;

            // 원래 위치에 아무것도 건설되어 있지 않게 되었으니..
            // 타일 원래 위치의 IsBuildStructure 를 false로 변경
            Structure OriginStructure = OriginTransform.GetComponent<Structure>();
            OriginStructure.IsBuildStructure = false;

        }

    }
}

