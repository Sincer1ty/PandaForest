using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    // Ÿ�Ͽ� ���๰�� �Ǽ��Ǿ� �ִ��� �˻��ϴ� ����
    public bool IsBuildStructure { set; get; }

    private void Awake()
    {
        IsBuildStructure = false;
    }
}
