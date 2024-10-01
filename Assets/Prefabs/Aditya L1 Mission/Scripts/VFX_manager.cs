using System.Collections;
using UnityEngine;

public class VFX_manager : MonoBehaviour
{
    [SerializeField, Tooltip("Assigned automatically")]
    public ParticleSystem smokeVFX_PSLV, thrustVFX_PSLV;

    private void Update()
    {
        if (PlaceOnPlane.IsPSLVSpawned())
        {
            GameObjectAssigner gameObjectAssigner = FindObjectOfType<GameObjectAssigner>();

            if (gameObjectAssigner != null)
            {
                smokeVFX_PSLV = gameObjectAssigner.smokeVFX;
                thrustVFX_PSLV = gameObjectAssigner.thrustVFX;
            }
            else
            {
                Debug.LogError("gameObjectAssigner not found in the scene.");
            }
        }
    }

    public void PlayThrustVFXAndIncreaseEmission()
    {
        if (thrustVFX_PSLV != null)
        {
            thrustVFX_PSLV.Play();

            StartCoroutine(IncreaseEmissionOverTime(5f, 50f));
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
