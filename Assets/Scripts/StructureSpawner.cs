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
    private bool isOnFieldButton = false;  // 밭 건설 버튼 눌렀는지 체크
    private GameObject followFieldClone = null; // 임시 타워 사용 완료 시 삭제를 위해 저장하는 변수

    public void RedayToSpawnField()
    {
        // 버튼을 중복해서 누르는 것을 방지하기 위해 필요
        if( isOnFieldButton == true )
        {
            return;
        }

        // 타워 건설 가능 여부 확인
        // 타워를 건설할 만큼 돈이 없으면 타워 건설 X => 추후 구현

        // 밭 건설 버튼을 눌렀다고 설정
        isOnFieldButton = true;

        // 마우스를 따라다니는 임시 타워 생성
        followFieldClone = Instantiate( fieldTemplate.followFieldPrefab );
        // followFieldClone = Instantiate(StructurePrefab);
    }

    public void SpawnStructure(Transform tileTransform) 
    {
        // 타워 건설 버튼을 눌렀을 때만 타워 건설 가능
        if(isOnFieldButton == false)
        {
            return;
        }

        Structure structure = tileTransform.GetComponent<Structure>();
        
        // 타워 건설 가능 여부 확인
        // 1. 현재 타일의 위치에 이미 타워가 건설되어 있으면 타워 건설 X
        if ( structure.IsBuildStructure == true)
        {
            return;
        }

        // 다시 타워 건설 버튼을 눌러서 타워를 건설하도록 변수 설정
        isOnFieldButton = false;

        // 타워가 건설되어 있음으로 설정
        structure.IsBuildStructure = true;

        // 선택한 타일의 위치에 건축물 건설
        // Instantiate(StructurePrefab, tileTransform.position, Quaternion.identity);
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(fieldTemplate.fieldPrefab, position, Quaternion.identity);

        // 타워 무기에 enemySpawner, playerGold, tile 정보 전달 => 추후 구현

        // 타워를 배치했기 때문에 마우스를 따라다니는 임시 타워 삭제
        Destroy(followFieldClone);
    }

}
