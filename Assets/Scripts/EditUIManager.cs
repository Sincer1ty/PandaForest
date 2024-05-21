using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditUIManager : MonoBehaviour
{
    public GameObject EditUI;
    private Animator PanelAnim;

    public bool isEditUIDown;

    public GameObject FloatingUI;

    public int index = -1;

    public bool isFloatCancel = false;
    public bool isFloatOK = false;

    public string ObjectTag;

    [SerializeField]
    private Placement placement;

    private Dictionary<string, int> tagToIdMap;

    private void Start()
    {
        PanelAnim = EditUI.GetComponent<Animator>();
        InitializeTagToIdMap();
    }

    // �±� <-> ID  ���� : ��ųʸ��� ���� 
    private void InitializeTagToIdMap()
    {
        tagToIdMap = new Dictionary<string, int>
        {
            { "ID_0", 0 },
            { "ID_1", 1 }

            // �ʿ��� �ٸ� �±׿� ID �߰�
        };
    }

    public void Floating_Cancel() // ��� Ŭ�� 
    {
        // ���� ��ġ�� ���ư��� 

        FloatingUI.SetActive(false);
        isFloatCancel = true;
    }

    public void Floating_Ok() // �Ϸ� Ŭ��
    {
        FloatingUI.SetActive(false);
        isFloatOK = true;
    }

    // ����UI Down/Up
    public void EditUIDown()
    {
        isEditUIDown = !isEditUIDown;
        PanelAnim.SetBool("isDown", isEditUIDown);
        
    }

    // �±� -> ID 
    public bool GetInfo(Vector3 position , string TagId)
    {
        if (tagToIdMap.TryGetValue(TagId, out int ObjectId))
        {
            return placement.EditStructure(position, ObjectId);
        }
        else
        {
            Debug.LogError($"Invalid TagId: {TagId}");
            return false;
        }

    }
}
