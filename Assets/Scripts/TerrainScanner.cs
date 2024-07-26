using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph;
using UnityEngine;

public class TerrainScanner : MonoBehaviour
{
    [SerializeField]
    private GameObject TerrainScannerPrefab;

    [SerializeField]
    private float duration = 10;

    [SerializeField]
    private float size = 500;

    public void SpawnTerrainScanner()
    {
        GameObject terrainScanner = Instantiate(TerrainScannerPrefab, Camera.main.transform.position, Quaternion.identity) as GameObject;
        ParticleSystem terrainScannerPS = terrainScanner.transform.GetChild(0).GetComponent<ParticleSystem>();

        if(terrainScannerPS != null )
        {
            var main = terrainScannerPS.main;
            main.startLifetime = duration;
            main.startSize = size;
        }
        else
        {
            Debug.Log("The first child doesn't have a particle system");
        }

        Destroy(terrainScanner, duration+1);
    }
}
