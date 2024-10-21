using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSLV_SoundHandler : MonoBehaviour
{
    [SerializeField]
    private AudioClip rocketGateOpenSFX, rocketLaunchSFX;

    //[SerializeField]
    //private 


    private void PlayRocketGateOpenSFX()
    {
        GlobalAudioPlayer.GetPlaySound(rocketGateOpenSFX);  
    }

    private void PlayRocketLaunchSFX()
    {
        GlobalAudioPlayer.GetPlaySound(rocketLaunchSFX);
    }
}
