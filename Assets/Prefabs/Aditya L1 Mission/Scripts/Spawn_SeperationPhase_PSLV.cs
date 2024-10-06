using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_SeperationPhase_PSLV : MonoBehaviour
{
    [SerializeField]
    private GameObject seperationPSLV;

    [SerializeField, Tooltip("Auto Assigned")]
    private PlaceOnPlane placeOnPlane;

    private void Awake()
    {
        placeOnPlane = FindObjectOfType<PlaceOnPlane>();
    }

    private void SpawnSeperationPhasePSLV()
    {
        placeOnPlane.IsPhase1Finished = true;

        var instantiationCords = placeOnPlane.GetMoonSurfacePosition();

        // Add 0.5 to the Y-coordinate
        instantiationCords.y += 1.5f;

        // Calculate the direction from the object to the camera
        Vector3 directionToCamera = Camera.main.transform.position - instantiationCords;

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
        Instantiate(seperationPSLV, instantiationCords, rotation);
    }


}
