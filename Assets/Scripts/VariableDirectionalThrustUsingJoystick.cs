using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableDirectionalThrustUsingJoystick : MonoBehaviour
{
    [SerializeField]
    private FixedJoystick directionalThrustJoystick;

    [Space]
    [SerializeField]
    private ParticleSystem frontThrustVFX, BackThrustVFX, rightThrustVFX, leftThrustVFX;

    [Space]
    [SerializeField]
    private Vector2 joyStickValues;

    private void Start()
    {
        directionalThrustJoystick = LanderControlsUIManager.getDirectionalThrustJoystick();
    }

    private void Update()
    {
        joyStickValues = directionalThrustJoystick.Direction;

        RightThrustVFXController();
        LeftThrustVFXController();
        FrontThrustVFXController();
        BackThrustVFXController();
    }


    private void RightThrustVFXController()
    {
        ParticleSystem.MainModule main = rightThrustVFX.main;

        float newStartSizeMultiplier = Mathf.Clamp(joyStickValues.x * 3, 0, 1f); 

        main.startSizeMultiplier = newStartSizeMultiplier / 3; 
    }
    private void LeftThrustVFXController()
    {
        ParticleSystem.MainModule main = leftThrustVFX.main;

        float newStartSizeMultiplier = Mathf.Clamp(-joyStickValues.x * 3, 0, 1f);

        main.startSizeMultiplier = newStartSizeMultiplier / 3;
    }

    private void FrontThrustVFXController()
    {
        ParticleSystem.MainModule main = frontThrustVFX.main;

        float newStartSizeMultiplier = Mathf.Clamp(joyStickValues.y * 3, 0, 1f);

        main.startSizeMultiplier = newStartSizeMultiplier / 3;
    }

    private void BackThrustVFXController()
    {
        ParticleSystem.MainModule main = BackThrustVFX.main;

        float newStartSizeMultiplier = Mathf.Clamp(-joyStickValues.y * 3, 0, 1f);

        main.startSizeMultiplier = newStartSizeMultiplier / 3;
    }



}
