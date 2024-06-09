using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // 행성의 리지드바디2D 컴포넌트
    // [SerializeField] Rigidbody2D planetRigidbody; 상단 콘솔 이런식으로도 사용가능함

    public float launchForce = 7.5f;   // 마우스로 발사하는 힘의 배수

    public GameObject planet;
    public Vector2 landingSpot;         // 착륙점에 꼭 할당하기 (태그)


    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;     // 마우스 조작 코드

    private bool isGravityActive = false;  // 중력 활성화 여부

    private bool isLaunched = false;       // 발사 상태 확인
    private bool hasLanded = false;        // 행성의 착지상태 확인
    //////////////////////////인디케이터///////////////////////////

    private LineRenderer lineRenderer;
    public int resolution = 30;             // 궤적의 해상도 (포인트 수)

    //////////////////////////인디케이터///////////////////////////
    void Start()
    {
        planetRigidbody = GetComponent<Rigidbody2D>();

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();   // LineRenderer를 가져오는 구문
        }

        landingSpot = new Vector2(4, 0);
    }   

    void Update()
    {
        if (!isLaunched) // 발사되지 않았을 때만 마우스 입력을 처리 = 발사된 이후엔 마우스 입력영향을 받지않음
        {
            if (Input.GetMouseButtonDown(0)) // 마우스 버튼 눌렀을 때
            {
                // 드래그 시작 위치 기록
                dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
                /////인디케이터 명령어/////
                dragStartPosition = planetRigidbody.position;
                ///////////////////////////
            }
            if (Input.GetMouseButton(0) && isDragging) // 마우스로 클릭 + 드래그를 하는동안 ~
            {
                dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dragVector = (dragStartPosition - dragEndPosition); // 클릭&드래그 하는동안의 힘을 계산
                Vector2 direction = dragVector.normalized;                  // 발사방향 계산
                float dragDistance = dragVector.magnitude;                  // 드래그 거리 계산 (드래그 정도)

                ShowTrajectory(dragStartPosition, direction * dragDistance * launchForce);
            }
            if (Input.GetMouseButtonUp(0))   // 마우스 버튼 뗐을 때
            {
                dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isDragging = false;
                //////인디케이터 명령어/////
                ClearTrajectory();  // 궤적(표시기) 지우기
                ////////////////////////////
                Vector2 dragVector = (dragStartPosition - dragEndPosition); // 마우스 클릭ON 좌표 - 클릭OFF 좌표 를 해서 힘계산
                Vector2 direction = dragVector.normalized;                  // 발사방향 계산
                float dragDistance = dragVector.magnitude;                  // 드래그 거리 계산

                planetRigidbody.AddForce(direction * dragDistance * launchForce, ForceMode2D.Impulse);

                isGravityActive = true;   // 마우스로 발사한 직후 중력 활성화
                isLaunched = true;
            }
        }

    }

    void AttracToLandingSpot()
    {
        Vector2 direction = (Vector3)landingSpot - transform.position;     // 방향 계산
        float distance = direction.magnitude;                                        // landingspot까지의 거리 계산
        Vector2 gravityDirection = direction.normalized;                             // 중력의 방향

        float adjustedDistance = Mathf.Max(distance, 0.001f);                        // 최소 거리 값을 #로 설정하여 거리가 #보다 작아지지 않도록 함
        float gravityStrength = 10 / adjustedDistance;                               // 조정된 거리를 사용하여 중력 강도 계산 
        /*  if (distance < 1f)  // 착륙점 (landingSpot)에 매우 가까워졌을때
          {
              planetRigidbody.velocity = Vector3.ClampMagnitude(planetRigidbody.velocity, 50f); // 속도를 최대 #f로 제한
              gravityStrength = Mathf.Lerp(gravityStrength, 0, 1 - distance);
              planetRigidbody.velocity *= 0.01f; 
          }*/
       // planetRigidbody.AddForce(gravityDirection * gravityStrength);                // 조절된 중력 적용
        planetRigidbody.velocity += gravityDirection * 0.7f;
    }
    //public float maxSpeed = 100f;                                                  // 최대 속도 설정
    void FixedUpdate()
    {
        planetRigidbody.drag = 1.5f;           // 발사된 행성의 속도 저항력 설정

        if (isLaunched)
        {
            AttracToLandingSpot();
        }
    }

    void ShowTrajectory(Vector2 startPosition, Vector2 startVelocity)
    {
        resolution = 3; // 궤적의 해상도를 낮춰서 궤적의 길이를 줄임

        Vector3[] points = new Vector3[resolution];
        float timeStep = 0.1f;   // 시간 간격

        // 드래그 방향에 따른 궤적 위치 조정
        float trajectoryOffset = startVelocity.y > 0 ? -2f : 2f; // 위로 드래그하면 궤적을 아래로, 아래로 드래그하면 궤적을 위로 조정

        for (int i = 0; i < resolution; i++)
        {
            float time = i * timeStep;
            Vector2 point = startPosition + startVelocity * time + 0.5f * Physics2D.gravity * time * time; // 점 계산

            points[i] = new Vector3(point.x, point.y, 0); // 2D 점을 3D로 변환
       
        }

        lineRenderer.positionCount = resolution;
        lineRenderer.SetPositions(points); // LineRenderer에 점 설정

    }
    void ClearTrajectory()
    {
        lineRenderer.positionCount = 0; // 궤적 지우기
    }
}
