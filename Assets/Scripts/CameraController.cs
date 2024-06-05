using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;

    private float targetSize;
    private float originSize;

    private bool isZoomOut = false; // 전환 중인지 여부를 나타내는 플래그
    private float zoomingTime = 1.2f; // 전환 기간

    private float zoomInTime = 0f; // 전환 시간을 계산하기 위한 타이머 변수
    private float zoomuOutTime = 0f; // 전환 시간을 계산하기 위한 타이머 변수

    private void Start()
    {
        mainCamera = Camera.main;

        originSize = mainCamera.orthographicSize;
        targetSize = 12.0f;

        //StartCoroutine(test());
    }

    //private void LateUpdate()
    //{
    //    if (isZoomOut)
    //    {
    //        // 전환 시간이 기간보다 작으면
    //        if (currentTime < zoomTime)
    //        {
    //            // 전환 타이머를 증가시키고, 보간된 크기를 적용합니다.
    //            currentTime += Time.deltaTime;
    //            float t = Mathf.Clamp01(currentTime / zoomTime); // 0과 1 사이로 정규화
    //            mainCamera.orthographicSize = Mathf.Lerp(originSize, targetSize, t);
    //        }
    //        else
    //        {
    //            // 전환 시간이 기간을 초과하면 전환 종료
    //            isZoomOut = false;
    //            currentTime += Time.deltaTime;
    //            float t = Mathf.Clamp01(currentTime / zoomTime); // 0과 1 사이로 정규화
    //            mainCamera.orthographicSize = Mathf.Lerp(targetSize, originSize, t);
    //        }
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //mainCamera.orthographicSize = Mathf.Lerp(originSize, targetSize, 0.5f);
    //    isZoomOut = true;
    //    currentTime = 0f;
    //}

    //IEnumerator test()
    //{
    //    yield return new WaitForSeconds(3);

    //    isZoomOut = true;
    //}
}
