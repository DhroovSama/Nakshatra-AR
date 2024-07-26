using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderAnimEventsHandler : MonoBehaviour
{
    [SerializeField]
    private UI_Manager ui_Manager;

    [SerializeField] LanderAnimation landerAnimation;

    [SerializeField] GameObject Rover;

    [SerializeField]
    private bool isLanderAnimationEnded = false;
    public bool IsLanderAnimationEnded { get { return isLanderAnimationEnded; } }

    [SerializeField]
    private GameObject RoverTutorialUI;


    public GameObject RoverPrefab
    {
        get { return Rover; }
    }

    private void Awake()
    {
        landerAnimation = FindObjectOfType<LanderAnimation>();

        ui_Manager = FindObjectOfType<UI_Manager>();
    }

    private void LandingAnimationEndEvent()
    {
        landerAnimation.OnLandingAnimationEnd();

        isLanderAnimationEnded = true;

        ui_Manager.EnableLander_Controls_Info_UI();
        LanderControlsUIManager.getDirectionalThrustJoystickContainer().SetActive(true);
    } 

    private void EnableUserTakesControlOverUI()
    {
        StartCoroutine(EnableUserWarningBeforeGivingControlOver());
    }

    private IEnumerator EnableUserWarningBeforeGivingControlOver()
    {
        LanderControlsUIManager.GetUserWarningToGiveControlOver().SetActive(true);

        yield return new WaitForSeconds(2f);

        LanderControlsUIManager.GetUserWarningToGiveControlOver().SetActive(false);
    }
    
    private void DoorAnimationEndEvent()
    {
        landerAnimation.OnDoorAnimationEnd();
    }

    private void RoverAnimationEndEvent()
    {
        RoverTutorialUI.SetActive(true);

        landerAnimation.OnRoverAnimationEnd();
    }

}
