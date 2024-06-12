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
    public bool isMerging;

    private GravityField gravityField;

    private bool isInGravityField = false;

    private void Start()
    {
        gravityField = PlanetManager.Instance.gravityField;
    }

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
    //    if (collision.gameObject.tag == "GravityField" && isTouch == true && isMerging == false)
    //    {
    //        if (GameManager.Instance != null)
    //        {
    //            GameManager.Instance.GameOver();
    //        }
    //        else
    //        {
    //            Debug.Log("게임매니저 Null!!");
    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            isTouch = true;
            Planet otherPlanet = collision.gameObject.GetComponent<Planet>();
           
            if (otherPlanet.data == data)
            {
                isMerging = true;
                otherPlanet.isMerging = true;

                ScoreManager.Instance.AddScore(data.mergeScore);
                SoundManager.Instance.PlayMergeSound();

                PlanetData nextPlanetData = PlanetManager.Instance.NextPlanetData(data.id);
                otherPlanet.transform.position = (transform.position + otherPlanet.transform.position) / 2;
                otherPlanet.SetData(nextPlanetData);
                otherPlanet.PlanetInitialization();           

                otherPlanet.ApplyForceToOther(transform.position, nextPlanetData);

                Destroy(gameObject);
            }
        }
    }

    private void PlanetInitialization()
    {
        isTouch = true;
        isSpawn = true;
        isMerging = false;
    }
        
    private void ApplyForceToOther(Vector2 center, PlanetData data)
    {
        float mergeForce = 50f;
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

    public PlanetData GetData()
    {
        return data;
    }

    private void Update()
    {
        if (isTouch && isInGravityField)
        {
            gravityField.SetDistanceFromCenter(this);
        }
    }

    private void FixedUpdate()
    {
        isInGravityField = gravityField.IsInField(this);


        if (isTouch && !isMerging && !isInGravityField)
        {
            GameManager.Instance.GameOver();
        }
    }
}