using UnityEngine;

public class ClosePanelButton : MonoBehaviour
{
    //private UIPanel uiPanel;

    // Start is called before the first frame update
    //void Start()
    //{
    //    uiPanel = UIPanel.Instance;
    //}

    public void DoClosePanel()
    {
        UIPanel.Instance.Close();
        //uiPanel.Close();
    }
}
