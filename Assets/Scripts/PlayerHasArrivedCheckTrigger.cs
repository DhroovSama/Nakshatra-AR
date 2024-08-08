using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHasArrivedCheckTrigger : MonoBehaviour
{

    private static PlayerHasArrivedCheckTrigger instance;

    [SerializeField]
    private GameObject LanderTutorial;

    [Space]
    [SerializeField]
    private bool isPlayerArrivedAtLandingZone = false;

    [Space]
    [SerializeField]
    private VoiceOverData LanderTutorial_1_vo;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            VoiceOverManager.Instance.TriggerVoiceOver(LanderTutorial_1_vo);

            isPlayerArrivedAtLandingZone = true;

            LanderTutorial.SetActive(true);

            this.gameObject.SetActive(false);   
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
