using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ǹ� ���� ��Ÿ���� �޼��� ����
public interface IBuildingState 
{
    void EndState(); // ���� ���� ����
    void OnAction(Vector3Int gridPosition); // Ư�� �׸��� ��ġ���� ���� ����
    void UpdateState(Vector3Int gridPosition); // ���� ������Ʈ
    
}
