using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverTutorialUIManager : MonoBehaviour
{
    public void EnableRoverArrowsTutorialUI()
    {
        LanderControlsUIManager.GetRoverTutorialControls().SetActive(true);
    }

    public void DisableRoverArrowsTutorialUI()
    {
        LanderControlsUIManager.GetRoverTutorialControls().SetActive(false);
    }

    public void EnableFactsCountUI()
    {
        FactsCollectedGlobal.getFactsCountUI().SetActive(true);
    }
}
