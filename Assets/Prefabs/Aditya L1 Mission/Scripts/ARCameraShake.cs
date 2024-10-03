using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class ARCameraShake : MonoBehaviour
{
    public Volume volume;
    private LensDistortion lensDistortion;

    [SerializeField]
    private VibrationController vibrationController;

    [Header("Shake parameters"), Space]
    [Tooltip("Duration of the shake effect")]
    public float duration = 0.5f;
    [Tooltip("Initial magnitude of the shake effect")]
    public float magnitude = 0.5f;

    [Header("Haptic feedback parameters"), Space]
    [Tooltip("Toggle haptic feedback")]
    public bool enableHaptics = true;
    [Tooltip("Duration of haptic feedback")]
    public float hapticDuration = 0.1f;

    void Start()
    {
        if (volume != null)
        {
            volume.profile.TryGet(out lensDistortion);
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
            StartCoroutine(ShakeCoroutine());
        }
        else
        {
            Debug.LogWarning("LensDistortion is not found in the URP Volume Profile.");
        }
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        float originalIntensity = lensDistortion.intensity.value;

        if (enableHaptics)
        {
            TriggerHaptics();
        }

        while (elapsed < duration)
        {
            float normalizedTime = elapsed / duration;

            float currentMagnitude = Mathf.Lerp(magnitude, 0f, normalizedTime);

            float randomIntensity = Random.Range(-currentMagnitude, currentMagnitude);

            lensDistortion.intensity.value = randomIntensity;

            elapsed += Time.deltaTime;

            yield return null;
        }

        lensDistortion.intensity.value = originalIntensity;
    }

    private void TriggerHaptics()
    {
        vibrationController.VibratePhone_Heavy();
    }
}
