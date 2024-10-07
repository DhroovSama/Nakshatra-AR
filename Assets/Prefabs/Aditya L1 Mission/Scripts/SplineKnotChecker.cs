using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineAnimate))]
public class SplineKnotChecker : MonoBehaviour
{
    [SerializeField]
    private NextPhaseManager_PSLVOrbitShift nextPhaseManager_PSLVOrbitShift;

    [SerializeField]
    private List<SplineContainer> splines = new List<SplineContainer>();

    [SerializeField, Range(0.001f, 0.015f), Tooltip("Threshold distance to consider the object 'at' a knot")]
    private float thresholdDistance = 0.1f;

    [SerializeField]
    private List<int> targetKnotIndices = new List<int>();

    private SplineAnimate splineAnimate;
    private int currentSplineIndex = 0;
    private bool hasCrossedKnot = false;
    private bool finalActionTriggered = false; // Flag to ensure TriggerFinalAction is called only once

    [SerializeField]
    private bool hasLoggedNow = false;

    private bool canPressButton = false;

    // Variables for duration control
    [SerializeField]
    private float initialDuration = 10.0f; // Adjust as needed (starting duration)

    [SerializeField]
    private float durationDecrement = 2.0f; // Adjust as needed (amount to decrease after each spline)

    private float currentDuration;

    private void Start()
    {
        ChangeButtonNormalColor(Color.white);

        splineAnimate = GetComponent<SplineAnimate>();

        // Subscribe to the button's onClick event
        GlobalUIProvider_AdityaL1.getOrbitShiftButton().onClick.AddListener(OnButtonPressed);

        if (splines == null || splines.Count == 0)
        {
            Debug.LogWarning("No splines assigned!");
            return;
        }

        if (splineAnimate == null)
        {
            Debug.LogWarning("SplineAnimate component is missing!");
            return;
        }

        ValidateTargetKnotIndices();

        // Initialize current duration
        currentDuration = initialDuration;
        splineAnimate.Duration = currentDuration;

        // Set the initial spline to the first one in the list
        SetSpline(splines[currentSplineIndex]);
    }

    private void ValidateTargetKnotIndices()
    {
        // Ensure targetKnotIndices has the same count as splines
        while (targetKnotIndices.Count < splines.Count)
        {
            targetKnotIndices.Add(0); // Default to 0 if not enough indices provided
        }

        // Validate each target knot index
        for (int i = 0; i < splines.Count; i++)
        {
            if (targetKnotIndices[i] < 0 || targetKnotIndices[i] >= splines[i].Spline.Count)
            {
                Debug.LogWarning($"Invalid knot index for spline {i}. Setting to 0.");
                targetKnotIndices[i] = 0;
            }
        }
    }

    private void Update()
    {
        if (splines.Count == 0 || splineAnimate == null)
            return;

        if (hasCrossedKnot && currentSplineIndex < splines.Count - 1)
            return;

        // Get the current spline and knot index
        var currentSpline = splines[currentSplineIndex];
        int targetKnotIndex = targetKnotIndices[currentSplineIndex];

        // Get the position of the target knot in world space
        Vector3 knotPosition = currentSpline.transform.TransformPoint(currentSpline.Spline[targetKnotIndex].Position);

        // Get the current position of the animated object
        Vector3 objectPosition = transform.position;

        // Calculate the distance between the object and the knot
        float distance = Vector3.Distance(objectPosition, knotPosition);

        // Check if the object is within the threshold distance of the knot
        if (distance <= thresholdDistance)
        {
            if (!hasLoggedNow)
            {
                hasLoggedNow = true; // Set the flag to prevent multiple logs
                ChangeButtonNormalColor(Color.green);
                Debug.Log("now");
            }

            canPressButton = true; // Allow the button to be pressed

            // Check if we're on the last spline and trigger the final action if not already triggered
            if (currentSplineIndex == splines.Count - 1 && !finalActionTriggered)
            {
                finalActionTriggered = true; // Set the flag to ensure this action is triggered only once
                TriggerFinalAction();
            }
        }
        else
        {
            hasLoggedNow = false; // Reset the log flag when the object moves away
            canPressButton = false; // Disable button press when not near the knot
            ChangeButtonNormalColor(Color.red);
        }
    }

    // Public method to be called when the button is clicked
    public void OnButtonPressed()
    {
        if (canPressButton && !hasCrossedKnot)
        {
            hasCrossedKnot = true; // Prevent multiple triggers for this knot
            OnCrossedKnot();
            hasLoggedNow = false; // Reset the log flag for the next knot
            canPressButton = false; // Reset the button press flag
        }
        else
        {
            Debug.Log("Button pressed at the wrong time.");
        }
    }

    private void OnCrossedKnot()
    {
        Debug.Log($"Object has crossed knot {targetKnotIndices[currentSplineIndex]} on spline {currentSplineIndex}.");

        // Check if we are on the last spline
        if (currentSplineIndex < splines.Count - 1)
        {
            // Decrease the duration to increase speed
            currentDuration -= durationDecrement;
            if (currentDuration < 0.1f) // Ensure duration doesn't become negative or too small
            {
                currentDuration = 0.1f;
            }

            currentSplineIndex++;
            SetSpline(splines[currentSplineIndex]);
            hasCrossedKnot = false; // Reset for the next spline
        }
        else
        {
            // This is the last spline, but action is handled by the Update method to ensure it's only triggered once
            Debug.Log("Reached the target point on the last spline.");
        }
    }

    private void SetSpline(SplineContainer splineContainer)
    {
        if (splineContainer != null)
        {
            splineAnimate.Container = splineContainer;
            splineAnimate.Duration = currentDuration; // Set the new duration
            splineAnimate.Restart(true); // Restart the animation on the new spline
            Debug.Log($"Switched to spline {currentSplineIndex} with duration {currentDuration}");
        }
    }

    private void ChangeButtonNormalColor(Color newColor)
    {
        // Get the button from the GlobalUIProvider
        var button = GlobalUIProvider_AdityaL1.getOrbitShiftButton();

        // Get the current color block of the button
        var colorBlock = button.colors;

        // Change the colors to the new color
        colorBlock.normalColor = newColor;
        colorBlock.highlightedColor = newColor;
        colorBlock.pressedColor = newColor;
        colorBlock.selectedColor = newColor;

        // Reassign the updated color block back to the button
        button.colors = colorBlock;
    }

    private void OnDrawGizmos()
    {
        if (splines == null || splines.Count == 0)
            return;

        for (int i = 0; i < splines.Count; i++)
        {
            var spline = splines[i];
            int knotIndex = (i < targetKnotIndices.Count) ? targetKnotIndices[i] : 0;

            if (spline != null && spline.Spline.Count > knotIndex)
            {
                // Get the position of the knot in world space
                Vector3 knotPos = spline.transform.TransformPoint(spline.Spline[knotIndex].Position);

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(knotPos, thresholdDistance);
            }
        }
    }

    private void TriggerFinalAction()
    {
        GlobalUIProvider_AdityaL1.getNextPhaseButton().gameObject.SetActive(true);
        GlobalUIProvider_AdityaL1.getNextPhaseButton().onClick.AddListener(nextPhaseManager_PSLVOrbitShift.StartHandleNextPhase);
    }

    private void OnDestroy()
    {
        GlobalUIProvider_AdityaL1.getOrbitShiftButton().onClick.RemoveListener(OnButtonPressed);
        GlobalUIProvider_AdityaL1.getNextPhaseButton().onClick.RemoveListener(nextPhaseManager_PSLVOrbitShift.StartHandleNextPhase);
    }
}
