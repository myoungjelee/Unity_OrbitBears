/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // 행성의 리지드바디2D 컴포넌트
    // [SerializeField] Rigidbody2D planetRigidbody;
    public float forceMultiplier = 5f;   // 힘의 배수

    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;

    void Start()
    {

    }
    void Update()
    {
        // 마우스 클릭 할때
        if (Input.GetMouseButtonDown(0))  //(0) == 좌클릭
                                          //(1) == 우클릭
        {
            // 드래그 시작 위치 기록
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))  // 여기에 있으면 void Start()아래의 함수기 때문에 게임 시작시엔
                                          // 마우스를 누르지 않은 상태이므로 여기 if문이 바로 실행됨 상시로
                                          // 사용할 커맨드 이기에 Update로 옮겼음
        {
            dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;
            
            Vector2 dragVector = dragStartPosition - dragEndPosition; // 마우스 클릭ON 좌표 - 클릭OFF 좌표 를 해서 힘계산
            Vector2 direction = dragVector.normalized;  // 발사방향 계산
            float dragDistance = dragVector.magnitude;  // 드래그 거리 계산

            planetRigidbody.AddForce(direction * dragDistance * forceMultiplier, ForceMode2D.Impulse);

        }
    }
}
*/