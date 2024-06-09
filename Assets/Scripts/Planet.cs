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

    private bool isTouch;
    private bool isSpawn;
    private bool isMerge;

    private void Start()
    {
    }

    public void SetData(PlanetData newData)
    {
        data = newData;
        GetComponent<SpriteRenderer>().sprite = data.sprite;
        GetComponent<Rigidbody2D>().mass = data.mass;
        transform.localScale = new Vector3(data.radius * 2f, data.radius * 2f, data.radius * 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GravityField" && isSpawn == false)
        {
            PlanetManager.Instance.ReloadingPlanet();
            isSpawn = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Planet")
        {
            isTouch = true;

            Planet otherPlanet = collision.gameObject.GetComponent<Planet>();
           // if (isMerge) return;
            if (otherPlanet.data == data)
            {
                //isMerge = true;
                //otherPlanet.isMerge = true;

                //Planet nextPlanet = PlanetManager.Instance.SpawnPlanet(PlanetManager.Instance.NextPlanetData(data.id + 1), collision.contacts[0].point);              
                //nextPlanet.isSpawn = true;
                //nextPlanet.isTouch = true;

                //Destroy(gameObject);
                //Destroy(otherPlanet.gameObject);
               // otherPlanet.isMerge = true;
                ScoreManager.Instance.AddScore(data.mergeScore);
                SoundManager.Instance.AddPlaySound();

                PlanetData nextPlanetData = PlanetManager.Instance.NextPlanetData(data.id + 1);
                otherPlanet.SetData(nextPlanetData);
                ApplyForceToOther((transform.position + otherPlanet.transform.position) / 2, nextPlanetData);
                Destroy(gameObject);             
            }
        }
    }

    private void ApplyForceToOther(Vector2 center, PlanetData data)
    {
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

            planetRb.AddForce(-dir.normalized * dist * Mathf.Sqrt(data.mass) * 15, ForceMode2D.Impulse);
        }
    }

}