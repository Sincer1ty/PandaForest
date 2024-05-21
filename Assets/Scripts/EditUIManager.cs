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

    // 태그 <-> ID  매핑 : 딕셔너리로 관리 
    private void InitializeTagToIdMap()
    {
        tagToIdMap = new Dictionary<string, int>
        {
            { "ID_0", 0 },
            { "ID_1", 1 }

            // 필요한 다른 태그와 ID 추가
        };
    }

    public void Floating_Cancel() // 취소 클릭 
    {
        // 원래 위치로 돌아가기 

        FloatingUI.SetActive(false);
        isFloatCancel = true;
    }

    public void Floating_Ok() // 완료 클릭
    {
        FloatingUI.SetActive(false);
        isFloatOK = true;
    }

    // 편집UI Down/Up
    public void EditUIDown()
    {
        isEditUIDown = !isEditUIDown;
        PanelAnim.SetBool("isDown", isEditUIDown);
        
    }

    // 태그 -> ID 
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
