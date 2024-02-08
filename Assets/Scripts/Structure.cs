using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    // 타일에 건축물이 건설되어 있는지 검사하는 변수
    public bool IsBuildStructure { set; get; }

    private void Awake()
    {
        IsBuildStructure = false;
    }
}
