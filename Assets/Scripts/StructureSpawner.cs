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

        // ���콺�� ����ٴϴ� �ӽ� ������ ����
        followFieldClone = Instantiate( fieldTemplate.followFieldPrefab );
        
        // �Ǽ��� ����� �� �ִ� �ڷ�ƾ �Լ� ����
        StartCoroutine("OnFieldCancelSystem");
    }



    public void SpawnStructure(Transform tileTransform) 
    {
        Structure structure = tileTransform.GetComponent<Structure>();

        //  ���⼭���� Ȯ��
        // �Ǽ� ��ư�� ������ �� && ���� Ÿ�� ��ġ�� �Ǽ��Ǿ����� ���� ��
        if (isOnFieldButton == true && structure.IsBuildStructure == false)
        {
            // �ٽ� �Ǽ� ��ư�� ������ �Ǽ��ϵ��� ���� ����
            isOnFieldButton = false;

            // �ǹ��� �Ǽ��Ǿ� �������� ����
            structure.IsBuildStructure = true;

            // ������ Ÿ���� ��ġ�� ���๰ �Ǽ�
            // Instantiate(StructurePrefab, tileTransform.position, Quaternion.identity);
            Vector3 position = tileTransform.position + Vector3.back;
            GameObject clone = Instantiate(fieldTemplate.fieldPrefab, position, Quaternion.identity);

            // Ÿ�� ���⿡ enemySpawner, playerGold, tile ���� ���� => ���� ����

            // ��ġ�߱� ������ ���콺�� ����ٴϴ� �ӽ� ������ ����
            Destroy(followFieldClone);
            // �Ǽ��� ����� �� �ִ� �ڷ�ƾ �Լ� ����
            StopCoroutine("OnFieldCancelSystem");

            
        }


    }

    private IEnumerator OnFieldCancelSystem()
    {
        while(true)
        {
            // ESCŰ �Ǵ� ���콺 ������ ��ư�� ������ �� Ÿ�� �Ǽ� ���
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnFieldButton = false;
                // ���콺�� ����ٴϴ� �ӽ� ������ ����
                Destroy(followFieldClone);
                break;
            }
            yield return null;
        }
    }

}
