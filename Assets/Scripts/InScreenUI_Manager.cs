using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InScreenUI_Manager : MonoBehaviour
{
    [SerializeField]
    private PortalDoor portalDoor;

    [SerializeField]
    private GameObject footPrints;

    [Header("In Screen UI")]

    [SerializeField]
    [Tooltip("Follow Footsteps UI")]
    private GameObject Popup_1;

    [Header("Lander Tutorial Popup 4")]
    [SerializeField]
    private GameObject landerTutorial_PopUp4;

    [SerializeField]
    private GameObject OtherUI_Popup4;

    [SerializeField]
    private GameObject AllTheBestTextbox;

    //[SerializeField]
    //private GameObject sliderInstructions_PopUp4;

    [Space] 
    [SerializeField]
    private bool isPopup_1_enabled = false;

    [SerializeField]
    [Tooltip("time for Popup 1 of foot to Enable in seconds")]
    private float timeforPopup1Enable;

    [Space]
    [SerializeField]
    private VoiceOverData followFootstepSO;

    private void Update()
    {
        if(portalDoor != null && !isPopup_1_enabled)
        {
            if(portalDoor.IsPortalPassed)
            {
                Invoke("Popup_1_Enable", timeforPopup1Enable);
            }
        }
    }

    private void Popup_1_Enable()
    {
        Popup_1.SetActive(true);

        footPrints.SetActive(true);

        isPopup_1_enabled = true;
    }

    public void EnableJoystickWithHand_Lander()
    {
        LanderControlsUIManager.getDirectionalThrustJoystickContainer().SetActive(true);

        LanderControlsUIManager.GetHandTouchUI().SetActive(true);
    }

    public void DisableJoystickWithHand_Lander()
    {
        LanderControlsUIManager.getDirectionalThrustJoystickContainer().SetActive(false);

        LanderControlsUIManager.GetHandTouchUI().SetActive(false);
    }

    public void EnableThrustSliderWithHandsSwipe_Lander()
    {
        LanderControlsUIManager.GetHandSwipeUI().SetActive(true);
    }

    public void DisableThrustSliderWithHandsSwipe_Lander()
    {
        LanderControlsUIManager.GetHandSwipeUI().SetActive(false);

    }

    public void DisablePopup_LanderTutorial()
    {
        //sliderInstructions_PopUp4.SetActive(false);

        AllTheBestTextbox.SetActive(true);

        OtherUI_Popup4.SetActive(false);

        enable_LanderSpawnerButtonContainer();

        Invoke("disable_Popup3_LanderTutorial", 1.5f);
    }

    private void disable_Popup3_LanderTutorial()
    {
        landerTutorial_PopUp4.SetActive(false);
    }

    public void enable_LanderSpawnerButtonContainer()
    {
        LanderControlsUIManager.getLanderSpawnerButtonContainer().SetActive(true);
    }

    public void EnableTerrainScannerButtonUI()
    {
        LanderControlsUIManager.getTerrainScannerButton().SetActive(true);
    }

    public void DisableTerrainScannerButtonUI()
    {
        LanderControlsUIManager.getTerrainScannerButton().SetActive(false);
    }

    public void playFollowFootstepsVO()
    {
        VoiceOverManager.Instance.TriggerVoiceOver(followFootstepSO);
    }
}
