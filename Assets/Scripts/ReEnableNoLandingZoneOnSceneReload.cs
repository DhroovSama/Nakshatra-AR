using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReEnableNoLandingZoneOnSceneReload : MonoBehaviour
{
    [SerializeField]
    private TriggerTerrainScanner triggerTerrainScanner;

    public void EnableNoLandingZones()
    {
        triggerTerrainScanner.NoLandingZones.SetActive(true);
    }
}
