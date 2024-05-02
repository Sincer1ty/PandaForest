using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    //�̱���
    private static UIPanel instance;

    public static UIPanel Instance
    {
        get
        {
            if (!instance) //null �̸�
            {
                instance = FindObjectOfType<UIPanel>();

                if (!instance)
                {
                    Debug.LogWarning("UIPanel�� �����մϴ�.");
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
        //Setting Panel �� �ƴϸ�
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

        //�г� ��� ��õ�
        Time.timeScale = 1f;
        //��� UI ��ư visible
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
