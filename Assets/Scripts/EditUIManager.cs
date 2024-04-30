using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditUIManager : MonoBehaviour
{
    public GameObject EditUI;
    Animator PanelAnim;

    public bool isEditUIDown;

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
        // ����UI Down
        if(!isEditUIDown)
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
}
