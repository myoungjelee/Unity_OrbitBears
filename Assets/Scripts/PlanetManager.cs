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

    private void Start()
    {
        currentPlanetData = GetRandomPlanetData();
        nextPlanetData = GetRandomPlanetData();
        ReloadingPlanet();
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

    public PlanetData NextPlanetData(int currentData)
    {
        return planetSetting.planetDatas[currentData + 1];
    } 
}
