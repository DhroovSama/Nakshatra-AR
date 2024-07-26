using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DisableARPlaneWhenObjectSpawned : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager planeManager;

    private void DisableARPlane()
    {
        foreach(var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }

    public void getDisableARPlane()
    {
        DisableARPlane();

        planeManager.enabled = false;
    }
}


