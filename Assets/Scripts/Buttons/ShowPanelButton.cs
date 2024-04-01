using UnityEngine;
public class ShowPanelButton : MonoBehaviour
{
    public string PanelId;

    public void DoShowPanel()
    {
        UIPanel.Instance.Show(PanelId);
    }
}
