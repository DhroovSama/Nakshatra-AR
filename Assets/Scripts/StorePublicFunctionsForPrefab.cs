using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePublicFunctionsForPrefab : MonoBehaviour
{
    [SerializeField]
    private RoverMechanics roverMechanics;

    [SerializeField]
    private GameObject roverFPVCamera;

    private void Awake()
    {
        if(roverMechanics == null)
        {
            roverMechanics = FindObjectOfType<RoverMechanics>();
        }
    }

    public void EnableRoverFPV_UI()
    {
        roverMechanics.RoverControls.SetActive(true);

        roverMechanics.RoverFPV.SetActive(true);

        roverFPVCamera.SetActive(true);
    }
}
