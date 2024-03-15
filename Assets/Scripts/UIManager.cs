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
            if (!instance) //null 이면
            {
                instance = FindObjectOfType<UIManager>();

                if (!instance)
                {
                    Debug.LogWarning("UIManager를 생성합니다.");
                    GameObject container = new GameObject();
                    container.name = "UIManager";
                    instance = container.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    //private Stack<UIPopup> openPopups = new Stack<UIPanel>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //뒤로가기 안쓸 예정 (회의 안건 첨부)
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    //최근 팝업 닫기
        //}
    }

    //public void OpenPopup(UIPanel popup)
    //{
    //    if(popup != null)
    //    {
    //        //팝업 열기
    //    }
    //}


}
