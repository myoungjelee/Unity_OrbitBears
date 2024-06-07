using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public GameObject shooter;
    public GameObject[] planetPrefabs;

    public PlanetManager planetManager;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Instantiate(planetPrefabs[Random.RandomRange(0,3)], shooter.transform);
    }

   
  
}
