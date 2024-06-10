using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera; // 메인 카메라
    public float zoomOutSize = 7f; // 줌아웃 시 카메라 크기
    public float zoomInSize = 5f; // 줌인(원래) 시 카메라 크기
    public float zoomSpeed = 2f; // 줌 전환 속도

    private bool isDragging = false; // 드래그 상태 확인
    private bool isZoomedOut = false; // 줌아웃 상태 확인
    private float targetZoomSize; // 목표 줌 크기

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        targetZoomSize = mainCamera.orthographicSize;
    }

    void Update()
    {
        // 마우스 클릭 드래그 상태 확인
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            StartZoomOut();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            StartZoomIn();
        }

        // 카메라 줌 전환
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoomSize, Time.deltaTime * zoomSpeed);
    }

    void StartZoomOut()
    {
        if (!isZoomedOut)
        {
            targetZoomSize = zoomOutSize;
            isZoomedOut = true;
        }
    }

    void StartZoomIn()
    {
        if (isZoomedOut)
        {
            targetZoomSize = zoomInSize;
            isZoomedOut = false;
        }
    }
}