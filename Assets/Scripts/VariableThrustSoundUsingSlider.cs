using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class VariableThrustSoundUsingSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private UnityEngine.UI.Slider thrustSlider;

    [SerializeField]
    private AudioSource thrustAudioSource;

    [SerializeField]
    private AudioClip thrustSFX;

    [SerializeField]
    private VibrationController vibrationController;  // Reference to VibrationController ScriptableObject

    private bool isPlaying = false;

    void Start()
    {
        thrustSlider.onValueChanged.AddListener(OnSliderValueChanged);

        if (thrustSlider.value > 0)
        {
            PlayThrustSound();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        vibrationController.VibratePhone_Light(); // Trigger vibration when the slider is touched
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Optional: Handle pointer up if needed
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
