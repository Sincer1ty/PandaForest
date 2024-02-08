using UnityEngine;

// 스크립터블 오브젝트

[CreateAssetMenu]
public class FieldTemplate : ScriptableObject
{
    public GameObject fieldPrefab; // 밭 생성을 위한 프리팹
    public GameObject followFieldPrefab; // 임시 밭 프리팹

}
