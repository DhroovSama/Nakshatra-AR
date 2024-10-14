using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OscillateAlphaValue : MonoBehaviour
{
    [SerializeField]
    private RawImage tutorialUI;

    [SerializeField, Range(1f, 10f), Tooltip("Oscillation rate")]
    private float oscillationSpeed = 1f;

    [SerializeField, Tooltip("Duration to oscillate before disabling")]
    private float duration = 5f; // Set the desired duration in seconds

    private bool isOscillating = false;

    void Update()
    {
        if (isOscillating && tutorialUI != null)
        {
            // Calculate a value that oscillates between 0 and 1 over time
            float t = (Mathf.Sin(Time.time * oscillationSpeed) + 1f) / 2f;

            // Interpolate between green and white based on t
            Color newColor = Color.Lerp(Color.green, Color.white, t);

            // Apply the new color to the RawImage
            tutorialUI.color = newColor;
        }
    }

    public void StartOscillation()
    {
        StartCoroutine(OscillateAndDisable());
    }

    private IEnumerator OscillateAndDisable()
    {
        isOscillating = true;
        yield return new WaitForSeconds(duration);
        isOscillating = false;
        gameObject.SetActive(false); // Disables the GameObject
    }
}
