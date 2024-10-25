using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UIProviderFromGlobalUIProvider : MonoBehaviour
{
    [SerializeField]
    private VoiceOverData Phase2SeperationStageVO;

    private void EnablePSLVSeperationTutorial()
    {
        GlobalUIProvider_AdityaL1.getSeperationPhaseTutorial().SetActive(true);
        VoiceOverManager.Instance.TriggerVoiceOver(Phase2SeperationStageVO);
    }
}
