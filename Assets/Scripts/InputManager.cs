using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 입력 이벤트 관리, 마우스 입력 처리
public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera; 

    private Vector3 lastPosition; // 마지막으로 선택된 위치 저장

    // 추후 GetSelectedMapPosition 수정 필요
    // [SerializeField]
    // private LayerMask placementLayermask; // 레이캐스트가 충돌을 검출할 레이어 지정

    // OnClicked: 마우스 왼쪽 버튼을 클릭할 때 발생하는 이벤트
    // OnExit: Escape 키를 누를 때 발생하는 이벤트
    public event Action OnClicked, OnExit; 

   
    // 마우스 입력 및 키 입력 감지
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    // 모바일로 구현하면 필요 없을듯..?
    // 현재 마우스 포인터가 UI 요소 위에 있는지 확인
    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();

    // 레이 충돌 지점 반환 : 유저가 클릭한 위치 알아내기
    public Vector3 GetSelectedMapPosition()
    {
        // 레이캐스트 2D <-> 3D 때문에, 위치정보 가져오기 임시 구현함
        /*
        Vector3 mousePos = Input.mousePosition;
        // mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
        */

        // 화면의 마우스 좌표를 기준으로 게임 월드 상의 좌표를 구한다.
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        transform.position = sceneCamera.ScreenToWorldPoint(position);
        // z 위치를 0으로 설정
        transform.position = new Vector3(transform.position.x, transform.position.y);

        return transform.position;
    }
}
