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
    private GameObject OrbitPSLV_Phase3;

    [Space]
    [SerializeField]
    private GameObject Phase2;

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
        Instantiate(OrbitPSLV_Phase3, userTapCordinates, rotation);
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
