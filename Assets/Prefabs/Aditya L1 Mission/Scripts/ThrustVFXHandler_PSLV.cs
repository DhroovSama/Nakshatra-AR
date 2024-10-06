using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustVFXHandler_PSLV : MonoBehaviour
{
    [SerializeField]
    public List<ParticleSystem> thrustVFXSystems_PSLV; // List to hold multiple ParticleSystems

    [SerializeField]
    private ParticleSystem thrustSmokeVFX;

    [SerializeField, Range(1f, 20f), Tooltip("How long till the emission rate will increase")]
    private float duration;

    [SerializeField, Range(1f, 200f), Tooltip("The emission rate of the thrust VFX")]
    private float targetRate;

    public void PlayThrustVFXAndIncreaseEmission()
    {
        if (thrustVFXSystems_PSLV != null && thrustVFXSystems_PSLV.Count > 0)
        {
            foreach (ParticleSystem thrustVFX_PSLV in thrustVFXSystems_PSLV)
            {
                if (thrustVFX_PSLV != null)
                {
                    thrustVFX_PSLV.Play();
                    StartCoroutine(IncreaseEmissionOverTime(thrustVFX_PSLV, duration, targetRate));
                }
                else
                {
                    Debug.LogError("A ParticleSystem in thrustVFXSystems_PSLV is not assigned.");
                }
            }
        }
        else
        {
            Debug.LogError("thrustVFXSystems_PSLV is not assigned or is empty.");
        }
    }

    private IEnumerator IncreaseEmissionOverTime(ParticleSystem particleSystem, float duration, float targetRate)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
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

        // Set the final target rate after the loop completes
        var finalRateOverTime = emissionModule.rateOverTime;
        finalRateOverTime = new ParticleSystem.MinMaxCurve(targetRate);
        emissionModule.rateOverTime = finalRateOverTime;
    }

    private void PlaySmokeThrustVFX()
    {
        thrustSmokeVFX.Play();
    }
}
