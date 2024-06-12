using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // 행성의 리지드바디2D 컴포넌트

    public float launchForce = 0.01f;   // 마우스로 발사하는 힘의 배수

    public Vector2 landingSpot;         // 착륙점에 꼭 할당하기 (태그)

    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;     // 마우스 조작 코드

    private bool isGravityActive = false;  // 중력 활성화 여부

    private bool isLaunched = false;       // 발사 상태 확인
    private bool hasLanded = false;        // 행성의 착지상태 확인

    private LineRenderer lineRenderer;
    public int resolution = 20;             // 궤적의 해상도 (포인트 수)

    public ParticleSystem flyingTrailEffect;

    private Planet planet;

    void Start()
    {
        planet = GetComponent<Planet>();
        planetRigidbody = GetComponent<Rigidbody2D>();

        // LineRenderer 초기 설정
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 기본 쉐이더 사용
        lineRenderer.textureMode = LineTextureMode.Tile; // 텍스처를 타일 형태로 반복
        lineRenderer.widthMultiplier = 0.7f; // 선의 두께 설정

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();   // LineRenderer를 가져오는 구문
        }

        landingSpot = new Vector2(4, 0);

        flyingTrailEffect = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (!isLaunched) // 발사되지 않았을 때만 마우스 입력을 처리
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
            if (Input.GetMouseButton(0) && isDragging) // 마우스로 클릭 + 드래그를 하는 동안
            {
                dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dragVector = (dragStartPosition - dragEndPosition); // 클릭&드래그 하는 동안의 힘을 계산

                // 드래그 벡터의 크기 제한
                float maxDragDistance = 5.0f; // 최대 드래그 거리
                dragVector = Vector2.ClampMagnitude(dragVector, maxDragDistance);

                // 각도 제한
                float angleLimit = 90.0f; // 각도 제한
                float angle = Vector2.SignedAngle(Vector2.right, dragVector);
                if (Mathf.Abs(angle) > angleLimit)
                {
                    angle = Mathf.Sign(angle) * angleLimit;
                    dragVector = Quaternion.Euler(0, 0, angle) * Vector2.right * dragVector.magnitude;
                }

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
                Vector2 dragVector = (dragStartPosition - dragEndPosition); // 마우스 클릭ON 좌표 - 클릭OFF 좌표를 해서 힘 계산

                // 드래그 벡터의 크기 제한
                float maxDragDistance = 5.0f; // 최대 드래그 거리
                dragVector = Vector2.ClampMagnitude(dragVector, maxDragDistance);

                // 각도 제한
                float angleLimit = 90.0f; // 각도 제한
                float angle = Vector2.SignedAngle(Vector2.right, dragVector);
                if (Mathf.Abs(angle) > angleLimit)
                {
                    angle = Mathf.Sign(angle) * angleLimit;
                    dragVector = Quaternion.Euler(0, 0, angle) * Vector2.right * dragVector.magnitude;
                }

                Vector2 direction = dragVector.normalized;                  // 발사방향 계산
                float dragDistance = dragVector.magnitude;                  // 드래그 거리 계산

                planetRigidbody.velocity = direction * dragDistance * launchForce;

                isGravityActive = true;   // 마우스로 발사한 직후 중력 활성화
                isLaunched = true;
                if (!planet.isTouch)
                {
                    flyingTrailEffect.Play();
                }
                else
                {
                    flyingTrailEffect.Stop();
                }
            }
        }
    }

    void AttracToLandingSpot()
    {
        Vector2 direction = (Vector3)landingSpot - transform.position;     // 방향 계산
        float distance = direction.magnitude;                                        // landingspot까지의 거리 계산
        Vector2 gravityDirection = direction.normalized;                             // 중력의 방향

        float adjustedDistance = Mathf.Max(distance, 0.001f);                        // 최소 거리 값을 #로 설정하여 거리가 #보다 작아지지 않도록 함
        float gravityStrength = 0.01f / adjustedDistance;                               // 조정된 거리를 사용하여 중력 강도 계산 

        //planetRigidbody.velocity += gravityDirection * gravityStrength * Time.fixedDeltaTime;
        planetRigidbody.velocity += gravityDirection * 1f;

       // float clampedValue = Mathf.Clamp(gravityStrength,  )  // 클램프 : 최소값 최대값 제한
    }

    void FixedUpdate()
    {
        planetRigidbody.drag = 2f;           // 발사된 행성의 속도 저항력 설정

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