using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RocketPhase
{
    public string phaseName;           // For reference
    public string animatorTrigger;     // Animator trigger name
    public GameObject phaseGameObject; // Associated GameObject
    // public AnimationClip animationClip; // Optional: Reference to the AnimationClip    
}

public class RocketSeperationStages_Manager : MonoBehaviour
{
    [SerializeField, Tooltip("Auto Assigned")]
    private SeperationPhaseValues_PSLV seperationPhaseValues_PSLV;

    [Header("Animation")]
    [Space]
    [SerializeField, Tooltip("Auto Assigned")]
    private Animator pslvSeperationAnimator;

    [SerializeField]
    private List<RocketPhase> rocketPhases; // List of phases

    [Header("UI Elements")]
    [SerializeField]
    private Button nextButton;
    // Removed the back button
    // [SerializeField]
    // private Button backButton;

    private int currentPhaseIndex = -1; // Start before the first phase

    [SerializeField, Range(1f, 15f), Tooltip("in how much time the next phase button will appear after Phase 5 Enables")]
    private float nextPhaseButtonTime = 0f;

    private void Start()
    {
        // Initialize the next button
        nextButton.interactable = rocketPhases.Count > 0;

        // Deactivate all phase GameObjects at the start
        foreach (var phase in rocketPhases)
        {
            phase.phaseGameObject.SetActive(false);
        }

        // Subscribe to the next button click event
        nextButton.onClick.AddListener(NextPhase);
        // Removed the back button listener
        // backButton.onClick.AddListener(PreviousPhase);
    }

    private void Update()
    {
        if (PlaceOnPlane.IsPhase1Finished_PSLV())
        {
            if (seperationPhaseValues_PSLV == null)
            {
                seperationPhaseValues_PSLV = FindObjectOfType<SeperationPhaseValues_PSLV>();
            }

            if (pslvSeperationAnimator == null)
            {
                var pslvSeperationObject = GameObject.FindWithTag("PSLV_Seperation");
                if (pslvSeperationObject != null)
                {
                    pslvSeperationAnimator = pslvSeperationObject.GetComponent<Animator>();
                }
            }
        }
    }

    private void NextPhase()
    {
        if (currentPhaseIndex < rocketPhases.Count - 1)
        {
            currentPhaseIndex++;
            UpdatePhase();
        }
        else
        {
            // Optionally, disable the next button when all phases are completed
            nextButton.interactable = false;
            Debug.Log("All phases completed.");
        }
    }

    // Removed the PreviousPhase method
    /*
    private void PreviousPhase()
    {
        if (currentPhaseIndex > 0)
        {
            currentPhaseIndex--;
            UpdatePhase();
        }
        else if (currentPhaseIndex == 0)
        {
            ResetToDefault();
        }
    }
    */

    private void UpdatePhase()
    {
        // Deactivate all phase GameObjects
        foreach (var phase in rocketPhases)
        {
            phase.phaseGameObject.SetActive(false);
        }

        if (currentPhaseIndex >= 0 && currentPhaseIndex < rocketPhases.Count)
        {
            // Activate the current phase GameObject
            var currentPhase = rocketPhases[currentPhaseIndex];
            currentPhase.phaseGameObject.SetActive(true);

            // Trigger the animator
            pslvSeperationAnimator.ResetTrigger("StartPhase_Default"); // Reset default trigger if needed
            pslvSeperationAnimator.SetTrigger(currentPhase.animatorTrigger);

            // Update the next button interactability
            nextButton.interactable = currentPhaseIndex < rocketPhases.Count - 1;

            // Optional: Start the phase values updates
            /*
            if (currentPhaseIndex == 0) // Assuming phase 1 starts the mission time
            {
                StartSeperationPhase();
            }
            */

            Debug.Log($"Phase {currentPhase.phaseName} activated.");

            if(currentPhase.phaseName == "(5) Satellite Seperation")
            {
                StartCoroutine(EnableNextPhaseButtonTimer());
            }
        }
    }

    private IEnumerator EnableNextPhaseButtonTimer()
    {
        yield return new WaitForSeconds(nextPhaseButtonTime);

        GlobalUIProvider_AdityaL1.getNextPhaseButton().gameObject.SetActive(true);
    }

    public void StartSeperationPhase()
    {
        Debug.Log($"Phase {currentPhaseIndex + 1} Started");

        seperationPhaseValues_PSLV.MissionTimeStart();
        seperationPhaseValues_PSLV.StartVelocityUpdate();
        seperationPhaseValues_PSLV.StartAltitudeUpdate();
        seperationPhaseValues_PSLV.StartRangeUpdate();
        seperationPhaseValues_PSLV.StartAzimuthUpdate();
    }

    private void ResetToDefault()
    {
        currentPhaseIndex = -1;

        // Deactivate all phase GameObjects
        foreach (var phase in rocketPhases)
        {
            phase.phaseGameObject.SetActive(false);
        }

        pslvSeperationAnimator.SetTrigger("StartPhase_Default");

        // Update the next button interactability
        nextButton.interactable = rocketPhases.Count > 0;

        Debug.Log("Reset to default state.");
    }
}
