using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHasArrivedCheckTrigger : MonoBehaviour
{

    private static PlayerHasArrivedCheckTrigger instance;

    [SerializeField]
    private GameObject LanderTutorial, playerChecker;

    [Space]
    [SerializeField]
    private bool isPlayerArrivedAtLandingZone = false;

    [Space]
    [SerializeField]
    private VoiceOverData LanderTutorial_1_vo;

    [Space]
    [SerializeField]
    private VibrationController vibrationController;

    [Space]
    [SerializeField]
    private UISoundSO soundSO;

    [Space]
    [SerializeField]
    private AudioClip playerCheckerSFX;



    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            vibrationController.VibratePhone_Medium();

            GlobalAudioPlayer.GetPlaySound(playerCheckerSFX);

            soundSO.PlayPlayerCheckerSound();

            VoiceOverManager.Instance.TriggerVoiceOver(LanderTutorial_1_vo);

            isPlayerArrivedAtLandingZone = true;

            LanderTutorial.SetActive(true);

            playerChecker.SetActive(false);

            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void LaunchLanderUITutorial()
    {

    }

    public static bool get_IsPlayerArrivedAtLandingZone()
    {
        return instance.isPlayerArrivedAtLandingZone;
    }
}
