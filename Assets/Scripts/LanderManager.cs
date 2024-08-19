using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderManager : MonoBehaviour
{
    [SerializeField]
    private SunlightTimer sunlightTimerScript;

    private void Awake()
    {
        if(sunlightTimerScript == null)
        {
            sunlightTimerScript = FindObjectOfType<SunlightTimer>();
        }
    }

    public void StartNightTimer()
    {
        sunlightTimerScript.StartRotateSunLight_Night();
    }

    public void EnableTerrainScannerButton()
    {
        LanderControlsUIManager.getTerrainScannerControlButton().SetActive(true);
    }
}
