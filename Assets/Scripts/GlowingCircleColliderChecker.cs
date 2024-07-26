using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingCircleColliderChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            PlaceOnPlane.getSpawnedObjectMoonSurface();
        }
    }
}
