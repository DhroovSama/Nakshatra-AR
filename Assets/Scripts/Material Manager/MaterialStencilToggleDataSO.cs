using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MaterialStencilToggleData", menuName = "Tools/Material Stencil Toggle Data")]
public class MaterialStencilToggleDataSO : ScriptableObject
{
    public Material[] LanderBody = new Material[0];
    public Material[] LanderDoor = new Material[0];
    public Material[] RoverBody = new Material[0];
    public Material[] RoverSolarPanels = new Material[0];
    public Material[] Wheels = new Material[0];
    public Material[] Terrain = new Material[0];
    public Material[] NightSky = new Material[0];
    public Material[] AsteroidsOnTerrain = new Material[0];

    public Material[] GetAllMaterials()
    {
        List<Material> allMaterials = new List<Material>();
        allMaterials.AddRange(LanderBody);
        allMaterials.AddRange(LanderDoor);
        allMaterials.AddRange(RoverBody);
        allMaterials.AddRange(RoverSolarPanels);
        allMaterials.AddRange(Wheels);
        allMaterials.AddRange(Terrain);
        allMaterials.AddRange(NightSky);
        allMaterials.AddRange(AsteroidsOnTerrain);

        return allMaterials.ToArray();
    }
}
