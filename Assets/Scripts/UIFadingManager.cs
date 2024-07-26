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
    private Vector2 scrollPosition;

    private Vector2 lastScrollPosition;

    private CanvasGroup mainScrollUIGroup;

    void Start()
    {
        lastScrollPosition = scrollRect.normalizedPosition;

        BG_Blur.color = new Color(BG_Blur.color.r, BG_Blur.color.g, BG_Blur.color.b, 0f);

        mainScrollUIGroup = mainScrollUI.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (scrollRect.normalizedPosition != lastScrollPosition)
        {
            lastScrollPosition = scrollRect.normalizedPosition;
            scrollPosition = scrollRect.normalizedPosition;
            FadeTitle();
        }
    }

    private void FadeTitle()
    {
        float fadeTo;
        float fadeDuration = 0.15f;
        float fadeDurationOfCanvasGroup = 0.5f;

        if (scrollRect.normalizedPosition.y < 1)
        {
            fadeTo = 0f;

            //mainScrollUI.SetActive(true);
            StartFadeIn(fadeDurationOfCanvasGroup);
        }
        else if (scrollRect.normalizedPosition.y >= 1)
        {
            fadeTo = 1f;

            StartFadeOut(fadeDurationOfCanvasGroup);
            //mainScrollUI.SetActive(false);
        }
        else
        {
            fadeTo = 1f - Mathf.Abs(scrollRect.normalizedPosition.y);
        }

        Title.CrossFadeAlpha(fadeTo, fadeDuration, false);
        text_1.CrossFadeAlpha(fadeTo, fadeDuration, false);
        text_2.CrossFadeAlpha(fadeTo, fadeDuration, false);
        text_3.CrossFadeAlpha(fadeTo, fadeDuration, false);
        text_4.CrossFadeAlpha(fadeTo, fadeDuration, false);
        text_5.CrossFadeAlpha(fadeTo, fadeDuration, false);

        Color closeButtonColor = closebutton.GetComponent<Image>().color;
        closeButtonColor.a = fadeTo;
        closebutton.GetComponent<Image>().color = closeButtonColor;
        closebutton.interactable = fadeTo > 0f;

        Color lineColor = line.color;
        lineColor.a = fadeTo;
        line.color = lineColor;

        float bgBlurAlpha = 1f - fadeTo;
        Color bgBlurColor = BG_Blur.color;
        bgBlurColor.a = bgBlurAlpha;
        BG_Blur.color = bgBlurColor;
    }

    IEnumerator FadeInMainScrollUI(float duration)
    {
        mainScrollUIGroup.interactable = true;
        mainScrollUIGroup.blocksRaycasts = true;

        float elapsedTime = 0f;
        float startAlpha = mainScrollUIGroup.alpha;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / duration);
            mainScrollUIGroup.alpha = newAlpha;
            yield return null; 
        }

        mainScrollUIGroup.alpha = 1f;
    }

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeInMainScrollUI(duration));
    }

    IEnumerator FadeOutMainScrollUI(float duration)
    {
        mainScrollUIGroup.interactable = false;
        mainScrollUIGroup.blocksRaycasts = false;

        float elapsedTime = 0f;
        float startAlpha = mainScrollUIGroup.alpha;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / duration);
            mainScrollUIGroup.alpha = newAlpha;
            yield return null;
        }

        //mainScrollUIGroup.alpha = 0f;
    }

    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeOutMainScrollUI(duration));
    }
}
