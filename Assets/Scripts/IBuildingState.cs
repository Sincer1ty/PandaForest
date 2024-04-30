using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 건물 상태 나타내는 메서드 선언
public interface IBuildingState 
{
    void EndState(); // 현재 상태 종료
    void OnAction(Vector3Int gridPosition); // 특정 그리드 위치에서 동작 수행
    void UpdateState(Vector3Int gridPosition); // 상태 업데이트
    
}
