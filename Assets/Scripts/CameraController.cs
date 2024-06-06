using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.TimeZoneInfo;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;

    private float expendSize;
    private float originSize;

    private float zoomDurationTime = 1.2f; // 전환 시간

    private void Start()
    {
        mainCamera = Camera.main;

        originSize = mainCamera.orthographicSize;
        expendSize = 12.0f;

        StartCoroutine(test());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            // 카메라를 줌 아웃
            CameraZoomOut();

            // 지정된 지속 시간 이후에 줌 인 메소드 호출
            Invoke("CameraZoomIn", zoomDurationTime);
        }
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(zoomDurationTime);

        // 카메라를 줌 아웃
        CameraZoomOut();

        // 지정된 지속 시간 이후에 줌 인 메소드 호출
        Invoke("CameraZoomIn", zoomDurationTime);
    }

    private void CameraZoomOut()
    {
        StartCoroutine(CameraControl(expendSize));
    }

    private void CameraZoomIn()
    {
        StartCoroutine(CameraControl(originSize));
    }

    // 카메라의 orthographic 크기를 부드럽게 변경하는 코루틴
    IEnumerator CameraControl(float targetSize)
    {
        float currentTime = 0;   // 현재시간 초기화                     
        float currentSize = mainCamera.orthographicSize;  // 현재 orthographicSize 저장

        // 지정된 시간 동안 시작 크기와 목표 크기 사이를 보간
        while (currentTime < zoomDurationTime)
        {
            mainCamera.orthographicSize = Mathf.Lerp(currentSize, targetSize, currentTime / zoomDurationTime);

            // 경과 시간 증가
            currentTime += Time.deltaTime;

            // 다음 프레임까지 대기
            yield return null;
        }
    }
}
