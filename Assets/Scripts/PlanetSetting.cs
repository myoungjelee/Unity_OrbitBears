using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;


[CreateAssetMenu(fileName = "PlanetsSetting", menuName = "ScriptableObjects/PlanetsSetting", order = 1)]

public class PlanetSetting : ScriptableObject
{
    public PlanetData[] planetDatas;

}

