using System.Collections;
using System.Collections.Generic;
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
    public float radius;
    public Sprite nextSizeSprite;

    public bool isTouch;
    private bool isSpawn;
    private bool isMerging;

    public void SetData(PlanetData newData)
    {
        data = newData;
        GetComponent<SpriteRenderer>().sprite = data.sprite;
        GetComponent<Rigidbody2D>().mass = data.mass;
        transform.localScale = data.radius * 2f * Vector3.one;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GravityField" && isSpawn == false)
        {
            PlanetManager.Instance.ReloadingPlanet();
            isSpawn = true;
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "GravityField" && isTouch == true && !isMerging)
    //    {
    //        if(GameManager.Instance != null)
    //        {
    //            GameManager.Instance.GameOver();
    //        }
    //        else
    //        {
    //            Debug.Log("게임매니저가 null입니다.");
    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 파괴 상태를 확인
        if (gameObject == null) return;

        if (collision.gameObject.tag == "Planet")
        {
            Planet otherPlanet = collision.gameObject.GetComponent<Planet>();
            if (otherPlanet.data == data)
            {
                isMerging = true;
                ScoreManager.Instance.AddScore(data.mergeScore);
                SoundManager.Instance.PlayMergeSound();

                PlanetData nextPlanetData = PlanetManager.Instance.NextPlanetData(data.id);
                otherPlanet.SetData(nextPlanetData);
                otherPlanet.isTouch = true;
                GetComponent<CircleCollider2D>().enabled = false;
                ApplyForceToOther((transform.position + otherPlanet.transform.position) / 2, nextPlanetData);
                Destroy(gameObject);             
            }
        }
    }

    private void ApplyForceToOther(Vector2 center, PlanetData data)
    {
        float mergeForce = 15;
        var overlappingPlanets = Physics2D.OverlapCircleAll(center, data.radius);
        foreach (var planetCol in overlappingPlanets)
        {
            if (planetCol.gameObject == gameObject) continue;
            if (!planetCol.gameObject.CompareTag("Planet")) continue;

            Planet otherPlanet = planetCol.gameObject.GetComponent<Planet>();
            
            if (otherPlanet.data == this.data) continue;

            var planetRb = planetCol.GetComponent<Rigidbody2D>();

            var dir = (Vector2)otherPlanet.transform.position - center;

            var dist = dir.magnitude;
            dist -= data.radius + otherPlanet.data.radius;

            planetRb.AddForce(-dir.normalized * dist * Mathf.Sqrt(data.mass) * mergeForce, ForceMode2D.Impulse);
        }
    }

}