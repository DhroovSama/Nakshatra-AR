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
    private float joystickMagnitude;

    [SerializeField]
    private float maxVolume = 1.0f; // Maximum volume when the joystick is at full input

    [SerializeField]
    private float minVolume = 0.1f; // Minimum volume when the joystick is at no input

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
        joystickMagnitude = fixedJoystick.Direction.magnitude;

        float targetVolume = Mathf.Lerp(minVolume, maxVolume, joystickMagnitude);

        if (joystickMagnitude > 0.1f)
        {
            if (!directionalThrustPlayer.isPlaying)
            {
                directionalThrustPlayer.Play();
            }

            directionalThrustPlayer.volume = targetVolume;
        }
        else
        {
            if (directionalThrustPlayer.isPlaying && directionalThrustPlayer.volume > minVolume)
            {
                directionalThrustPlayer.volume = Mathf.Lerp(directionalThrustPlayer.volume, 0f, Time.deltaTime * 2f);
                if (directionalThrustPlayer.volume <= 0.01f)
                {
                    directionalThrustPlayer.Stop();
                }
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
    }
}
