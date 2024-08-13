using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationButtonUIProvider : MonoBehaviour
{
    [SerializeField]
    private List<RawImage> locationUIList = new List<RawImage>();

    [SerializeField]
    private List<GameObject> loactionUIListContainGameobjects = new List<GameObject>();

    public List<RawImage> GetLocationUIList()
    {
        return locationUIList;
    }

    public List<GameObject> GetLocationUIListContainGameobjects()
    {
        return loactionUIListContainGameobjects;
    }
}
