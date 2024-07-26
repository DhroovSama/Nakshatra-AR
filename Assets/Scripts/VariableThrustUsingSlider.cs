using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableThrustUsingSlider : MonoBehaviour
{
    [SerializeField]
    private Slider thrustSlider;

    [SerializeField]
    private ParticleSystem mainThrustVFX;

    private void Start()
    {
        thrustSlider = LanderControlsUIManager.getsliderControls();
    }

    private void Update()
    {
        ChangeThrustVFXAccordingToSlider();
    }

    private void ChangeThrustVFXAccordingToSlider()
    {
        ParticleSystem.MainModule main = mainThrustVFX.main;

        float newSize = Mathf.Lerp(0.0f, 0.25f, thrustSlider.value);

        main.startSizeMultiplier = newSize;
    }

}
