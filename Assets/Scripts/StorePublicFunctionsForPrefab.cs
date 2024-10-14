using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePublicFunctionsForPrefab : MonoBehaviour
{
    [SerializeField]
    private RoverMechanics roverMechanics;

    [SerializeField]
    private GameObject roverFPVCamera;

    [SerializeField]
    private FirstTimeRoverTutorialHandler firstTimeRoverTutorialHandler;

    private void Awake()
    {
        if(roverMechanics == null)
        {
            roverMechanics = FindObjectOfType<RoverMechanics>();
        }

        if(firstTimeRoverTutorialHandler == null)
        {
            firstTimeRoverTutorialHandler = FindObjectOfType<FirstTimeRoverTutorialHandler>();
        }
    }

    public void EnableRoverFPV_UI()
    {
        roverMechanics.RoverControls.SetActive(true);

        roverMechanics.RoverFPV.SetActive(true);

        roverFPVCamera.SetActive(true);
    }

    public void EnableLocationPopTutorial()
    {
        firstTimeRoverTutorialHandler.TriggerLoactionPopupTutorial();
    }
}
