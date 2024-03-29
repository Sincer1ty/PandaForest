using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveManager : MonoBehaviour
{
    private float speed = 0.25f;
    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    private Camera mainCamera;
    public float zoomSpeed = 0.25f;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
            TouchMove();
        if (Input.touchCount == 2)
            Zoom();
    }

    void Zoom()
    {
        Debug.Log("Zoom");

        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

        mainCamera.orthographicSize += deltaMagDiff * zoomSpeed * Time.deltaTime;
        mainCamera.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.8f);
        mainCamera.orthographicSize = Mathf.Min(Camera.main.orthographicSize, 8f);
    }

    void TouchMove()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            prePos = touch.position - touch.deltaPosition;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            nowPos = touch.position - touch.deltaPosition;
            movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * speed;
            transform.Translate(movePos);
            prePos = touch.position - touch.deltaPosition;
        }
    }
}
