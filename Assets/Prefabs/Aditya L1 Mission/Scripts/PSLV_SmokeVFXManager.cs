using System.Collections;
using UnityEngine;

public class PSLV_SmokeVFXManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem smokeVFX;

    private void StartIncreasingRadiusAndEmission(float targetRadius, float targetEmissionRate, float duration)
    {
        if (smokeVFX == null)
        {
            Debug.LogError("smokeVFX is not assigned.");
            return;
        }

        StartCoroutine(IncreaseRadiusAndEmissionOverTime(targetRadius, targetEmissionRate, duration));
    }

    private IEnumerator IncreaseRadiusAndEmissionOverTime(float targetRadius, float targetEmissionRate, float duration)
    {
        var shape = smokeVFX.shape;
        float initialRadius = shape.radius;

        var emission = smokeVFX.emission;
        float initialEmissionRate = emission.rateOverTime.constant;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;


            float newRadius = Mathf.Lerp(initialRadius, targetRadius, t);
            float newEmissionRate = Mathf.Lerp(initialEmissionRate, targetEmissionRate, t);

            shape.radius = newRadius;

            var rate = emission.rateOverTime;
            rate.constant = newEmissionRate;
            emission.rateOverTime = rate;

            yield return null; 
        }

        shape.radius = targetRadius;

        var finalRate = emission.rateOverTime;
        finalRate.constant = targetEmissionRate;
        emission.rateOverTime = finalRate;
    }
}
