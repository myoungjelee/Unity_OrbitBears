using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // 행성의 리지드바디2D 컴포넌트

    // [SerializeField] Rigidbody2D planetRigidbody; 상단 콘솔 이런식으로도 사용가능함
    public float forceMultiplier = 80f;   // 힘의 배수

    public GameObject planet;
    public GameObject landingSpot; // 착륙점에 꼭 할당하기 (태그)

    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;   // 마우스 조작 코드


    private bool isGravityActive = false; // 중력 활성화 여부
                                          // private bool isInFlight = false;  // 공이 날아가고 있는 상태인지 확인

    private bool isLaunched = false; // 발사 상태 확인
    private bool hasLanded = false;   // 공이 착지했는지 확인


    //private bool isDecelerating = false;

    void Start()
    {
        planetRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // 마우스 클릭 할때
        if (Input.GetMouseButtonDown(0))

        {
            // 드래그 시작 위치 기록
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))          //여기에 있으면 void Start()아래의 함수기 때문에 게임 시작시엔
                                                // 마우스를 누르지 않은 상태이므로 여기 if문이 바로 실행됨 상시로
                                                // 사용할 커맨드 이기에 Update로 옮겼음
        {
            dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;

            Vector2 dragVector = (dragStartPosition - dragEndPosition); // 마우스 클릭ON 좌표 - 클릭OFF 좌표 를 해서 힘계산
            Vector2 direction = dragVector.normalized;  // 발사방향 계산
            float dragDistance = dragVector.magnitude;  // 드래그 거리 계산

            planetRigidbody.AddForce(direction * dragDistance * forceMultiplier, ForceMode2D.Impulse);

            isGravityActive = true;
            isLaunched = true;
        }
        if (isLaunched)
        {
            AttracToLandingSpot();
        }
    }
    void AttracToLandingSpot()
    {
        Vector2 direction = landingSpot.transform.position - transform.position;     // 방향 계산
        float distance = direction.magnitude;                                        // landingspot까지의 거리 계산
        Vector2 gravityDirection = direction.normalized;                             // 중력의 방향

        float adjustedDistance = Mathf.Max(distance, 0.1f);                          // 최소 거리 값을 0.5로 설정하여 거리가 0.5보다 작아지지 않도록 함
        float gravityStrength = 10 / adjustedDistance;                               // 조정된 거리를 사용하여 중력 강도 계산
        //float gravityStrength = Mathf.Clamp(10 / distance, 0.1f, 10);              // 거리에 따른 중력 강도 조절, 최소값과 최대값 설정
    
      /*  if (distance < 1f)  // 착륙점 (landingSpot)에 매우 가까워졌을때
        {
            planetRigidbody.velocity = Vector3.ClampMagnitude(planetRigidbody.velocity, 50f); // 속도를 최대 #f로 제한
            gravityStrength = Mathf.Lerp(gravityStrength, 0, 1 - distance);
            planetRigidbody.velocity *= 0.01f; 
        }*/
        planetRigidbody.AddForce(gravityDirection * gravityStrength);                // 조절된 중력 적용
    }
    //public float maxSpeed = 100f;                                                    // 최대 속도 설정
    void FixedUpdate()
    {
        planetRigidbody.drag = 1.5f;         // 저항력 설정  1.6f 황밸
    }

    public float surfaceFriction = 0.01f;     // 표면 마찰력
    public float spinFriction = 5f;        // 회전 마찰력

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == landingSpot)
        {
            // 충돌 후 마찰력 적용
            planetRigidbody.drag = surfaceFriction;
            planetRigidbody.angularDrag = spinFriction;
        }
    }
    /*    public float maxSpeed = 100f;                                                    // 최대 속도 설정
        void FixedUpdate()
        {
                                         if (planetRigidbody.velocity.magnitude > maxSpeed)
                                         {
                                         현재 속도 방향을 유지하면서 속도의 크기를 maxSpeed로 조정
                                         planetRigidbody.velocity = planetRigidbody.velocity.normalized * maxSpeed;
                                         }
        }*/
}
