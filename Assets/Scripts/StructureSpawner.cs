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

        // 마우스를 따라다니는 임시 구조물 생성
        followFieldClone = Instantiate( fieldTemplate.followFieldPrefab );
        
        // 건설을 취소할 수 있는 코루틴 함수 시작
        StartCoroutine("OnFieldCancelSystem");
    }



    public void SpawnStructure(Transform tileTransform) 
    {
        Structure structure = tileTransform.GetComponent<Structure>();

        //  여기서부터 확인
        // 건설 버튼을 눌렀을 때 && 현재 타일 위치에 건설되어있지 않을 때
        if (isOnFieldButton == true && structure.IsBuildStructure == false)
        {
            // 다시 건설 버튼을 눌러서 건설하도록 변수 설정
            isOnFieldButton = false;

            // 건물이 건설되어 있음으로 설정
            structure.IsBuildStructure = true;

            // 선택한 타일의 위치에 건축물 건설
            // Instantiate(StructurePrefab, tileTransform.position, Quaternion.identity);
            Vector3 position = tileTransform.position + Vector3.back;
            GameObject clone = Instantiate(fieldTemplate.fieldPrefab, position, Quaternion.identity);

            // 타워 무기에 enemySpawner, playerGold, tile 정보 전달 => 추후 구현

            // 배치했기 때문에 마우스를 따라다니는 임시 구조물 삭제
            Destroy(followFieldClone);
            // 건설을 취소할 수 있는 코루틴 함수 중지
            StopCoroutine("OnFieldCancelSystem");

            
        }


    }

    private IEnumerator OnFieldCancelSystem()
    {
        while(true)
        {
            // ESC키 또는 마우스 오른쪽 버튼을 눌렀을 때 타워 건설 취소
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnFieldButton = false;
                // 마우스를 따라다니는 임시 구조물 삭제
                Destroy(followFieldClone);
                break;
            }
            yield return null;
        }
    }

}
