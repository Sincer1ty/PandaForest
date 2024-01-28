using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private FacilitySpawner facilitySpawner;

    private Camera      mainCamera;
    private Ray         ray;
    private RaycastHit  hit;
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Tile"))
                {
                    facilitySpawner.SpawnFacility(hit.transform);
                }
            }
        }
    }
}
