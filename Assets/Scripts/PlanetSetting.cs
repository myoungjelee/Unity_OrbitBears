using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
=======
<<<<<<< HEAD

>>>>>>> parent of 78f00ea (Revert "데이터 생성 완료, 충돌 시 처리 구현")
public class PlanetSetting : MonoBehaviour
{
    public PlanetManager planetManager;
    public Transform spawnPoint;
    public float launchForce = 10f;

    private void Start()
    {

        if (planetManager == null)
        {
            // Resources �������� PlanetManager ScriptableObject�� �ε�
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
<<<<<<< HEAD
       
=======
        else
        {
            Debug.LogError("PlanetManager is not assigned or could not be found.");
        }
=======
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
>>>>>>> dev
        
>>>>>>> parent of 78f00ea (Revert "데이터 생성 완료, 충돌 시 처리 구현")
    }
}
