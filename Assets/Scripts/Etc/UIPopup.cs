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

        //패널 배경 일시정지
        Time.timeScale = 0;
    }

    public void Close()
    {
        if (popupCanvas != null)
        {
            popupCanvas.SetActive(false);
        }        

        //패널 배경 재시동
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
