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
    static private bool isOnPlacementButton = false;  // ���� ��ư �������� üũ
        

    private void Awake()
    {
        // "Maincamera" �±׸� ������ �ִ� ������Ʈ ��� �� Camera ������Ʈ ���� ����
        // Gameobject.FindGameobjectwithTag("Maincamera").GetComponent<Camera>(); �� ����
        mainCamera = Camera.main;
        
    }

    // ���� ��ġ �����ϴ� �ٸ� ��� �����غ��� �ҵ� ��
    void Update()
    {
        // �������� ���� ��ġ ��������
        if (Input.GetMouseButtonDown(0) && isOnPlacementButton == true)
        {
            OriginPosition = transform.position;

            
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                        
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // ������ �ε��� ������Ʈ�� �±װ� "Tile"�̸� => Ÿ�� ������Ʈ�� �����;��ϱ� ����
                if (hit.transform.CompareTag("Tile"))
                {
                    OriginTransform = hit.transform;
                   
                }
            }
        }
    }

    // ������� ��ư
    public void RedayToPlacement()
    {
        // ������� Ȱ��ȭ <-> ��Ȱ��ȭ
        isOnPlacementButton = !isOnPlacementButton;
        
    }


    // ���콺 �巡��
    void OnMouseDrag() 
    {
        if (isOnPlacementButton == false) 
        {            
            return;
        }
        
        // �������� ���콺 ����ٴϵ���
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // transform.position = objPosition;

        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // ������ �ε��� ������Ʈ�� �±װ� "Tile"�̸�
            if (hit.transform.CompareTag("Tile"))
            {
                transform.position = hit.transform.position + Vector3.back;
            }
        }

    }
    

    // �巡�� ������ �� ����
    void OnMouseUp()
    {
        if (isOnPlacementButton == false)
        {
            return;
        }

        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // 2D ����͸� ���� 3D ������ ������Ʈ�� ���콺�� �����ϴ� ���
        // ������ �ε����� ������Ʈ�� �����ؼ� hit�� ����
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // ������ �ε��� ������Ʈ�� �±װ� "Tile"�̸�
            if (hit.transform.CompareTag("Tile"))
            {
                PutStructure(hit.transform); 
            }
            else
            {
                // ���� �ڸ��� ���ư���
                transform.position = OriginPosition;
            }

        }
        
    }

    public void PutStructure(Transform tileTransform)
    {
        // �ش� Ÿ���� ������
        Structure structure = tileTransform.GetComponent<Structure>();
        

        // �Ǽ��Ǿ� ���� ��
        if (structure.IsBuildStructure == true)
        {
            // ���� �ڸ��� ���ư���
            transform.position = OriginPosition;
        }

        // ���� Ÿ�� ��ġ�� �Ǽ��Ǿ����� ���� ��
        if (structure.IsBuildStructure == false)
        {
            // �ǹ��� �Ǽ��Ǿ� �������� ����
            structure.IsBuildStructure = true;

            // ���� ��ġ�� �ƹ��͵� �Ǽ��Ǿ� ���� �ʰ� �Ǿ�����..
            // Ÿ�� ���� ��ġ�� IsBuildStructure �� false�� ����
            Structure OriginStructure = OriginTransform.GetComponent<Structure>();
            OriginStructure.IsBuildStructure = false;

        }

    }
}

