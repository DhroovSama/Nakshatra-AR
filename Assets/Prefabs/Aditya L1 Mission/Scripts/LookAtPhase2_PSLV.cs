using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPhase2_PSLV : MonoBehaviour
{
    [SerializeField]
    private RocketSeperationStages_Manager rocketSeperationStages_Manager;

    [SerializeField,Tooltip("Auto Assigned")] private GameObject seperationPhase_PSLV;
    [SerializeField] private GameObject arrowGameObject;

    [SerializeField]
    private VibrationController vibrationController;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private bool performRaycast = true;

    [SerializeField]
    private bool Phase2Start = false;

    private void Start()
    {
        mainCamera = Camera.main;
        arrowGameObject.SetActive(false); 
    }

    private void Update()
    {
        if (PlaceOnPlane.IsPhase1Finished_PSLV())
        {
            if(Phase2Start)
            {
                StartPhase2();
            }
            

            if (seperationPhase_PSLV == null)
            {
                seperationPhase_PSLV = FindObjectOfType<SeperationPhaseValues_PSLV>()?.gameObject;
            }

            if (performRaycast)
            {
                arrowGameObject.SetActive(true);

                PerformRaycast();
            }
        }

        if (seperationPhase_PSLV != null)
        {
            LookAtPhase2_AdityaL1();
        }
    }

    private void LookAtPhase2_AdityaL1()
    {
        arrowGameObject.transform.LookAt(seperationPhase_PSLV.transform);
    }

    private void PerformRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Phase2"))
            {
                vibrationController.VibratePhone_Heavy();

                Phase2Start = true;

                arrowGameObject.SetActive(false);

                performRaycast = false;
            }
        }
    }

    private void StartPhase2()
    {
        rocketSeperationStages_Manager.StartSeperationPhase();

        Phase2Start = false;
    }
}
