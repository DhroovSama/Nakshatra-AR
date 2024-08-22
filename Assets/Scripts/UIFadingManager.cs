using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIFadingManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Title, text_1, text_2, text_3, text_4, text_5;

    [Space]
    [SerializeField]
    private GameObject Title_GO, BG, mainScrollUI;

    [Space]
    [SerializeField]
    private Button closebutton;

    [SerializeField]
    private RawImage line;

    [SerializeField]
    private Image BG_Blur;

    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private TextMeshProUGUI timelineText;

    [SerializeField]
    private GameObject timelineInfo;

    [Space]
    [SerializeField]
    private float fadeInDuration = 0.5f;  // Adjust fade-in duration in the Inspector

    private CanvasGroup mainScrollUIGroup;

    private Coroutine fadeCoroutine;

    [SerializeField]
    private VibrationController VibrationController;

    void Start()
    {
        BG_Blur.color = new Color(BG_Blur.color.r, BG_Blur.color.g, BG_Blur.color.b, 0f);
        mainScrollUIGroup = mainScrollUI.GetComponent<CanvasGroup>();
        mainScrollUIGroup.blocksRaycasts = false; // Initially disable raycast blocking
    }

    void Update()
    {
        // Detect touch or mouse click
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            StartFadeIn(fadeInDuration);
        }
    }

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeInMainScrollUI(duration));
    }

    private IEnumerator FadeInMainScrollUI(float duration)
    {
        VibrationController.VibratePhone_Light();

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        // Disable the timelineInfo object
        timelineInfo.SetActive(false);

        // Enable raycast blocking and fade in the mainScrollUI, timelineText, and BG_Blur
        mainScrollUIGroup.blocksRaycasts = true;

        fadeCoroutine = StartCoroutine(FadeUI(mainScrollUIGroup, 1f, duration));
        StartCoroutine(FadeText(timelineText, 1f, duration));
        StartCoroutine(FadeImage(BG_Blur, 1f, duration));

        yield return fadeCoroutine;
    }

    private IEnumerator FadeUI(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.SmoothStep(startAlpha, targetAlpha, elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }

    private IEnumerator FadeText(TextMeshProUGUI textElement, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        float startAlpha = textElement.alpha;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            textElement.alpha = Mathf.SmoothStep(startAlpha, targetAlpha, elapsedTime / duration);
            yield return null;
        }

        textElement.alpha = targetAlpha;
    }

    private IEnumerator FadeImage(Image image, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = image.color;
        float startAlpha = startColor.a;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.SmoothStep(startAlpha, targetAlpha, elapsedTime / duration);
            image.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }
}
