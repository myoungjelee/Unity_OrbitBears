using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;


[CreateAssetMenu(fileName = "PlanetsData", menuName = "ScriptableObjects/PlanetsData", order = 1)]

public class PlanetManager : ScriptableObject
{
    public PlanetData[] planets;

    public void SpawnPlanet(Transform spawnPoint, float launchForce)
    {
        int index = Random.Range(0, planets.Length);
        PlanetData planetData = planets[index];

        GameObject planet = new GameObject(planetData.name);
        planet.transform.position = spawnPoint.position;
        planet.transform.localScale = Vector3.one * planetData.radius;

        SpriteRenderer spriteRenderer = planet.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = planetData.sprite;
        spriteRenderer.color = planetData.color;

        Rigidbody2D rb = planet.AddComponent<Rigidbody2D>();
        rb.AddForce(spawnPoint.up * launchForce, ForceMode2D.Impulse);

        Planet planetComponent = planet.AddComponent<Planet>();
        planetComponent.radius = planetData.radius;
        planetComponent.nextSizeSprite = planetData.nextSizeSprite;
    }   
=======
using UnityEngine;

public class PlanetManager : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
>>>>>>> dev
}
