using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhaseManager_PSLVOrbitShift : MonoBehaviour
{
    [SerializeField, Tooltip("Auto Assigned")]
    private PlaceOnPlane placeOnPlane;

    [SerializeField, Tooltip("Auto Assigned")]

    private FadeHandler fadeHandler;
    [Space]
    [SerializeField, Tooltip("Assign the next Phase 4 Prefab i.e. Earth Orbit Escape Phase")]
    private GameObject OrbitPSLV_Phase4;

    [Space]
    private GameObject Phase3;

    private void Start()
    {
        GlobalUIProvider_AdityaL1.getNextPhaseButton().onClick.AddListener(StartHandleNextPhase);

        if (placeOnPlane == null)
        {
            placeOnPlane = FindObjectOfType<PlaceOnPlane>();
        }

        if (fadeHandler == null)
        {
            fadeHandler = FindObjectOfType<FadeHandler>();
        }
    }

    public void StartHandleNextPhase()
    {
        StartCoroutine(HandleNextPhase());
    }

    private IEnumerator HandleNextPhase()
    {
        placeOnPlane.IsPhase3Finished = true;

        //fadeHandler.FadeIn();

        yield return new WaitForSeconds(2f);

        DestroyPhase3();

        // Get the user-tapped coordinates and adjust the Y position
        var userTapCoordinates = placeOnPlane.GetMoonSurfacePosition();

        // Increase the Y position to elevate the object when instantiated
        userTapCoordinates.y += 1.0f; // Adjust this value as needed for the desired elevation

        // Calculate the direction from the object to the camera
        Vector3 directionToCamera = Camera.main.transform.position - userTapCoordinates;

        // Zero out the Y component to only consider horizontal rotation
        directionToCamera.y = 0;

        // If the direction vector is zero, default to forward
        if (directionToCamera == Vector3.zero)
        {
            directionToCamera = Vector3.forward;
        }

        // Calculate the rotation towards the camera
        Quaternion rotation = Quaternion.LookRotation(directionToCamera);

        // Apply an additional rotation of 90 degrees along the X-axis
        Quaternion additionalRotation = Quaternion.Euler(90, 0, 0);

        // Combine the rotations
        Quaternion finalRotation = rotation * additionalRotation;

        // Instantiate the object with the calculated rotation and adjusted position
        Instantiate(OrbitPSLV_Phase4, userTapCoordinates, finalRotation);

        //GlobalUIProvider_AdityaL1.getOrbitShiftPhaseTutorial().SetActive(true);

        GlobalUIProvider_AdityaL1.getNextPhaseButton().gameObject.SetActive(false);
    }

    private void DestroyPhase3()
    {
        Phase3 = GameObject.FindWithTag("Phase3");

        Destroy(Phase3);

        GlobalUIProvider_AdityaL1.getOrbitShiftButton().gameObject.SetActive(false);

        fadeHandler.FadeOut();
    }

    private void OnDestroy()
    {
        GlobalUIProvider_AdityaL1.getNextPhaseButton().onClick.RemoveListener(StartHandleNextPhase);
    }
}
