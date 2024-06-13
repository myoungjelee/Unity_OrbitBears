using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlanetData
{
    public string name;
    public int id;
    public Sprite sprite;
    public Color color = Color.white;
    public float radius;
    public float mass;
    public int mergeScore;
    public Sprite nextSizeSprite;
}

public class Planet : MonoBehaviour
{
    private PlanetData data;
    private HashSet<GameObject> contactedObjects = new HashSet<GameObject>(); //중복을 허용하지 않는

    public bool outGravityField = false;
    public bool isMerge = false;
    public bool touchPlanet = false;


    public void SetData(PlanetData newData)
    {
        data = newData;
        if (newData.sprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = data.sprite;

        }
        else
        {
            GetComponent<Rigidbody2D>().mass = data.mass;
        }
        transform.localScale = new Vector3(data.radius * 2.5f, data.radius * 2.5f, data.radius * 2.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            Planet otherPlanet = collision.gameObject.GetComponent<Planet>();

            touchPlanet = true;

            if (otherPlanet.data == data)   //
            {
                PlanetData nextPlanetData = PlanetManager.Instance.NextPlanetIndex(data.id);
                otherPlanet.SetData(nextPlanetData);
                ScoreManager.Instance.AddScore(data.mergeScore);
                SoundManager.Instance.PlayMergeSound();

                isMerge = true;
                otherPlanet.isMerge = true;         //합쳐지는 두 행성 상태 변환

                Destroy(gameObject);
                return;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // GravityField에 접촉 감지 확인
        if (collision.gameObject.CompareTag("GravityField"))      //행성이 들어옴
        {
            // 접촉된 물체가 이미 있는지 확인
            if (!contactedObjects.Contains(collision.gameObject))   //HashSet
            {
                // 접촉된 물체 추가
                contactedObjects.Add(collision.gameObject);

                // 특정 함수 호출
                PlanetManager.Instance.AfterShootPlanet();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //행성이 나감을 감지
    {
        if (collision.gameObject.CompareTag("GravityField"))
        {
            Debug.Log("Exit");
            outGravityField = true;
            CheckGameOver();
        }
    }

    private void CheckGameOver()
    {
        Debug.Log("gameOver");
        // 행성이 합쳐지지 않았고, 중력장을 나갔을 때 게임 오버
        if (!isMerge && outGravityField && touchPlanet)
        {
            GameManager.Instance.GameOver();
        }
    }

}