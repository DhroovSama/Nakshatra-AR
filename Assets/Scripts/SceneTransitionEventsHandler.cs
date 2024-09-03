using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionEventsHandler : MonoBehaviour
{
    [SerializeField]
    private AudioClip thrustSFX;

    public void PlayThrustTransitionSFX()
    {
        GlobalAudioPlayer.GetPlaySound(thrustSFX);
    }
}
