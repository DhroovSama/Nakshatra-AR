using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ObjectDisabler_PSLVSeperation : MonoBehaviour
{
    [SerializeField]
    private GameObject phaseSeperationUI;

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

    [SerializeField] private AudioClip rocketSeperationSFX;

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

    public void PlayRocketSeperationSFX()
    {
        Handheld.Vibrate();
        GlobalAudioPlayer.GetPlaySound(rocketSeperationSFX);
    }

    public void DisableSeperationPhaseUI()
    {
        GlobalUIProvider_AdityaL1.getSeperationPhaseUI().SetActive(false);
    }
}
