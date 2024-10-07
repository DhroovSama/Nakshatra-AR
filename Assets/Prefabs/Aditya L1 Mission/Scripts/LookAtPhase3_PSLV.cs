using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPhase3_PSLV : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowGameObject;

    [SerializeField]
    private VibrationController vibrationController;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private bool performRaycast = true;

    [SerializeField]
    private bool Phase3Start = false;

    [SerializeField, Tooltip("Auto Assigned")]
    private GameObject targetPhase3Object;

    private void Start()
    {
        mainCamera = Camera.main;
        arrowGameObject.SetActive(false);
    }

    private void Update()
    {
        if (PlaceOnPlane.IsPhase2Finished_PSLV())
        {
            if (targetPhase3Object == null)
            {
                // Automatically find the target object if not assigned
                targetPhase3Object = GameObject.FindWithTag("Phase3");
            }

            if (performRaycast)
            {
                arrowGameObject.SetActive(true);
                PerformRaycast();
            }
        }

        if (targetPhase3Object != null)
        {
            LookAtPhase3_AdityaL1();
        }
    }

    private void LookAtPhase3_AdityaL1()
    {
        // Make the arrow point towards the Phase3 object
        arrowGameObject.transform.LookAt(targetPhase3Object.transform);
    }

    private void PerformRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Phase3"))
            {
                // Trigger vibration and proceed with the Phase3 logic
                vibrationController.VibratePhone_Heavy();

                // Disable the arrow and raycast
                arrowGameObject.SetActive(false);
                performRaycast = false;

                Phase3Start = false;
            }
        }
    }

    // Function to start Phase 3 interactions
    public void StartPhase3()
    {
        Phase3Start = true;
        performRaycast = true;
    }
}
