using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;

    // 추후 GetSelectedMapPosition 수정 필요
    // [SerializeField]
    // private LayerMask placementLayermask; // 그리드로 사용하려는 평면만 감지

    public event Action OnClicked, OnExit;

   
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    // 모바일로 구현하면 필요 없을듯..?
    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();


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
