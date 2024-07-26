using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoLandingZoneCollisionChecker : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lander"))
        {
            LanderCollisionHandler.Instance.StartMissionFailSequence();
        }
    }
}
