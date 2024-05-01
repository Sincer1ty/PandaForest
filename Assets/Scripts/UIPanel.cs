using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    //싱글턴
    private static UIPanel instance;

    public static UIPanel Instance
    {
        get
        {
            if (!instance) //null 이면
            {
                instance = FindObjectOfType<UIPanel>();

                if (!instance)
                {
                    Debug.LogWarning("UIPanel를 생성합니다.");
                    GameObject container = new GameObject();
                    container.name = "UIPanel";
                    instance = container.AddComponent<UIPanel>();
                }
            }
            return instance;
        }
    }

    public GameObject BtnSetting;

    public List<PanelModel> Panels;

    private Queue<PanelInstanceModel> queue = new Queue<PanelInstanceModel>();

    public void Show(string panelId)
    {
        PanelModel panelModel = Panels.FirstOrDefault(panel => panel.PanelId == panelId);
        Debug.Log("Show "+panelId);
        
        if(panelModel != null)
        {
            Debug.Log(panelModel.PanelId);

            var newInstancePanel = Instantiate(panelModel.PanelPrefab, transform);

            queue.Enqueue(new PanelInstanceModel
            {
                PanelId = panelId,
                PanelInstance = newInstancePanel
            });
        }
        else
        {
            Debug.LogWarning($"Trying to use panelId = {panelId}, but this is not found in Panels");
        }
        
        Time.timeScale = 0;
        //Setting Panel 이 아니면
        if(panelId != "Panel_Setting")
            BtnSetting.SetActive(false);
    }

    public void Close()
    {
        if (AnyPanelShowing())
        {
            var lastPanel = queue.Dequeue();

            Destroy(lastPanel.PanelInstance);
        }

        //패널 배경 재시동
        Time.timeScale = 1f;
        //배경 UI 버튼 visible
        if(!BtnSetting.activeSelf)
            BtnSetting.SetActive(true);
    }

    public bool AnyPanelShowing()
    {
        return GetAmountPanelsInQueue() > 0;
    }

    public int GetAmountPanelsInQueue()
    {
        return queue.Count;
    }
}
