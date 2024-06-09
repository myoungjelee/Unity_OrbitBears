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

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}