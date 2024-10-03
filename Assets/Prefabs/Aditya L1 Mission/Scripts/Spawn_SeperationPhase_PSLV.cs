using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_SeperationPhase_PSLV : MonoBehaviour
{
    [SerializeField]
    private GameObject seperationPSLV;

    [SerializeField, Tooltip("Auto Assigned")]
    private PlaceOnPlane placeOnPlane;

    private void Awake()
    {
        placeOnPlane = FindObjectOfType<PlaceOnPlane>();
    }

    private void SpawnSeperationPhasePSLV()
    {
        var instantiationCords = placeOnPlane.GetMoonSurfacePosition();

        Instantiate(seperationPSLV, instantiationCords, Quaternion.identity);
    }
}
