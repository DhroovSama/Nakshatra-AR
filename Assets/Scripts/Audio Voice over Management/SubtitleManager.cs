using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance;
    public TextMeshProUGUI subtitleText;
    public float displayDuration = 5f;
    public float typewriterSpeed = 0.05f; // Speed of the typewriter effect

    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplaySubtitle(string subtitle)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeSubtitle(subtitle));
    }

    private IEnumerator TypeSubtitle(string subtitle)
    {
        subtitleText.text = "";
        foreach (char letter in subtitle.ToCharArray())
        {
            subtitleText.text += letter;
            yield return new WaitForSeconds(typewriterSpeed);
        }

        yield return new WaitForSeconds(displayDuration);

        ClearSubtitle();
    }

    private void ClearSubtitle()
    {
        subtitleText.text = "";
    }
}
