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
        [Tooltip("The UI text associated with this entry.")]
        public TextMeshProUGUI text;

        [Tooltip("Voice-over data for this entry.")]
        public VoiceOverData voiceOverData;

        [Tooltip("Audio clip to play when this entry is activated.")]
        public AudioClip audioClip; // Individual audio clips for each entry
    }

    [SerializeField]
    private List<RollingTextEntry> rollingTextEntries;

    [SerializeField]
    private TextMeshProUGUI finalText;

    [SerializeField]
    private VoiceOverData finalVoiceOverData;

    [SerializeField, Tooltip("Audio clip to play when the final voice-over is activated.")]
    private AudioClip finalAudioClip; // New field for the final audio clip

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
            ResetLaunchSequence();
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
        float totalDuration = CalculateTotalVoiceOverDuration();

        if (totalDuration <= 0f)
        {
            Debug.LogError("Total Voice-Over Duration is zero or negative. Slider cannot be updated.");
            isPlayingVoiceOver = false;
            yield break;
        }

        float elapsedTime = 0f;

        while (currentTextIndex < rollingTextEntries.Count)
        {
            if (!holdButtonComponent.isHolding)
            {
                isPlayingVoiceOver = false;
                yield break;
            }

            UpdateRollingTextDisplay();
            vibrationController?.VibratePhone_Medium();

            // Play the individual audio clip for the current entry
            AudioClip currentAudioClip = rollingTextEntries[currentTextIndex].audioClip;
            if (currentAudioClip != null)
            {
                GlobalAudioPlayer.GetPlaySound(currentAudioClip);
            }
            else
            {
                Debug.LogWarning($"No audio clip assigned for RollingTextEntry at index {currentTextIndex}.");
            }

            VoiceOverManager.Instance.TriggerVoiceOver(rollingTextEntries[currentTextIndex].voiceOverData);

            float clipLength = GetVoiceOverClipLength(rollingTextEntries[currentTextIndex].voiceOverData);
            float clipTime = 0f;

            while (clipTime < clipLength)
            {
                if (!holdButtonComponent.isHolding)
                {
                    isPlayingVoiceOver = false;
                    yield break;
                }

                clipTime += Time.deltaTime;
                elapsedTime += Time.deltaTime;

                preCheckSlider.value = Mathf.Clamp01(elapsedTime / totalDuration);
                yield return null;
            }

            currentTextIndex++;
        }

        OnLaunchReady();
        isPlayingVoiceOver = false;
    }

    private float CalculateTotalVoiceOverDuration()
    {
        float totalDuration = 0f;
        foreach (var entry in rollingTextEntries)
        {
            totalDuration += GetVoiceOverClipLength(entry.voiceOverData);
        }
        return totalDuration;
    }

    private float GetVoiceOverClipLength(VoiceOverData voiceOverData)
    {
        AudioClip clipToPlay = LanguageManager.Instance.useHindiAudio && voiceOverData.hindiVoiceOverClip != null
            ? voiceOverData.hindiVoiceOverClip
            : voiceOverData.englishVoiceOverClip;

        return clipToPlay != null ? clipToPlay.length : 0f;
    }

    private void OnLaunchReady()
    {
        Debug.Log("Launch Ready!");

        vfxManager.smokeVFX_PSLV.Play();
        HideAllTexts();
        finalText.gameObject.SetActive(true);
        SetTextAlpha(finalText, 1f);
        SetTextSize(finalText, bigFontSize);
        SetTextPosition(finalText, centerPosition);

        launchReady = true;
        preCheckSlider.value = 1f;

        preCheckSlider.gameObject.SetActive(false);
        holdButton.gameObject.SetActive(false);
        launchButton.gameObject.SetActive(true);

        if (finalVoiceOverData != null)
        {
            StartCoroutine(PlayFinalVoiceOver());
        }
    }

    private IEnumerator PlayFinalVoiceOver()
    {
        isFinalVoiceOverPlaying = true;

        // Trigger vibration
        vibrationController?.VibratePhone_Medium();

        // Play the final audio clip using GlobalAudioPlayer
        if (finalAudioClip != null)
        {
            GlobalAudioPlayer.GetPlaySound(finalAudioClip);
        }
        else
        {
            Debug.LogWarning("No final audio clip assigned.");
        }

        // Trigger the final voice-over
        VoiceOverManager.Instance.TriggerVoiceOver(finalVoiceOverData);

        // Get the length of the voice-over clip to wait for its completion
        float clipLength = GetVoiceOverClipLength(finalVoiceOverData);
        yield return new WaitForSeconds(clipLength);

        isFinalVoiceOverPlaying = false;
    }

    private void UpdateRollingTextDisplay()
    {
        HideAllTexts();

        int middleIndex = currentTextIndex % rollingTextEntries.Count;
        int topIndex = (currentTextIndex - 1 + rollingTextEntries.Count) % rollingTextEntries.Count;
        int bottomIndex = (currentTextIndex + 1) % rollingTextEntries.Count;

        SetTextProperties(rollingTextEntries[middleIndex].text, bigFontSize, 1f, centerPosition);
        SetTextProperties(rollingTextEntries[topIndex].text, smallFontSize, 30f / 255f, topPosition);
        SetTextProperties(rollingTextEntries[bottomIndex].text, smallFontSize, 30f / 255f, bottomPosition);
    }

    private void SetTextProperties(TextMeshProUGUI text, float size, float alpha, Vector3 position)
    {
        text.gameObject.SetActive(true);
        SetTextAlpha(text, alpha);
        SetTextSize(text, size);
        SetTextPosition(text, position);
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

    private void ResetLaunchSequence()
    {
        isPlayingVoiceOver = false;
        isFinalVoiceOverPlaying = false;
        currentTextIndex = 0;
        preCheckSlider.value = 0f;
        HideAllTexts();
        finalText.gameObject.SetActive(false);
        preCheckSlider.gameObject.SetActive(true);
        holdButton.gameObject.SetActive(true);
        launchButton.gameObject.SetActive(false);
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
