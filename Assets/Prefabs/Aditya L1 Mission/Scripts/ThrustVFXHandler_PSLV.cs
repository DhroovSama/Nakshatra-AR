using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustVFXHandler_PSLV : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem thrustVFX_PSLV;

    [SerializeField, Range(1f, 20f), Tooltip("how long till the emmission rate will increase")]
    private float duration;

    [SerializeField, Range(1f, 200f), Tooltip("the emmission rate of the thrust vfx")]
    private float targetRate;

    public void PlayThrustVFXAndIncreaseEmission()
    {
        if (thrustVFX_PSLV != null)
        {
            thrustVFX_PSLV.Play();

            StartCoroutine(IncreaseEmissionOverTime(duration, targetRate));
        }
        else
        {
            Debug.LogError("thrustVFX_PSLV is not assigned.");
        }
    }

    private IEnumerator IncreaseEmissionOverTime(float duration, float targetRate)
    {
        ParticleSystem.EmissionModule emissionModule = thrustVFX_PSLV.emission;
        float initialRate = emissionModule.rateOverTime.constant;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float newRate = Mathf.Lerp(initialRate, targetRate, timeElapsed / duration);
            var rateOverTime = emissionModule.rateOverTime;
            rateOverTime = new ParticleSystem.MinMaxCurve(newRate);
            emissionModule.rateOverTime = rateOverTime;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        var finalRateOverTime = emissionModule.rateOverTime;
        finalRateOverTime = new ParticleSystem.MinMaxCurve(targetRate);
        emissionModule.rateOverTime = finalRateOverTime;
    }
}
