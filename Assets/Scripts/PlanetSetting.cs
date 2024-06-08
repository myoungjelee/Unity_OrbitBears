using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetSetting : MonoBehaviour
{
    public PlanetManager planetManager;
    public Transform spawnPoint;
    public float launchForce = 10f;

    private void Start()
    {

        if (planetManager == null)
        {
            // Resources 폴더에서 PlanetManager ScriptableObject를 로드
            planetManager = Resources.Load<PlanetManager>("PlanetManager");
        }
        SpawnPlanet();
    }

    public void SpawnPlanet()
    {
        if (planetManager != null)
        {
            planetManager.SpawnPlanet(spawnPoint, launchForce);
        }
        else
        {
            Debug.LogError("PlanetManager is not assigned or could not be found.");
        }
        
    }
}
