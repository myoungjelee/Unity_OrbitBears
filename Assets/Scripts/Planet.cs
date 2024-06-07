using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetData
{
    public string name;
    [HideInInspector] public uint id;
    public Sprite sprite;
    public Color color = Color.white;
    public float radius;
    public float mass;
    public int mergeScore;
    public float radiusData;
}

public class Planet : MonoBehaviour
{
    public PlanetData newData;

    public void SetData(PlanetData data)
    {
        newData = data;
    }
    
}
