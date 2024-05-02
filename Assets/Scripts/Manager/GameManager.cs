using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (!instance) //null �̸�
            {
                instance = FindObjectOfType<GameManager>();
                
                if (!instance)
                {
                    Debug.LogWarning("GameManager�� �����մϴ�.");
                    GameObject container = new GameObject();
                    container.name = "GameManager";
                    instance = container.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Save();
        }
    }

    public void Save()
    {
        Debug.Log("Save");
        Application.Quit();
    }

}