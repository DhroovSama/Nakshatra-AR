using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance;
    public TextMeshProUGUI subtitleText;

    [Range(0f, 1f)]
    [Tooltip("Typewriter effect rate for typing each word")]
    public float typewriterSpeed = 0.05f;

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

    public void DisplaySubtitles(List<VoiceOverData.SubtitleLine> subtitleLines)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeSubtitles(subtitleLines));
    }

    private IEnumerator TypeSubtitles(List<VoiceOverData.SubtitleLine> subtitleLines)
    {
        foreach (var line in subtitleLines)
        {
            subtitleText.text = "";
            foreach (char letter in line.sentence.ToCharArray())
            {
                subtitleText.text += letter;
                yield return new WaitForSeconds(typewriterSpeed);
            }

            yield return new WaitForSeconds(line.displayDuration);
        }

        ClearSubtitle();
    }

    private void ClearSubtitle()
    {
        subtitleText.text = "";
    }
}
