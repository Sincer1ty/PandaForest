using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditUIManager : MonoBehaviour
{
    public GameObject EditUI;
    Animator PanelAnim;

    public bool isEditUIDown;

    public GameObject FloatingUI;

    // Start is called before the first frame update
    void Start()
    {
        PanelAnim = EditUI.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EditUIDown()
    {
        // 편집UI Down
        if(!isEditUIDown)
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

    public void Floating_Cancel()
    {
        FloatingUI.SetActive(false);

        // 원래 위치로 돌아가야 함
    }

    public void Floating_Ok()
    {
        FloatingUI.SetActive(false);

        // 그리드 데이터 저장
    }
}
