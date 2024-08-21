using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionalThrustSound : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private AudioSource directionalThrustPlayer;

    [SerializeField]
    private FixedJoystick fixedJoystick;

    [SerializeField]
    private float maxVolume = 1.0f; // Maximum volume when the joystick is at full input

    [SerializeField]
    private VibrationController vibrationController; // Reference to VibrationController ScriptableObject

    private bool isJoystickTouched = false;

    private void Start()
    {
        if (directionalThrustPlayer == null)
        {
            Debug.LogError("Directional Thrust AudioSource is not assigned!");
        }

        if (fixedJoystick == null)
        {
            Debug.LogError("Fixed Joystick is not assigned!");
        }
    }

    private void Update()
    {
        if (isJoystickTouched)
        {
            UpdateThrustSound();
        }
    }

    private void UpdateThrustSound()
    {
        float joystickMagnitude = fixedJoystick.Direction.magnitude;

        if (joystickMagnitude > 0.1f)
        {
            // Set the volume based on the joystick's magnitude
            directionalThrustPlayer.volume = joystickMagnitude * maxVolume;

            // Start playing the sound if it's not already playing
            if (!directionalThrustPlayer.isPlaying)
            {
                directionalThrustPlayer.Play();
            }
        }
        else
        {
            // Stop the sound if the joystick is not being touched
            if (directionalThrustPlayer.isPlaying)
            {
                directionalThrustPlayer.Stop();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isJoystickTouched = true;
        vibrationController?.VibratePhone_Light(); // Trigger vibration when joystick is touched
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isJoystickTouched = false;
        directionalThrustPlayer.Stop(); // Stop the sound when the joystick is released
    }
}
