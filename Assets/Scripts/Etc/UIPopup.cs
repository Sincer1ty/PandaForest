using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    [SerializeField]
    private GameObject popupCanvas;

    public void Show()
    {
        if (popupCanvas != null)
        {
            popupCanvas.SetActive(true);
        }

        //�г� ��� �Ͻ�����
        Time.timeScale = 0;
    }

    public void Close()
    {
        if (popupCanvas != null)
        {
            popupCanvas.SetActive(false);
        }        

        //�г� ��� ��õ�
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
