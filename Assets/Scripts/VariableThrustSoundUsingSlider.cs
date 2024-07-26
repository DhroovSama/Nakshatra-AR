using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableThrustSoundUsingSlider : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Slider thrustSlider;

    [SerializeField]
    private AudioSource thrustAudioSource;

    [SerializeField]
    private AudioClip thrustSFX;

    private bool isPlaying = false; 

    void Start()
    {
        thrustSlider.onValueChanged.AddListener(OnSliderValueChanged);

        if (thrustSlider.value > 0)
        {
            PlayThrustSound();
        }
    }

    void OnSliderValueChanged(float newValue)
    {
        if (newValue > 0 && !isPlaying)
        {
            PlayThrustSound();
        }
        else if (newValue <= 0 && isPlaying)
        {
            StopThrustSound();
        }

        float scaledVolume = newValue / 10f;
        thrustAudioSource.volume = Mathf.Clamp(scaledVolume, 0, 1); 
    }


    void PlayThrustSound()
    {
        if (!isPlaying)
        {
            thrustAudioSource.clip = thrustSFX;
            thrustAudioSource.Play();
            isPlaying = true;
        }
    }

    void StopThrustSound()
    {
        if (isPlaying)
        {
            thrustAudioSource.Stop();
            isPlaying = false;
        }
    }

    void OnDestroy()
    {
        thrustSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
