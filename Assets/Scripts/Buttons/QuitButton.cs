using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void DoSave()
    {
        GameManager.Instance.Save();
    }
}
