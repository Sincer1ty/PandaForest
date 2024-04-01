using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    
    public static UIManager Instance
    {
        get
        {
            if (!instance) //null �̸�
            {
                instance = FindObjectOfType<UIManager>();

                if (!instance)
                {
                    Debug.LogWarning("UIManager�� �����մϴ�.");
                    GameObject container = new GameObject();
                    container.name = "UIManager";
                    instance = container.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    //private Stack<UIPopup> openPopups = new Stack<UIPanel>();

    // Update is called once per frame
    void Update()
    {
        //�ڷΰ��� �Ⱦ� ���� (ȸ�� �Ȱ� ÷��)
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    //�ֱ� �˾� �ݱ�
        //}
    }

    public void OpenPopup(UIPopup popup)
    {
        //���� ���� �ʿ�
        if (popup != null)
        {
            //�˾� ����
            popup.Show();
            //openPopups.Push(popup);
        }
    }


}