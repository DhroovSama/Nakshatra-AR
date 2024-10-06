using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeHandler : MonoBehaviour
{
    [SerializeField]
    private RawImage cameraFade;

    [SerializeField, Range(0f,10f), Tooltip("Duration of the fade in seconds")]
    private float fadeDuration = 1.0f; 

    private Coroutine fadeCoroutine;

    public void FadeIn()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeToAlpha(1.0f));
    }

    public void FadeOut()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeToAlpha(0.0f));
    }

    private IEnumerator FadeToAlpha(float targetAlpha)
    {
        float startAlpha = cameraFade.color.a;
        float elapsedTime = 0f;

        Color color = cameraFade.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            cameraFade.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        cameraFade.color = color;

        fadeCoroutine = null;
    }
}
