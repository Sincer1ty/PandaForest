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

    // ���� GetSelectedMapPosition ���� �ʿ�
    // [SerializeField]
    // private LayerMask placementLayermask; // �׸���� ����Ϸ��� ��鸸 ����

    public event Action OnClicked, OnExit;

   
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    // ����Ϸ� �����ϸ� �ʿ� ������..?
    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();


    public Vector3 GetSelectedMapPosition()
    {
        // ����ĳ��Ʈ 2D <-> 3D ������, ��ġ���� �������� �ӽ� ������
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

        // ȭ���� ���콺 ��ǥ�� �������� ���� ���� ���� ��ǥ�� ���Ѵ�.
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        transform.position = sceneCamera.ScreenToWorldPoint(position);
        // z ��ġ�� 0���� ����
        transform.position = new Vector3(transform.position.x, transform.position.y);

        return transform.position;
    }
}
