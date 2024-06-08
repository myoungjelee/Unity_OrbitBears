using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public float radius;
    public Sprite nextSizeSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Planet otherPlanet = collision.gameObject.GetComponent<Planet>();

        if (otherPlanet != null && Mathf.Approximately(otherPlanet.radius, this.radius))
        {
            Vector3 collisionPoint = collision.contacts[0].point;
            Destroy(otherPlanet.gameObject);
            Destroy(this.gameObject);

            if (nextSizeSprite != null)
            {
                GameObject newPlanet = new GameObject("Planet");
                newPlanet.transform.position = collisionPoint;
                newPlanet.transform.localScale = Vector3.one * (this.radius * 2); // 다음 크기로 증가

                SpriteRenderer spriteRenderer = newPlanet.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = nextSizeSprite;

                Rigidbody2D rb = newPlanet.AddComponent<Rigidbody2D>();
                rb.AddForce(collision.relativeVelocity, ForceMode2D.Impulse);

                Planet planetComponent = newPlanet.AddComponent<Planet>();
                planetComponent.radius = this.radius * 2; // 다음 크기로 증가
                planetComponent.nextSizeSprite = this.nextSizeSprite; // 다음 크기의 스프라이트 설정
            }
        }
    }
}