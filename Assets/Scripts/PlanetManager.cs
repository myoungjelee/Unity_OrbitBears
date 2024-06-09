using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.UI;



public class PlanetManager : MonoBehaviour
{
    private static PlanetManager instance;
    public GameObject planetPrefab;
    public PlanetSetting planetSetting;
    public Image nextPlanetImage;
    public Transform planetSpawnPoint;

    private PlanetData currentPlanetData;
    private PlanetData nextPlanetData;

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

        currentPlanetData = GetRandomPlanetData();
        nextPlanetData = GetRandomPlanetData();
        ReloadingPlanet();
    }  

    public PlanetData RandomData()
    {
        return GetRandomPlanetData();
    }

    public Planet SpawnPlanet(PlanetData data, Vector2 spawnPos)
    {
       Planet planet = Instantiate(planetPrefab, spawnPos, Quaternion.identity).GetComponent<Planet>();
        planet.SetData(data);

        return planet;
    }

    public PlanetData GetRandomPlanetData()
    {
        int id = Random.Range(0, 4);
        return planetSetting.planetDatas[id];
    }

    public void ReloadingPlanet()
    {
        currentPlanetData = nextPlanetData;
        nextPlanetData = GetRandomPlanetData();

        if(nextPlanetData.sprite != null)
        {
            nextPlanetImage.sprite = nextPlanetData.sprite;
            nextPlanetImage.rectTransform.sizeDelta = 75 * nextPlanetData.radius * Vector2.one;
        }

        SpawnPlanet(currentPlanetData, planetSpawnPoint.position);
    }

    public PlanetData NextPlanetData(int id)
    {
        return planetSetting.planetDatas[id];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ReloadingPlanet();
        }
    }  
}
