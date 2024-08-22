using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoverSoundManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    private AudioSource roverAudioPlayer;

    [SerializeField]
    private AudioClip roverMovingSFX, roverTurningSFX, roverBrakingSFX, roverRotationBrakingSFX;

    [Header("Controls")]
    [SerializeField]
    private Button forward;
    [SerializeField]
    private Button backward;
    [SerializeField]
    private Button right, left;

    // Function to play rover moving sound on PointerDown
    public void OnPointerDown_PlayRoverMovingSFX()
    {
        PlaySound(roverMovingSFX);
    }

    // Function to play rover turning sound on PointerDown
    public void OnPointerDown_PlayRoverTurningSFX()
    {
        PlaySound(roverTurningSFX);
    }

    // Function to stop rover sound and play braking sound on PointerUp
    public void OnPointerUp_StopRoverSFX()
    {
        StopSound();
        PlaySound(roverBrakingSFX);
    }

    // Helper function to play a sound
    private void PlaySound(AudioClip clip)
    {
        if (roverAudioPlayer != null && clip != null)
        {
            roverAudioPlayer.clip = clip;
            roverAudioPlayer.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip not assigned.");
        }
    }

    // Helper function to stop playing sound
    private void StopSound()
    {
        if (roverAudioPlayer != null)
        {
            roverAudioPlayer.Stop();
        }
    }
}
