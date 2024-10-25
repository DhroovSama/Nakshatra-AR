using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSLVCameraShakeHandler : MonoBehaviour
{
    [SerializeField]
    private ARCameraShake arCameraShake;

    private void Awake()
    {
        arCameraShake = FindObjectOfType<ARCameraShake>();
    }

    public void TriggerCameraShake()
    {
        arCameraShake.TriggerShake();
    }

    public void TriggerSeperationStageCameraShake()
    {
        arCameraShake.TriggerBlackAndWhite();
        arCameraShake.TriggerLightShake();
    }
}
