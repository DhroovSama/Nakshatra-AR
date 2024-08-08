using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance;
    public TextMeshProUGUI subtitleText;

    [Range(0f, 1f)]
    [Tooltip("Typewriter effect rate for typing each word")]
    public float typewriterSpeed = 0.05f;

    public Button actionButton; // Reference to the button
    public TextMeshProUGUI buttonText; // Text for the button

    private Coroutine typingCoroutine;
    private VoiceOverData nextVoiceOverData;

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

    public void DisplaySubtitles(List<VoiceOverData.SubtitleLine> subtitleLines, bool showButton, string buttonLabel, VoiceOverData nextData)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        actionButton.gameObject.SetActive(false); // Ensure the button is hidden initially
        nextVoiceOverData = nextData; // Store the next VoiceOverData reference
        typingCoroutine = StartCoroutine(TypeSubtitles(subtitleLines, showButton, buttonLabel));
    }

    private IEnumerator TypeSubtitles(List<VoiceOverData.SubtitleLine> subtitleLines, bool showButton, string buttonLabel)
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

        // Show the button if specified
        if (showButton)
        {
            buttonText.text = buttonLabel;
            actionButton.gameObject.SetActive(true);
            actionButton.onClick.RemoveAllListeners(); // Clear previous listeners
            actionButton.onClick.AddListener(OnButtonClicked);
        }
    }

    private void ClearSubtitle()
    {
        subtitleText.text = "";
    }

    private void OnButtonClicked()
    {
        if (nextVoiceOverData != null)
        {
            VoiceOverManager.Instance.TriggerVoiceOver(nextVoiceOverData);
        }
        actionButton.gameObject.SetActive(false); // Hide the button after it's clicked
    }
}
