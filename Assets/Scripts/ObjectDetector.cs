using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private StructureSpawner structureSpawner;

    private Camera      mainCamera;
    private Ray         ray;
    private RaycastHit  hit;

    public Panel_Inventory panel_Inventory;
    private void Awake()
    {
        // "Maincamera" �±׸� ������ �ִ� ������Ʈ ��� �� Camera ������Ʈ ���� ����
        // Gameobject.FindGameobjectwithTag("Maincamera").GetComponent<Camera>(); �� ����
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        // ���콺 ���� ��ư�� ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            // ī�޶� ��ġ���� ȭ���� ���콺 ��ġ�� �����ϴ� ���� ����
            // ray.origin : ������ ������ġ(= ī�޶� ��ġ) 
            // ray.direction : ������ �������
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // 2D ����͸� ���� 3D ������ ������Ʈ�� ���콺�� �����ϴ� ���
            // ������ �ε����� ������Ʈ�� �����ؼ� hit�� ����
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // ������ �ε��� ������Ʈ�� �±װ� "Tile"�̸�
                if (hit.transform.CompareTag("Tile"))
                {
                    // Ÿ���� �����ϴ� SpawnTower() ȣ��
                    structureSpawner.SpawnStructure(hit.transform);
                }
                
                if (hit.transform.CompareTag("Building"))
                {
                    Debug.Log("Inventory Open");
                    panel_Inventory.Show();
                }
            }
        }
    }
}
