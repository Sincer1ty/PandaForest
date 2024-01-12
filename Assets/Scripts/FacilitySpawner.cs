using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilitySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject facilityPrefab;

    public void SpawnFacility(Transform tileTransform)
    {
        Instantiate(facilityPrefab, tileTransform.position, Quaternion.identity);
    }
}
