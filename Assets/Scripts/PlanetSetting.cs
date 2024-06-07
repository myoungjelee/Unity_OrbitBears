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
            planetData[i].radius = planetData[i].radius * planetData[i].radius * Mathf.PI;
        }
    }
    
    public void SetPlanetId()
    {
        for(int i = 0; i < planetData.Length; i++)
        {
            planetData[i].id = i;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPlanetId();
    }

    //private void OnEnable()
    //{
    //    SetPlanetId();
    //}

    public PlanetData GetPlanetData(int id)
    {
        id %= planetData.Length;
        return planetData[id];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
