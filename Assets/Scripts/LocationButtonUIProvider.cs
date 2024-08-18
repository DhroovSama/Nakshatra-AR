using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationButtonUIProvider : MonoBehaviour
{
    [SerializeField]
    private List<RawImage> locationUIList = new List<RawImage>();

    [Space]
    [SerializeField]
    private List<GameObject> loactionUIListContainGameobjects = new List<GameObject>();

    [Space]
    [SerializeField]
    private List<GameObject> collectible_1_Element, collectible_2_LunarSurfaceImages, collectible_3_TempratureData, collectible_4_TopographicalData, collectible_5_SouthPole;

    public List<RawImage> GetLocationUIList()
    {
        return locationUIList; 
    }

    public List<GameObject> GetLocationUIListContainGameobjects()
    {
        return loactionUIListContainGameobjects;
    }

    public List<GameObject> GetCollectible_1_Element()
    {
        return collectible_1_Element;
    }

    public List<GameObject> GetCollectible_2_LunarSurfaceImages()
    {
        return collectible_2_LunarSurfaceImages;
    }

    public List<GameObject> GetCollectible_3_TempratureDatas()
    {
        return collectible_3_TempratureData;
    }

    public List<GameObject> GetCollectible_4_TopographicalData()
    {
        return collectible_4_TopographicalData;
    }

    public List<GameObject> GetCollectible_5_SouthPole()
    {
        return collectible_5_SouthPole;
    }
}
