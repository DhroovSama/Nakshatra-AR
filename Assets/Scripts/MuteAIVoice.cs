using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAIVoice : MonoBehaviour
{
    [SerializeField]
    private AudioSource AI_audioSource;

    [SerializeField]
    private bool isAiMute = false;

    public void MuteUnmuteAIFunction()
    {
        if(isAiMute)
        {
            AIVoice_UnMute();

            isAiMute = false;
        }
        else
        {
            AIVoice_Mute();

            isAiMute = true;
        }
    }

    private void AIVoice_Mute()
    {
        AI_audioSource.mute = true;
    }

    private void AIVoice_UnMute()
    {
        AI_audioSource.mute = false;
    }
}
