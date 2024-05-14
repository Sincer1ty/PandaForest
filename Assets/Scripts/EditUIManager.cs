using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditUIManager : MonoBehaviour
{
    public GameObject EditUI;
    Animator PanelAnim;

    public bool isEditUIDown;

    public GameObject FloatingUI;

    public int index = -1;

    public bool isFloatCancel = false;
    public bool isFloatOK = false;

    public string ObjectTag;

    [SerializeField]
    private Placement placement;

    private void Start()
    {
        PanelAnim = EditUI.GetComponent<Animator>();
    }

    public void Floating_Cancel() // ��� Ŭ�� 
    {
        // ���� ��ġ�� ���ư��� 

        FloatingUI.SetActive(false);
        isFloatCancel = true;
    }

    public void Floating_Ok() // �Ϸ� Ŭ��
    {
        // ��ġ �������� Ȯ�� �� 
        // ��ġ ���� : ��ġ�ϱ� 
        // ��ġ �Ұ��� : ���� �ڸ��� �������� 

        FloatingUI.SetActive(false);
        isFloatOK = true;
    }


    public void EditUIDown()
    {
        // ����UI Down
        if (!isEditUIDown)
        {
            PanelAnim.SetBool("isDown", true);
            isEditUIDown = true;
        }
        else // ����UI Up
        {
            PanelAnim.SetBool("isDown", false);
            isEditUIDown = false;
        }

    }

    // �±� -> ID 
    public void GetInfo(Vector3 position , string TagId)
    {
        int ObjectId = -1;

        if(TagId == "ID_0")
        {
            ObjectId = 0;
        }
        else if (TagId == "ID_1")
        {
            ObjectId = 1;
        }

        placement.EditStructure(position, ObjectId);
    }
}
