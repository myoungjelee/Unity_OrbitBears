using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetManager", menuName = "ScriptableObjects/PlanetManager")]

public class PlanetSetting : ScriptableObject
{

    [SerializeField] private float massCalculate = 1f;
    [SerializeField] private PlanetData[] planetData;


    public void MassCalulateArray(PlanetData[] planetData)
    {
        for(int i = 0; i < planetData.Length; i++)
        {
            planetData[i].radiusData = planetData[i].radius * planetData[i].radius * Mathf.PI;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
