using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject StructurePrefab;

    [SerializeField]
    private FieldTemplate fieldTemplate;

    [SerializeField]
    private bool isOnFieldButton = false;  // �� �Ǽ� ��ư �������� üũ
    private GameObject followFieldClone = null; // �ӽ� Ÿ�� ��� �Ϸ� �� ������ ���� �����ϴ� ����

    public void RedayToSpawnField()
    {
        // ��ư�� �ߺ��ؼ� ������ ���� �����ϱ� ���� �ʿ�
        if( isOnFieldButton == true )
        {
            return;
        }

        // Ÿ�� �Ǽ� ���� ���� Ȯ��
        // Ÿ���� �Ǽ��� ��ŭ ���� ������ Ÿ�� �Ǽ� X => ���� ����

        // �� �Ǽ� ��ư�� �����ٰ� ����
        isOnFieldButton = true;

        // ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
        followFieldClone = Instantiate( fieldTemplate.followFieldPrefab );
        // followFieldClone = Instantiate(StructurePrefab);
    }

    public void SpawnStructure(Transform tileTransform) 
    {
        // Ÿ�� �Ǽ� ��ư�� ������ ���� Ÿ�� �Ǽ� ����
        if(isOnFieldButton == false)
        {
            return;
        }

        Structure structure = tileTransform.GetComponent<Structure>();
        
        // Ÿ�� �Ǽ� ���� ���� Ȯ��
        // 1. ���� Ÿ���� ��ġ�� �̹� Ÿ���� �Ǽ��Ǿ� ������ Ÿ�� �Ǽ� X
        if ( structure.IsBuildStructure == true)
        {
            return;
        }

        // �ٽ� Ÿ�� �Ǽ� ��ư�� ������ Ÿ���� �Ǽ��ϵ��� ���� ����
        isOnFieldButton = false;

        // Ÿ���� �Ǽ��Ǿ� �������� ����
        structure.IsBuildStructure = true;

        // ������ Ÿ���� ��ġ�� ���๰ �Ǽ�
        // Instantiate(StructurePrefab, tileTransform.position, Quaternion.identity);
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(fieldTemplate.fieldPrefab, position, Quaternion.identity);

        // Ÿ�� ���⿡ enemySpawner, playerGold, tile ���� ���� => ���� ����

        // Ÿ���� ��ġ�߱� ������ ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
        Destroy(followFieldClone);
    }

}
