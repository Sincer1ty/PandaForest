using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel_Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        transform.gameObject.SetActive(true);
    }

}
