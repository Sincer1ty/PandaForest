using UnityEngine;
public class ShowPanelButton : MonoBehaviour
{
    public string PanelId;

    public void DoShowPanel()
    {
        Debug.Log("DoShowPanel");
        UIPanel.Instance.Show(PanelId);
    }
}
