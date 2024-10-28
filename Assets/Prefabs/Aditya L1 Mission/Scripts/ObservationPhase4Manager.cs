using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObservationPhase4Manager : MonoBehaviour
{
    [SerializeField, Tooltip("Auto Assigned")]
    private Button nextObservationButton;

    [SerializeField, Tooltip("Auto Assigned")]
    private TextMeshProUGUI observationCount;

    [Space]
    [SerializeField]
    private VoiceOverData observationVO_1, observationVO_2, observationVO_3, observationVO_4, observationVO_5;

    [SerializeField]
    private VoiceOverData endSimulationVo;

    private int currentObservationCount = 0;
    private const int maxObservations = 5;

    private void Awake()
    {
        nextObservationButton = GlobalUIProvider_AdityaL1.getnextObservationButton();
        observationCount = GlobalUIProvider_AdityaL1.getObservationCount();

        // Initialize the observation count text
        observationCount.text = $"{currentObservationCount}/{maxObservations} observations";

        // Add the button click listener
        nextObservationButton.onClick.AddListener(OnNextObservationButtonClicked);
    }

    private void OnNextObservationButtonClicked()
    {
        if (currentObservationCount >= maxObservations)
        {
            Debug.Log("Maximum observations reached.");
            nextObservationButton.interactable = false;
            
            return;
        }

        currentObservationCount++;

        observationCount.text = $"{currentObservationCount}/{maxObservations}";

        Debug.Log($"Observation count incremented to {currentObservationCount}");

        switch (currentObservationCount)
        {
            case 1:
                FirstObservation();
                break;
            case 2:
                SecondObservation();
                break;
            case 3:
                ThirdObservation();
                break;
            case 4:
                FourthObservation();
                break;
            case 5:
                FifthObservation();
                break;
            default:
                break;
        }
    }

    private void FirstObservation()
    {
        Debug.Log("FirstObservation method called.");

        VoiceOverManager.Instance.TriggerVoiceOver(observationVO_1);

        // Start the coroutine with the appropriate VoiceOverData
        StartCoroutine(EnableDisableNextObservationButton(observationVO_1));
    }

    private void SecondObservation()
    {
        Debug.Log("SecondObservation method called.");

        VoiceOverManager.Instance.TriggerVoiceOver(observationVO_2);

        StartCoroutine(EnableDisableNextObservationButton(observationVO_2));
    }

    private void ThirdObservation()
    {
        Debug.Log("ThirdObservation method called.");

        VoiceOverManager.Instance.TriggerVoiceOver(observationVO_3);

        StartCoroutine(EnableDisableNextObservationButton(observationVO_3));
    }

    private void FourthObservation()
    {
        Debug.Log("FourthObservation method called.");

        VoiceOverManager.Instance.TriggerVoiceOver(observationVO_4);

        StartCoroutine(EnableDisableNextObservationButton(observationVO_4));
    }

    private void FifthObservation()
    {
        Debug.Log("FifthObservation method called.");

        VoiceOverManager.Instance.TriggerVoiceOver(observationVO_5);

        StartCoroutine(EnableDisableNextObservationButton(observationVO_5));

        GlobalUIProvider_AdityaL1.getPhase4UI().SetActive(false);

        VoiceOverManager.Instance.TriggerVoiceOver(endSimulationVo);
        GlobalUIProvider_AdityaL1.getEndSimulationUI().SetActive(true);
    }

    private IEnumerator EnableDisableNextObservationButton(VoiceOverData voiceOverData)
    {
        nextObservationButton.gameObject.SetActive(false);

        AudioClip clipToPlay;
        if (LanguageManager.Instance.useHindiAudio && voiceOverData.hindiVoiceOverClip != null)
        {
            clipToPlay = voiceOverData.hindiVoiceOverClip;
        }
        else
        {
            clipToPlay = voiceOverData.englishVoiceOverClip;
        }

        if (clipToPlay != null)
        {
            yield return new WaitForSeconds(clipToPlay.length);
        }
        else
        {
            yield return new WaitForSeconds(5f);
        }

        nextObservationButton.gameObject.SetActive(true);
    }
}
