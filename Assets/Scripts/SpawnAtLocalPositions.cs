using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtLocalPositions : MonoBehaviour
{
    [SerializeField]
    private GameObject noLandingZone;

    private void Awake()
    {
        CorrectPosition();
    }

    private void CorrectPosition()
    {
        noLandingZone.transform.localPosition = Vector3.zero;
        noLandingZone.transform.localRotation = Quaternion.identity;
        noLandingZone.transform.localScale = Vector3.one;
    }
}
