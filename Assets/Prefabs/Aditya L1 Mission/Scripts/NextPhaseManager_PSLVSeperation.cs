using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhaseManager_PSLVSeperation : MonoBehaviour
{
    [SerializeField, Tooltip("Auto Assigned")]
    private PlaceOnPlane placeOnPlane;

    [SerializeField, Tooltip("Auto Assigned")]
    private FadeHandler fadeHandler;
    [Space]
    [SerializeField, Tooltip("Assign the next Phase 3 Prefab i.e. Earth Orbit Escape Phase")]
    private GameObject OrbitPSLV_PHase3;

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
            GlobalUIProvider_AdityaL1.getNextPhaseButton().onClick.AddListener(HandleNextPhase);
        }
        else { Debug.Log("Next Phase Button is not Assigned"); }
    }

    public void HandleNextPhase()
    {
        placeOnPlane.IsPhase2Finished = true;

        fadeHandler.FadeIn();

        var userTapCordinates = placeOnPlane.GetMoonSurfacePosition();

        // Calculate the direction from the object to the camera
        Vector3 directionToCamera = Camera.main.transform.position - userTapCordinates;

        // Zero out the Y component to only consider horizontal rotation
        directionToCamera.y = 0;

        // If the direction vector is zero, default to forward
        if (directionToCamera == Vector3.zero)
        {
            directionToCamera = Vector3.forward;
        }

        // Calculate the rotation towards the camera
        Quaternion rotation = Quaternion.LookRotation(directionToCamera);

        // Instantiate the object with the calculated rotation
        Instantiate(OrbitPSLV_PHase3, userTapCordinates, rotation);
    }

    private void OnDestroy()
    {
        if(GlobalUIProvider_AdityaL1.getNextPhaseButton() != null)
        {
            GlobalUIProvider_AdityaL1.getNextPhaseButton().onClick.RemoveListener(HandleNextPhase);
        }
    }
}
