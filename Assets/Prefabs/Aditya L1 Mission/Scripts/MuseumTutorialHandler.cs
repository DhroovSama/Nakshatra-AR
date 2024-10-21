using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuseumTutorialHandler : MonoBehaviour
{
    [Header("Tutorial Elements")]
    [SerializeField]
    private GameObject tutorialsContainer;

    [SerializeField]
    private GameObject blurLayer;

    [SerializeField]
    private Button PlaceButton, LockObjectButton;

    [SerializeField]
    private Scrollbar ObjectScroll;

    [SerializeField]
    private GameObject tutorialBox_1, tutorialBox_2, tutorialBox_3, tutorialBox_4, tutorialBox_5, tutorialBox_6, tutorialBox_7;

    [Header("Hand Tap UI")]
    [SerializeField]
    private RawImage handTapIMG_1, handTapIMG_2, handTapIMG_3, handPinchIMG, handSwipeIMG, handToggleIMG;

    [Header("Oscillation Settings")]
    [SerializeField, Tooltip("Frequency of oscillation in Hertz (cycles per second).")]
    private float oscillationFrequency = 2f;

    [SerializeField, Tooltip("Amplitude of oscillation in units.")]
    private float oscillationAmplitude = 10f;

    [Header("Color Lerp Settings")]
    [SerializeField, Tooltip("Rate at which the color lerps from white to green.")]
    private float colorLerpRate = 1f;

    private bool isTutorialSpawned = false;
    private Coroutine moveAndHighlightCoroutine;
    private Vector3 originalHandTapPosition;

    [SerializeField]
    private UISoundSO uiSoundSO;

    [SerializeField]
    private VibrationController vibrationController;

    [SerializeField]
    private VoiceOverData tutorial1vo, tutorial2vo, tutorial3vo, tutorial4vo, tutorial5vo, tutorial6vo;

    private void OnEnable()
    {
        PlaceButton.onClick.AddListener(EnableTutorial3);
        LockObjectButton.onClick.AddListener(EnableTutorial6);
        ObjectScroll.onValueChanged.AddListener(OnScrollbarValueChanged); // Added listener to the scrollbar
    }

    private void Start()
    {
        ObjectScroll.interactable = false;
        PlaceButton.interactable = false;
        LockObjectButton.interactable = false;

        originalHandTapPosition = handTapIMG_1.rectTransform.localPosition;

        StartCoroutine(WaitForMoonSurfaceSpawn());
    }

    private IEnumerator WaitForMoonSurfaceSpawn()
    {
        yield return new WaitUntil(() => PlaceOnPlane.IsMoonSurfaceSpawned());

        if (!isTutorialSpawned)
        {
            vibrationController.VibratePhone_Light();
            uiSoundSO.PlayTapSound();
            VoiceOverManager.Instance.TriggerVoiceOver(tutorial1vo);

            blurLayer.SetActive(true);
            ObjectSpawnedTutorial();
            isTutorialSpawned = true;
        }
    }

    private void ObjectSpawnedTutorial()
    {
        tutorialBox_1.SetActive(true);
        handTapIMG_1.gameObject.SetActive(true);
        MoveAndHighlightGHandTapUI();
    }

    private void MoveAndHighlightGHandTapUI()
    {
        if (moveAndHighlightCoroutine == null)
        {
            moveAndHighlightCoroutine = StartCoroutine(HandUIMoveAndHighlight_1());
        }
    }

    private IEnumerator HandUIMoveAndHighlight_1()
    {
        while (true)
        {
            // Calculate the oscillation offset using a sine wave
            float oscillationOffset = Mathf.Sin(Time.time * oscillationFrequency * 2 * Mathf.PI) * oscillationAmplitude;

            // Update the Y position of the handTapIMG
            Vector3 newPosition = originalHandTapPosition + new Vector3(0, oscillationOffset, 0);
            handTapIMG_1.rectTransform.localPosition = newPosition;

            // Calculate the lerp factor using a sine wave to smoothly transition between colors
            float lerpFactor = (Mathf.Sin(Time.time * colorLerpRate * 2 * Mathf.PI) + 1f) / 2f; // Normalizes between 0 and 1

            // Lerp the color from white to green based on the lerp factor
            handTapIMG_1.color = Color.Lerp(Color.white, Color.green, lerpFactor);

            // Wait for the next frame
            yield return null;
        }
    }

    private IEnumerator HandUIHighlight_2()
    {
        while (true)
        {
            float lerpFactor = (Mathf.Sin(Time.time * colorLerpRate * 2 * Mathf.PI) + 1f) / 2f;
            handTapIMG_2.color = Color.Lerp(Color.white, Color.green, lerpFactor);
            yield return null;
        }
    }

    private IEnumerator HandUIHighlight_3()
    {
        while (true)
        {
            float lerpFactor = (Mathf.Sin(Time.time * colorLerpRate * 2 * Mathf.PI) + 1f) / 2f;
            handPinchIMG.color = Color.Lerp(Color.white, Color.green, lerpFactor);
            yield return null;
        }
    }

    private IEnumerator HandSwipeUIMoveAndHighlight()
    {
        while (true)
        {
            float oscillationOffset = Mathf.Sin(Time.time * oscillationFrequency * 2 * Mathf.PI) * oscillationAmplitude;
            Vector3 newPosition = originalHandTapPosition + new Vector3(oscillationOffset, 0, 0);
            handSwipeIMG.rectTransform.localPosition = newPosition;

            float lerpFactor = (Mathf.Sin(Time.time * colorLerpRate * 2 * Mathf.PI) + 1f) / 2f;
            handSwipeIMG.color = Color.Lerp(Color.white, Color.green, lerpFactor);
            yield return null;
        }
    }

    private IEnumerator HandToggleUIMoveAndHighlight(float moveDistance, float moveSpeed)
    {
        Vector3 originalHandTapPosition = handToggleIMG.rectTransform.localPosition;

        while (true)
        {
            handToggleIMG.rectTransform.localPosition += new Vector3(moveSpeed * Time.deltaTime, 0, 0);

            if (Vector3.Distance(originalHandTapPosition, handToggleIMG.rectTransform.localPosition) >= moveDistance)
            {
                handToggleIMG.rectTransform.localPosition = originalHandTapPosition;
            }

            float lerpFactor = (Mathf.Sin(Time.time * colorLerpRate * 2 * Mathf.PI) + 1f) / 2f;
            handToggleIMG.color = Color.Lerp(Color.white, Color.green, lerpFactor);
            yield return null;
        }
    }

    private IEnumerator HandTapUIHighlight_4()
    {
        while (true)
        {
            float lerpFactor = (Mathf.Sin(Time.time * colorLerpRate * 2 * Mathf.PI) + 1f) / 2f;
            handTapIMG_3.color = Color.Lerp(Color.white, Color.green, lerpFactor);
            yield return null;
        }
    }

    public void AllowUserToMoveObject()
    {
        
        StartCoroutine(EnableTutorial2AfterSomeTime());
    }

    private IEnumerator EnableTutorial2AfterSomeTime()
    {
        blurLayer.SetActive(false);
        tutorialBox_1.SetActive(false);

        yield return new WaitForSeconds(3f);

        VoiceOverManager.Instance.TriggerVoiceOver(tutorial2vo);

        StopHandMovement_1();

        blurLayer.SetActive(true);
        tutorialBox_2.SetActive(true);
        handTapIMG_2.gameObject.SetActive(true);
    }

    private void StopHandMovement_1()
    {
        if (moveAndHighlightCoroutine != null)
        {
            StopCoroutine(moveAndHighlightCoroutine);
            moveAndHighlightCoroutine = null;

            handTapIMG_1.rectTransform.localPosition = originalHandTapPosition;
            handTapIMG_1.color = Color.white;
        }

        handTapIMG_1.gameObject.SetActive(false);
    }

    private void StopHandMovement_2()
    {
        if (moveAndHighlightCoroutine != null)
        {
            StopCoroutine(moveAndHighlightCoroutine);
            moveAndHighlightCoroutine = null;

            handTapIMG_2.color = Color.white;
        }

        handTapIMG_2.gameObject.SetActive(false);
    }

    private void StopHandPinchHighlight()
    {
        if (moveAndHighlightCoroutine != null)
        {
            StopCoroutine(moveAndHighlightCoroutine);
            moveAndHighlightCoroutine = null;

            handPinchIMG.color = Color.white;
        }

        handPinchIMG.gameObject.SetActive(false);
    }

    public void EnableUserToUsePlaceButton()
    {
        blurLayer.SetActive(false);
        tutorialBox_2.SetActive(false);
        StartCoroutine(HandUIHighlight_2());
    }

    private void EnableTutorial3()
    {
        StopHandMovement_2();
        VoiceOverManager.Instance.TriggerVoiceOver(tutorial3vo);
        blurLayer.SetActive(true);
        tutorialBox_3.SetActive(true);
        handPinchIMG.gameObject.SetActive(true);
    }

    public void EnableUserToPinch()
    {
        blurLayer.SetActive(false);
        tutorialBox_3.SetActive(false);
        StartCoroutine(HandUIHighlight_3());

        StartCoroutine(EnableTutorial4AfterSomeTime());
    }

    private IEnumerator EnableTutorial4AfterSomeTime()
    {
        yield return new WaitForSeconds(3f);

        VoiceOverManager.Instance.TriggerVoiceOver(tutorial4vo);
        StopHandPinchHighlight();
        handPinchIMG.gameObject.SetActive(false);
        blurLayer.SetActive(true);
        tutorialBox_4.SetActive(true);
        handSwipeIMG.gameObject.SetActive(true);
    }

    public void EnableUserToRoateobject()
    {
        blurLayer.SetActive(false);
        tutorialBox_4.SetActive(false);
        StartCoroutine(HandSwipeUIMoveAndHighlight());
        StartCoroutine(EnableTutorial5AfterSomeTime());
    }

    private IEnumerator EnableTutorial5AfterSomeTime()
    {
        yield return new WaitForSeconds(3f);

        VoiceOverManager.Instance.TriggerVoiceOver(tutorial5vo);
        StopCoroutine(HandSwipeUIMoveAndHighlight());
        handSwipeIMG.gameObject.SetActive(false);
        blurLayer.SetActive(true);
        handTapIMG_3.gameObject.SetActive(true);
        tutorialBox_5.SetActive(true);
    }

    public void EnableUserToUseLockButton()
    {
        StartCoroutine(HandTapUIHighlight_4());
        tutorialBox_5.SetActive(false);
        blurLayer.SetActive(false);
    }

    private void EnableTutorial6()
    {
        StopCoroutine(HandTapUIHighlight_4());
        handTapIMG_3.gameObject.SetActive(false);

        VoiceOverManager.Instance.TriggerVoiceOver(tutorial6vo);
        blurLayer.SetActive(true);
        tutorialBox_6.SetActive(true);
    }

    public void EnableTutorial7()
    {
        tutorialBox_6.SetActive(false);
        tutorialBox_7.SetActive(true);
        handToggleIMG.gameObject.SetActive(true);
    }

    public void EnableUserToToggleObject()
    {
        tutorialBox_7.SetActive(false);
        blurLayer.SetActive(false);

        StartCoroutine(HandToggleUIMoveAndHighlight(750, 450));
    }

    // Added method to handle scrollbar value change
    private void OnScrollbarValueChanged(float value)
    {
        if (Mathf.Approximately(value, 1f))
        {
            tutorialsContainer.SetActive(false);
            blurLayer.SetActive(false);
            handToggleIMG.gameObject.SetActive(false);
            //play sound and ai voice later
        }
    }

    private void OnDisable()
    {
        PlaceButton.onClick.RemoveListener(EnableTutorial3);
        LockObjectButton.onClick.RemoveListener(EnableTutorial6);
        ObjectScroll.onValueChanged.RemoveListener(OnScrollbarValueChanged); // Removed listener from scrollbar
    }
}
