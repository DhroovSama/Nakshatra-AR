using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;

public class ARCameraShake : MonoBehaviour
{
    public Volume volume;
    private LensDistortion lensDistortion;
    private ColorAdjustments colorAdjustments;

    [SerializeField]
    private VibrationController vibrationController;

    [Header("Shake Parameters"), Space]
    [Tooltip("Duration of the shake effect")]
    public float duration = 0.5f;
    [Tooltip("Initial magnitude of the shake effect")]
    public float magnitude = 0.5f;

    [Header("Light Shake Parameters"), Space]
    [Tooltip("Duration of the light shake effect")]
    public float lightDuration = 0.25f;
    [Tooltip("Initial magnitude of the light shake effect")]
    public float lightMagnitude = 0.25f;

    [Header("Haptic Feedback Parameters"), Space]
    [Tooltip("Toggle haptic feedback")]
    public bool enableHaptics = true;
    [Tooltip("Duration of haptic feedback")]
    public float hapticDuration = 0.1f;

    [Header("Black and White Effect Parameters"), Space]
    [Tooltip("Duration of the black and white effect")]
    public float bwEffectDuration = 0.5f;
    [Tooltip("Target saturation value (-100 is full black and white, 0 is normal color)")]
    [Range(-100f, 0f)]
    public float targetSaturation = -50f;

    void Start()
    {
        if (volume != null)
        {
            volume.profile.TryGet(out lensDistortion);
            volume.profile.TryGet(out colorAdjustments);
        }
        else
        {
            Debug.LogError("Volume is not assigned.");
        }
    }

    public void TriggerShake()
    {
        if (lensDistortion != null)
        {
            StartCoroutine(ShakeCoroutine(duration, magnitude));
        }
        else
        {
            Debug.LogWarning("LensDistortion is not found in the URP Volume Profile.");
        }
    }

    public void TriggerLightShake()
    {
        if (lensDistortion != null)
        {
            StartCoroutine(ShakeCoroutine(lightDuration, lightMagnitude));
        }
        else
        {
            Debug.LogWarning("LensDistortion is not found in the URP Volume Profile.");
        }
    }

    public void TriggerBlackAndWhite()
    {
        if (colorAdjustments != null)
        {
            StartCoroutine(BlackAndWhiteCoroutine());
        }
        else
        {
            Debug.LogWarning("ColorAdjustments is not found in the URP Volume Profile.");
        }
    }
    public void TriggerBlackAndWhite_withTime(float bwTime)
    {
        if (colorAdjustments != null)
        {
            StartCoroutine(BlackAndWhiteCoroutine_withTime(bwTime));
        }
        else
        {
            Debug.LogWarning("ColorAdjustments is not found in the URP Volume Profile.");
        }
    }

    private IEnumerator ShakeCoroutine(float customDuration, float customMagnitude)
    {
        float elapsed = 0.0f;
        float originalIntensity = lensDistortion.intensity.value;

        if (enableHaptics)
        {
            TriggerHaptics();
        }

        while (elapsed < customDuration)
        {
            float normalizedTime = elapsed / customDuration;

            float currentMagnitude = Mathf.Lerp(customMagnitude, 0f, normalizedTime);

            float randomIntensity = Random.Range(-currentMagnitude, currentMagnitude);

            lensDistortion.intensity.value = randomIntensity;

            elapsed += Time.deltaTime;

            yield return null;
        }

        lensDistortion.intensity.value = originalIntensity;
    }

    private IEnumerator BlackAndWhiteCoroutine()
    {
        float elapsed = 0.0f;
        float originalSaturation = colorAdjustments.saturation.value;

        // Fade to target saturation
        while (elapsed < bwEffectDuration)
        {
            float normalizedTime = elapsed / bwEffectDuration;

            colorAdjustments.saturation.value = Mathf.Lerp(originalSaturation, targetSaturation, normalizedTime);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Optional: Hold the effect for a brief moment
        yield return new WaitForSeconds(0.1f);

        // Fade back to original saturation
        elapsed = 0.0f;

        while (elapsed < bwEffectDuration)
        {
            float normalizedTime = elapsed / bwEffectDuration;

            colorAdjustments.saturation.value = Mathf.Lerp(targetSaturation, originalSaturation, normalizedTime);

            elapsed += Time.deltaTime;

            yield return null;
        }

        colorAdjustments.saturation.value = originalSaturation;
    }
    private IEnumerator BlackAndWhiteCoroutine_withTime(float bwDuration)
    {
        float elapsed = 0.0f;
        float originalSaturation = colorAdjustments.saturation.value;

        // Fade to target saturation
        while (elapsed < bwDuration)
        {
            float normalizedTime = elapsed / bwDuration;

            colorAdjustments.saturation.value = Mathf.Lerp(originalSaturation, targetSaturation, normalizedTime);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Optional: Hold the effect for a brief moment
        yield return new WaitForSeconds(0.1f);

        // Fade back to original saturation
        elapsed = 0.0f;

        while (elapsed < bwDuration)
        {
            float normalizedTime = elapsed / bwDuration;

            colorAdjustments.saturation.value = Mathf.Lerp(targetSaturation, originalSaturation, normalizedTime);

            elapsed += Time.deltaTime;

            yield return null;
        }

        colorAdjustments.saturation.value = originalSaturation;
    }

    private void TriggerHaptics()
    {
        vibrationController.VibratePhone_Heavy();
    }
}
