using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;


public class PlanetManager : MonoBehaviour
{
    private static PlanetManager instance;
    public GameObject planetPrefab;
    public PlanetSetting planetSetting;

    public static PlanetManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PlanetManager>();
            }
            return instance;
        }
    }

    private Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = new Vector3(-8, 0, 0);

        SpawnPlanet(RandomData(), spawnPosition);
    }
    


    public PlanetData GetPlanetData(int id)
    {
        id = Random.Range(0, 4);
        return planetSetting.planetDatas[id];
    }

    public PlanetData RandomData()
    {
        return GetPlanetData(Random.Range(0, 4));
    }

    public Planet SpawnPlanet(PlanetData data, Vector2 spawnPos)
    {
       Planet planet = Instantiate(planetPrefab, spawnPos, Quaternion.identity).GetComponent<Planet>();
        planet.SetData(data);

        return planet;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPlanet(RandomData(), spawnPosition);
        }
    }
    

}
