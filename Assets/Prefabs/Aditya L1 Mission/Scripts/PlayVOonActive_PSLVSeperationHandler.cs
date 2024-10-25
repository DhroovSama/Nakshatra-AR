using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVOonActive_PSLVSeperationHandler : MonoBehaviour
{
    [SerializeField]
    private VoiceOverData seperationVO;

    [SerializeField]
    private VibrationController vibrationController;

    void OnEnable()
    {
        vibrationController.VibratePhone_Light();

        VoiceOverManager.Instance.TriggerVoiceOver(seperationVO);
    }
}
