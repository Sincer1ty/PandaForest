using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDatabase : MonoBehaviour
{
    public void ObjDataSetting()
    {
        
    }
    
}

public class ObjData
{
    [field: SerializeField]
    public string Name { get; private set; }
    // [field: SerializeField]
    public int ID { get; private set; }
    // [field: SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;
    // [field: SerializeField]
    public GameObject Prefab { get; private set; }

    public ObjData(string name, int id, Vector2Int size, GameObject prefab)
    {
        Name = name;
        ID = id;
        Size = size;
        Prefab = prefab;
    }
}