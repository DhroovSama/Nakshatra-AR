using UnityEngine;

public class TerrainTextureChanger : MonoBehaviour
{
    public Material materialToChange; 

    public Texture2D landingZoneAlbedo;

    public Texture2D normalTerrainAlbedo;

    void Start()
    {
        if (materialToChange != null && landingZoneAlbedo != null)
        {
            ChangeAlbedoTexture(materialToChange, normalTerrainAlbedo);
        }
    }

    public void ChangeTexture_LandingZone()
    {
        ChangeAlbedoTexture(materialToChange, landingZoneAlbedo);
    }

    public void ChangeTexture_NormalTerrain()
    {
        ChangeAlbedoTexture(materialToChange, normalTerrainAlbedo);
    }

    void ChangeAlbedoTexture(Material material, Texture2D newTexture)
    {
        material.SetTexture("_BaseMap", newTexture);
    }

    private void OnApplicationQuit()
    {
        ChangeAlbedoTexture(materialToChange, normalTerrainAlbedo);
    }
}
