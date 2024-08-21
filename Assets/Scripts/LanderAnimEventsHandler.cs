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

    [SerializeField]
    [Space]
    private VoiceOverData RoverTutorial_1_VO;

    [Space]
    [Header("Audio")]

    [SerializeField]
    private AudioSource landerAudioPlayer;

    [SerializeField]
    private AudioClip doorOpeningSFX;

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
    private void PlayDoorOpeningSFX()
    {
        if (landerAudioPlayer != null && doorOpeningSFX != null)
        {
            landerAudioPlayer.PlayOneShot(doorOpeningSFX);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip not assigned.");
        }
    }


    private void RoverAnimationEndEvent()
    {
        VoiceOverManager.Instance.TriggerVoiceOver(RoverTutorial_1_VO);

        RoverTutorialUI.SetActive(true);

        landerAnimation.OnRoverAnimationEnd();
    }

}
