using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PSLV_LaunchManager : MonoBehaviour
{
    [SerializeField]
    private Button holdButton;

    [SerializeField]
    private Slider preCheckSlider;

    [SerializeField, Tooltip("Holds all the checks TM UGUI texts")]
    private GameObject checksUI;

    [SerializeField]
    private VibrationController vibrationController;

    [SerializeField]
    private VFX_manager vfxManager;

    private HoldButton holdButtonComponent;

    [System.Serializable]
    public class RollingTextEntry
    {
        public TextMeshProUGUI text;
        public VoiceOverData voiceOverData;
    }

    [SerializeField]
    private List<RollingTextEntry> rollingTextEntries;

    [SerializeField]
    private TextMeshProUGUI finalText;

    [SerializeField]
    private VoiceOverData finalVoiceOverData;

    private int currentTextIndex = 0;

    [SerializeField] private float bigFontSize = 36f;
    [SerializeField] private float smallFontSize = 28f;
    [SerializeField] private float offsetY = 50f;

    private Vector3 centerPosition = Vector3.zero;
    private Vector3 topPosition;
    private Vector3 bottomPosition;

    private bool launchReady = false;
    private bool isPlayingVoiceOver = false;
    private bool isFinalVoiceOverPlaying = false;

    [SerializeField]
    private Button launchButton;

    [SerializeField]
    private Animator pslvAnimator;

    [SerializeField]
    private AnimationClip launchAnimation;

    private void Start()
    {
        holdButtonComponent = holdButton.GetComponent<HoldButton>();

        if (holdButtonComponent == null)
        {
            holdButtonComponent = holdButton.gameObject.AddComponent<HoldButton>();
        }

        preCheckSlider.value = 0f;

        centerPosition = Vector3.zero;
        topPosition = new Vector3(0, offsetY, 0);
        bottomPosition = new Vector3(0, -offsetY, 0);

        HideAllTexts();
        finalText.gameObject.SetActive(false); 

        launchButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        AssignPSLV_Animator();

        if (launchReady)
        {
            return;
        }

        if (holdButtonComponent.isHolding && !isPlayingVoiceOver)
        {
            StartCoroutine(PlayVoiceOverSequence());
        }
        else if (!holdButtonComponent.isHolding && (isPlayingVoiceOver || isFinalVoiceOverPlaying))
        {
            StopAllCoroutines();
            isPlayingVoiceOver = false;
            isFinalVoiceOverPlaying = false;
            currentTextIndex = 0;
            preCheckSlider.value = 0f;
            HideAllTexts();
            finalText.gameObject.SetActive(false);

            // Ensure the slider and hold button are active again
            preCheckSlider.gameObject.SetActive(true);
            holdButton.gameObject.SetActive(true);

            // Hide the LAUNCH button if it's active
            launchButton.gameObject.SetActive(false);
        }
    }


    private void AssignPSLV_Animator()
    {
        if (PlaceOnPlane.IsPSLVSpawned() && !PlaceOnPlane.IsPhase1Finished_PSLV())
        {
            ThrustVFXHandler_PSLV thrustVFXHandler_PSLV = FindObjectOfType<ThrustVFXHandler_PSLV>();

            if (thrustVFXHandler_PSLV != null)
            {
                pslvAnimator = thrustVFXHandler_PSLV.gameObject.GetComponent<Animator>();
            }
            else
            {
                Debug.LogError("thrustVFXHandler_PSLV not found in the scene.");
            }
        }
    }
    private IEnumerator PlayVoiceOverSequence()
    {
        isPlayingVoiceOver = true;
        while (currentTextIndex < rollingTextEntries.Count)
        {
            if (!holdButtonComponent.isHolding)
            {
                isPlayingVoiceOver = false;
                yield break;
            }

            UpdateRollingTextDisplay();

            // Call vibration every time the next text plays
            if (vibrationController != null)
            {
                vibrationController.VibratePhone_Medium();
            }

            var middleEntry = rollingTextEntries[currentTextIndex];

            // Trigger the voice-over using the existing VoiceOverManager
            VoiceOverManager.Instance.TriggerVoiceOver(middleEntry.voiceOverData);

            // Get the length of the voice-over clip
            float clipLength = GetVoiceOverClipLength(middleEntry.voiceOverData);

            // Wait for the duration of the clip or until the button is released
            float timeElapsed = 0f;
            while (timeElapsed < clipLength)
            {
                if (!holdButtonComponent.isHolding)
                {
                    isPlayingVoiceOver = false;
                    yield break;
                }
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            // Increase the index
            currentTextIndex++;

            // Update the preCheckSlider
            preCheckSlider.value = (float)(currentTextIndex) / rollingTextEntries.Count;
        }

        // All voice-overs have been played, proceed to OnLaunchReady()
        OnLaunchReady();
        isPlayingVoiceOver = false;
    }

    private float GetVoiceOverClipLength(VoiceOverData voiceOverData)
    {
        AudioClip clipToPlay;

        if (LanguageManager.Instance.useHindiAudio && voiceOverData.hindiVoiceOverClip != null)
        {
            clipToPlay = voiceOverData.hindiVoiceOverClip;
        }
        else
        {
            clipToPlay = voiceOverData.englishVoiceOverClip;
        }

        return clipToPlay != null ? clipToPlay.length : 0f;
    }

    private void OnLaunchReady()
    {
        Debug.Log("Launch Ready!");

        vfxManager.smokeVFX_PSLV.Play();

        // Hide rolling texts
        HideAllTexts();

        // Display the finalText
        finalText.gameObject.SetActive(true);
        SetTextAlpha(finalText, 1f);
        SetTextSize(finalText, bigFontSize);
        SetTextPosition(finalText, centerPosition);

        // Indicate that the launch is ready
        launchReady = true;

        // Ensure the slider is full
        preCheckSlider.value = 1f;

        // Deactivate the preCheckSlider and holdButton
        preCheckSlider.gameObject.SetActive(false);
        holdButton.gameObject.SetActive(false);

        // Activate the LAUNCH button
        launchButton.gameObject.SetActive(true);

        // Trigger the voice-over for the final text
        if (finalVoiceOverData != null)
        {
            StartCoroutine(PlayFinalVoiceOver());
        }
    }

    private IEnumerator PlayFinalVoiceOver()
    {
        isFinalVoiceOverPlaying = true;

        // Call vibration when the final text is displayed
        if (vibrationController != null)
        {
            vibrationController.VibratePhone_Medium();
        }

        VoiceOverManager.Instance.TriggerVoiceOver(finalVoiceOverData);

        float clipLength = GetVoiceOverClipLength(finalVoiceOverData);

        // Wait for the duration of the clip
        float timeElapsed = 0f;
        while (timeElapsed < clipLength)
        {
            // No need to check holdButton here since it's deactivated
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isFinalVoiceOverPlaying = false;

        // Proceed with any additional logic after the final voice-over completes
        // For example, you might want to enable other UI elements or animations
    }

    private void UpdateRollingTextDisplay()
    {
        // Hide all texts first
        HideAllTexts();

        // Get indices for top, middle, bottom texts
        int middleIndex = currentTextIndex % rollingTextEntries.Count;
        int topIndex = (currentTextIndex - 1 + rollingTextEntries.Count) % rollingTextEntries.Count;
        int bottomIndex = (currentTextIndex + 1) % rollingTextEntries.Count;

        // Activate and set up the middle text
        var middleEntry = rollingTextEntries[middleIndex];
        var middleText = middleEntry.text;
        middleText.gameObject.SetActive(true);
        SetTextAlpha(middleText, 1f); // Full alpha
        SetTextSize(middleText, bigFontSize); // Bigger size
        SetTextPosition(middleText, centerPosition);

        // Activate and set up the top text
        var topEntry = rollingTextEntries[topIndex];
        var topText = topEntry.text;
        topText.gameObject.SetActive(true);
        SetTextAlpha(topText, 30f / 255f); // Reduced alpha
        SetTextSize(topText, smallFontSize); // Smaller size
        SetTextPosition(topText, topPosition);

        // Activate and set up the bottom text
        var bottomEntry = rollingTextEntries[bottomIndex];
        var bottomText = bottomEntry.text;
        bottomText.gameObject.SetActive(true);
        SetTextAlpha(bottomText, 30f / 255f); // Reduced alpha
        SetTextSize(bottomText, smallFontSize); // Smaller size
        SetTextPosition(bottomText, bottomPosition);
    }

    private void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }

    private void SetTextSize(TextMeshProUGUI text, float size)
    {
        text.fontSize = size;
    }

    private void SetTextPosition(TextMeshProUGUI text, Vector3 position)
    {
        text.rectTransform.anchoredPosition = position;
    }

    private void HideAllTexts()
    {
        foreach (var entry in rollingTextEntries)
        {
            entry.text.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        launchButton.onClick.AddListener(OnLaunchButtonClicked);
    }

    private void OnDisable()
    {
        launchButton.onClick.RemoveListener(OnLaunchButtonClicked);
    }

    private void OnLaunchButtonClicked()
    {
        if (pslvAnimator != null)
        {
            pslvAnimator.SetTrigger("triggerLaunch");

            launchButton.gameObject.SetActive(false);

            checksUI.SetActive(false);

            holdButton.gameObject.SetActive(false); 
        }
        else
        {
            Debug.LogWarning("Animator is not assigned.");
        }
    }

}
