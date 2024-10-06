using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisabler_PSLVSeperation : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> bigRocket = new List<GameObject>();

    [SerializeField]
    private GameObject boosterBottom;

    [SerializeField]
    private GameObject boosterMiddle;

    [SerializeField]
    private GameObject boosterTop;

    [SerializeField]
    private List<GameObject> smallRocket = new List<GameObject>();

    [SerializeField]
    private List<GameObject> heatShield = new List<GameObject>();

    public void DestroyObjects_Phase1()
    {
        foreach (GameObject go in bigRocket)
        {
            Destroy(go);
        }
    }

    public void DestroyObjects_Phase2()
    {
        foreach (GameObject go in smallRocket)
        {
            Destroy(go);
        }

    }
    public void DestroyObjects_Phase3()
    {
        Destroy(boosterBottom);
        Destroy(boosterMiddle);
    }

    public void DestroyObjects_Phase4()
    {
        foreach(GameObject go in heatShield)
        {
            Destroy(go);
        }
    }
    public void DestroyObjects_Phase5()
    {
        Destroy(boosterTop);
    }
}
