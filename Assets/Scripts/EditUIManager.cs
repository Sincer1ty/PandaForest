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

    public void Floating_Cancel() // 취소 클릭 
    {
        // 원래 위치로 돌아가기 

        FloatingUI.SetActive(false);
        isFloatCancel = true;
    }

    public void Floating_Ok() // 완료 클릭
    {
        // 설치 가능한지 확인 후 
        // 설치 가능 : 설치하기 
        // 설치 불가능 : 원래 자리로 돌려놓기 

        FloatingUI.SetActive(false);
        isFloatOK = true;
    }


    public void EditUIDown()
    {
        // 편집UI Down
        if (!isEditUIDown)
        {
            PanelAnim.SetBool("isDown", true);
            isEditUIDown = true;
        }
        else // 편집UI Up
        {
            PanelAnim.SetBool("isDown", false);
            isEditUIDown = false;
        }

    }

    // 태그 -> ID 
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
