using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // 행성의 리지드바디2D 컴포넌트
   
    // [SerializeField] Rigidbody2D planetRigidbody; 상단 콘솔 이런식으로도 사용가능함
    public float forceMultiplier = 2f;   // 힘의 배수

    public GameObject planet;
    public GameObject landingSpot; // 착륙점에 꼭 할당하기 (태그)

    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;

    public Transform gravityCenter;

    public float gravityStrength = 30f;
    public float decelerationRate = 10f; // 속도 감소율 (0.95는 매 프레임마다 5%씩 속도를 줄임)
    public float landingRadius = 1f;

    private bool isGravityActive = false; // 중력 활성화 여부
                                          // private bool isInFlight = false;  // 공이 날아가고 있는 상태인지 확인
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
        ///////////////////////////////////////
        // 마우스 클릭 이벤트 처리
        if (Input.GetMouseButtonDown(0) && isGravityActive && !hasLanded)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D collider = Physics2D.OverlapPoint(mousePos);
        }
        ///////////////////////////////////////
        if (Input.GetMouseButtonUp(0))          // 여기에 있으면 void Start()아래의 함수기 때문에 게임 시작시엔
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
        }



    }
    void FixedUpdate()
    {
        /*if (isGravityActive )   // 중력이 활성화 되었을때 하단 명령실행
        {
            // 중력점과 행성사이의 벡터값을 계산
            Vector2 directionToCenter = (Vector2)gravityCenter.position - planetRigidbody.position;
            float distanceToCenter = directionToCenter.magnitude;
            // 중력 벡터값 계산
            float gravityFroceMagnitude = gravityStrength / (distanceToCenter * distanceToCenter);
            Vector2 gravityForce = directionToCenter.normalized * gravityFroceMagnitude;

            // Calculate the gravity vector
            //Vector2 gravityForce = directionToCenter.normalized * gravityForceMagnitude;

            // Calculate the gravity force magnitude (using the inverse square law)
            float gravityForceMagnitude = gravityStrength / (distanceToCenter * distanceToCenter);

            

            // Apply the gravity force to the ball
            planetRigidbody.AddForce(gravityForce);

            // 행성이 착륙지점 근처에 착지했는지 확인
            if (distanceToCenter <= landingRadius)
            {
                // 행성이 중력지점에 착륙했을때 위치와 속도 0으로 설정
                planetRigidbody.velocity = Vector2.zero;
                planetRigidbody.angularVelocity = 0.0f;
                planetRigidbody.position = gravityCenter.position;

                isGravityActive = false; // 중력 비활성화
                hasLanded = true;       
            }
        }*/
        /* if (isGravityActive && !hasLanded)
        {
            Vector2 directionToCenter = (Vector2)gravityCenter.position - planetRigidbody.position;
            float distanceToCenter = directionToCenter.magnitude;

            if (distanceToCenter > landingRadius)
             {
                 // 일정한 크기의 중력 벡터를 계산합니다.
                 Vector2 gravityForce = directionToCenter.normalized * gravityStrength;

                 // 계산된 중력 벡터를 공에 적용합니다.
                 planetRigidbody.AddForce(gravityForce);
             }
             else
             {
                 void OnCollisionEnter2D(Collision2D collision)
                 {
                     // 충돌한 오브젝트의 태그가 "LandingSpot"인지 확인합니다.
                     if (collision.gameObject.CompareTag("LandingSpot"))
                     {
                         // 속도를 Vector2.zero로 설정하여 오브젝트의 속도를 0으로 만듭니다.
                         planetRigidbody.velocity = Vector2.zero;
                     }
                 }
             }
        }
        else if (isDecelerating)
        {
            // 속도를 점진적으로 줄임
            planetRigidbody.velocity *= decelerationRate;

            // 어느 정도 속도가 줄어들면 완전히 멈춤
            if (planetRigidbody.velocity.magnitude < 0.1f)
            {
                planetRigidbody.velocity = Vector2.zero;
                isDecelerating = false;
            }
        }*/
        /*           if (isGravityActive)
               {
                   Vector2 directionToLandingSpot = (landingSpot.transform.position - transform.position).normalized;
               float distance = Vector2.Distance(transform.position, landingSpot.transform.position);
                   // 속도 점진적으로 감소
                   planetRigidbody.velocity *= decelerationRate;
                   print("a");
               }*/
        if (isGravityActive)
        {
            Vector2 directionToLandingSpot = (landingSpot.transform.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, landingSpot.transform.position);

            // 거리에 반비례하는 중력을 적용
            float gravityForce = gravityStrength / distance; // 거리가 줄어들수록 중력 감소
            planetRigidbody.AddForce(directionToLandingSpot * gravityForce);

            // 속도 감소 로직 추가
            if (planetRigidbody.velocity.magnitude > 1f) // 속도가 너무 낮아지면 멈추도록 함
            {
                planetRigidbody.velocity *= decelerationRate;
            }
        }
    }

/*    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == landingSpot)
        {
            // LandingSpot에 닿으면 속도를 0으로 설정하고 달라붙음
            planetRigidbody.velocity = Vector2.zero;
            planetRigidbody.isKinematic = true;
            transform.position = collision.contacts[0].point;
            isGravityActive = false;
        }
    }*/
}
