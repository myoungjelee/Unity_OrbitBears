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
        SpawnPlanet();
    }

    public void SpawnPlanet()
    {
        if (planetManager != null)
        {
            planetManager.SpawnPlanet(spawnPoint, launchForce);
        }
    }
}
