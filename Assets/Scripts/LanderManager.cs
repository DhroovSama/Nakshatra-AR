using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderManager : MonoBehaviour
{
    public void EnableTerrainScannerButton()
    {
        LanderControlsUIManager.getTerrainScannerControlButton().SetActive(true);
    }
}
