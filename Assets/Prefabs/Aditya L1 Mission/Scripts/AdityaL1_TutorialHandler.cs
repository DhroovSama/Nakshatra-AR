using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdityaL1_TutorialHandler : MonoBehaviour
{
    [SerializeField]
    private GlobalUIProvider_AdityaL1 globalUIProvider_AdityaL1;

    [Space]

    [SerializeField]
    private RawImage blurBgIMG, holdButtonHandTapUI, nextStageHandTapUI, orbitShiftHandTapUI, L1HandTapUI;
    [Space]
    [SerializeField]
    private GameObject welcomeBox, holdButtonTutorial, SeperationPhaseUI, nextStageButtonTutorial, L1Phase4Tutorial;

    [Space] 
    [SerializeField]
    private Button holdbutton, nextStageButton_SeperationStage, orbitShiftButton, observationButton;

    private bool isWelcomeBoxDisplayed = false;

    #region OscillationSettings
    [Header("Oscillation Settings")]
    [SerializeField, Tooltip("Frequency of oscillation in Hertz (cycles per second).")]
    private float oscillationFrequency = 2f;

    [SerializeField, Tooltip("Amplitude of oscillation in units.")]
    private float oscillationAmplitude = 10f;

    [Header("Color Lerp Settings")]
    [SerializeField, Tooltip("Rate at which the color lerps from white to green.")]
    private float colorLerpRate = 1f;
    #endregion

    private Coroutine enableWelcomeBoxCoroutine;

    private void OnEnable()
    {
        holdbutton.onClick.AddListener(DisableHoldButtonUI);
        nextStageButton_SeperationStage.onClick.AddListener(DisableNextStageHandTapUI);
        orbitShiftButton.onClick.AddListener(DisableOrbitShfitHandTapUI);
        observationButton.onClick.AddListener(DisableObservationHandTapUI);
    }

    private void Start()
    {
        //nextStageButton_SeperationStage.interactable = false;
        holdbutton.interactable = false;
        enableWelcomeBoxCoroutine = StartCoroutine(EnableWelcomeBox());
    }

    private IEnumerator EnableWelcomeBox()
    {
        yield return new WaitUntil(() => PlaceOnPlane.IsMoonSurfaceSpawned() && !isWelcomeBoxDisplayed);

        blurBgIMG.gameObject.SetActive(true);
        welcomeBox.SetActive(true);

        isWelcomeBoxDisplayed = true;
    }

    public void StartPSLVUserTapUIHighlight()
    {
        if (enableWelcomeBoxCoroutine != null)
        {
            StopCoroutine(enableWelcomeBoxCoroutine);
            enableWelcomeBoxCoroutine = null;
        }

        blurBgIMG.gameObject.SetActive(false);
        welcomeBox.SetActive(false);

        StartCoroutine(HandUIMoveAndHighlight_PSLVUserTapper());
    }

    private IEnumerator HandUIMoveAndHighlight_PSLVUserTapper()
    {
        while (!PlaceOnPlane.IsPhase1Finished_PSLV())
        {
            var PSLV_RevealButton = globalUIProvider_AdityaL1.UserTap;

            if (PSLV_RevealButton != null)
            {
                var revealButtonIMG = PSLV_RevealButton.GetComponentInChildren<RawImage>();
                if (revealButtonIMG != null)
                {
                    float lerpFactor = (Mathf.Sin(Time.time * colorLerpRate * 2 * Mathf.PI) + 1f) / 2f; 

                    revealButtonIMG.color = Color.Lerp(Color.white, Color.green, lerpFactor);

                    yield return null;
                }
                else
                {
                    Debug.Log("revealButtonIMG is NULL");
                    yield return null; 
                }
            }
            else
            {
                Debug.Log("PSLV_RevealButton is NULL");
                yield return null; 
            }
        }
        yield break;
    }

    private IEnumerator HighlightHoldButtonUI()
    {
        while (true)
        {
            float lerpFactor = (Mathf.Sin(Time.time * colorLerpRate * 2 * Mathf.PI) + 1f) / 2f; // Normalizes between 0 and 1

            // Lerp the color from white to green based on the lerp factor
            holdButtonHandTapUI.color = Color.Lerp(Color.white, Color.green, lerpFactor);

            // Wait for the next frame
            yield return null;
        }
        
    }

    private IEnumerator HighlightNextStageHandTapButtonUI()
    {
        while (true)
        {
            // Calculate the lerp factor using a sine wave to smoothly transition between colors
            float lerpFactor = (Mathf.Sin(Time.time * colorLerpRate * 2 * Mathf.PI) + 1f) / 2f; // Normalizes between 0 and 1

            // Lerp the color from white to green based on the lerp factor
            nextStageHandTapUI.color = Color.Lerp(Color.white, Color.green, lerpFactor);

            // Wait for the next frame
            yield return null;
        }
    }
    private IEnumerator HighlightOrbutShiftStageHandTapButtonUI()
    {
        while (true)
        {
            // Calculate the lerp factor using a sine wave to smoothly transition between colors
            float lerpFactor = (Mathf.Sin(Time.time * colorLerpRate * 2 * Mathf.PI) + 1f) / 2f; // Normalizes between 0 and 1

            // Lerp the color from white to green based on the lerp factor
            orbitShiftHandTapUI.color = Color.Lerp(Color.white, Color.green, lerpFactor);

            // Wait for the next frame
            yield return null;
        }
    }
    private IEnumerator HighlightL1ObservationHandTapButtonUI()
    {
        while (true)
        {
            // Calculate the lerp factor using a sine wave to smoothly transition between colors
            float lerpFactor = (Mathf.Sin(Time.time * colorLerpRate * 2 * Mathf.PI) + 1f) / 2f; // Normalizes between 0 and 1

            // Lerp the color from white to green based on the lerp factor
            L1HandTapUI.color = Color.Lerp(Color.white, Color.green, lerpFactor);

            // Wait for the next frame
            yield return null;
        }
    }
    public void StartHighlightL1ObservationHandTapButtonUI()
    {
        StartCoroutine(HighlightL1ObservationHandTapButtonUI());
    } 

    public void StartHighlightOrbutShiftStageHandTapButtonUI()
    {
        StartCoroutine(HighlightOrbutShiftStageHandTapButtonUI());
    }


    public void EnableHoldButtonTutorial()
    {
        holdButtonHandTapUI.gameObject.SetActive(true);

        EnableUserToUseHoldButton();
    }

    public void EnableUserToUseHoldButton()
    {
        holdbutton.interactable = true;

        StartCoroutine(HighlightHoldButtonUI());
    }

    private void DisableHoldButtonUI()
    {
        StopCoroutine(HighlightHoldButtonUI());
        holdButtonHandTapUI.gameObject.SetActive(false);
    }

    public void EnableSeperationStageTutorial()
    {
        blurBgIMG.gameObject.SetActive(true);
        nextStageButtonTutorial.SetActive(true);
        nextStageHandTapUI.gameObject.SetActive(true);  
        //nextStageButton_SeperationStage.interactable = true;

    }

    public void StartHighlightNextStageHandTapButtonUI()
    {
        blurBgIMG.gameObject.SetActive(false);
        nextStageButtonTutorial.SetActive(false);
        StartCoroutine(HighlightNextStageHandTapButtonUI());
    }

    private void DisableNextStageHandTapUI()
    {
        nextStageHandTapUI.gameObject.SetActive(false);
    }
    private void DisableOrbitShfitHandTapUI()
    {
        orbitShiftHandTapUI.gameObject.SetActive(false);
    }

    private void DisableObservationHandTapUI()
    {
        L1HandTapUI.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        holdbutton.onClick.RemoveListener(DisableHoldButtonUI);
        nextStageButton_SeperationStage.onClick.RemoveListener(DisableNextStageHandTapUI);
        orbitShiftButton.onClick.RemoveListener(DisableOrbitShfitHandTapUI);
        observationButton.onClick.RemoveListener(DisableObservationHandTapUI);
    }
}
