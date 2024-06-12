using UnityEngine;

public class CameraControllerTest : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;

    public float zoomOutSize = 9.0f; // 줌 아웃 시 카메라 크기
    private float originSize; // 원래 카메라 크기
    public float zoomSpeed = 3f; // 줌 인/아웃 속도

    void Start()
    {
        mainCamera = Camera.main; // 메인 카메라 참조
        originSize = mainCamera.orthographicSize; // 원래 카메라 크기 저장
    }

    void Update()
    {
        // 마우스 버튼이 눌렸을 때
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        // 마우스 버튼이 떼어졌을 때
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 드래그 상태일 때, 카메라를 서서히 줌 아웃
        if (isDragging)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoomOutSize, Time.deltaTime * zoomSpeed);
        }
        else
        {
            // 드래그 상태가 아닐 때, 카메라를 서서히 원래 크기로 복원
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originSize, Time.deltaTime * zoomSpeed);
        }
    }
}