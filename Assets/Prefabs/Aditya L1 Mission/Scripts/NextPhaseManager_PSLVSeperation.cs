using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class NextPhaseManager_PSLVSeperation : MonoBehaviour
{
    [SerializeField, Tooltip("Auto Assigned")]
    private PlaceOnPlane placeOnPlane;

    [SerializeField, Tooltip("Auto Assigned")]
    private FadeHandler fadeHandler;
    [Space]
    [SerializeField, Tooltip("Assign the next Phase 3 Prefab i.e. Earth Orbit Escape Phase")]
    private GameObject OrbitPSLV_Phase3;

    [Space]
    [SerializeField]
    private GameObject Phase2;

    [SerializeField]
    private VoiceOverData phase3OrbitShiftStageVO;

    private void Start()
    {
        if (placeOnPlane == null)
        {
            placeOnPlane = FindObjectOfType<PlaceOnPlane>();
        }

        if (fadeHandler == null)
        {
            fadeHandler = FindObjectOfType<FadeHandler>();
        }

        if(GlobalUIProvider_AdityaL1.getNextPhaseButton() != null)
        {
            GlobalUIProvider_AdityaL1.getNextPhaseButton().onClick.AddListener(StartHandleNextPhase);
        }
        else { Debug.Log("Next Phase Button is not Assigned"); }
    }

    private void StartHandleNextPhase()
    {
        StartCoroutine(HandleNextPhase());
    }

    public IEnumerator HandleNextPhase()
    {
        placeOnPlane.IsPhase2Finished = true;

        fadeHandler.FadeIn();

        yield return new WaitForSeconds(2f);

        DestroyPhase2();

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
        Instantiate(OrbitPSLV_Phase3, userTapCoordinates, finalRotation);

        GlobalUIProvider_AdityaL1.getOrbitShiftPhaseTutorial().SetActive(true);
        VoiceOverManager.Instance.TriggerVoiceOver(phase3OrbitShiftStageVO);

        GlobalUIProvider_AdityaL1.getBlurBG().SetActive(true);
    }



    private void DestroyPhase2()
    {
        Phase2 = GameObject.FindWithTag("Phase2");

        Destroy(Phase2);

        GlobalUIProvider_AdityaL1.getNextPhaseButton().gameObject.SetActive(false);

        GlobalUIProvider_AdityaL1.getSeperationPhaseUI().gameObject.SetActive(false);

        fadeHandler.FadeOut();
    }

    private void OnDestroy()
    {
        if(GlobalUIProvider_AdityaL1.getNextPhaseButton() != null)
        {
            GlobalUIProvider_AdityaL1.getNextPhaseButton().onClick.RemoveListener(StartHandleNextPhase);
        }
    }
}
