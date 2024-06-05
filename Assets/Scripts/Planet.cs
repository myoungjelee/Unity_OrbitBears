using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData
{
    public string name;
    public uint id;
    public Sprite sprite;
    public Color color = Color.white;
    public float radius;
    public float mass;
    public int mergeScore;
}

public class Planet : MonoBehaviour
{
    public float size; //행성 크기
    public float speed; //날아갈 때 행성 속도
    public GameObject mergedPlanetPrefab;
    private Rigidbody2D rigidbody; //rigidbody라고 선언

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); 
        
    }

    void OnCollisionEnter2D (Collision2D collision) //2개의 collision 2D가 충돌 시 호출. 물리적 충돌 감지 함수
    {
        Planet otherPlanet = collision.gameObject.GetComponent<Planet>();   //프리팹에 planet 오브젝트 넣어줌으로 재사용 가능
        if (otherPlanet != null && otherPlanet.size == size)    //만약에 충돌한 객체가 같은 크기의 행성이라면 merge() 함수로 합체
        {
            Merge(otherPlanet);
        }
    }

    void Merge(Planet otherPlanet)  //충돌 후 새로 생성될 행성 위치를 현재 위치로 설정
    {
        Vector3 mergedPosition = (transform.position + otherPlanet.transform.position) / 2;     //충돌한 두 행성의 가운데 지점에 행성이 놓임

        float mergedSize = size + otherPlanet.size;   //두 행성 size의 합으로 합쳐진 행성 크기 도출

        GameObject mergedPlanet = Instantiate(mergedPlanetPrefab, mergedPosition, Quaternion.identity); //합쳐진 행성 생성
        mergedPlanet.GetComponent<Planet>().size = mergedSize;


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
