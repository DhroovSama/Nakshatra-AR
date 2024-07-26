using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(ARRaycastManager))]
public class DistanceChecker : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject projectilePrefab;

    GameObject plane;
    TouchControls controls;
    bool isPressed;
    ARRaycastManager aRRaycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    [Space]
    [SerializeField] private bool isMoonSurfaceSpawned = false;
    private static PlaceOnPlane instance;
    [SerializeField] private RawImage pointer;

    private bool projectileFired = false;

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        controls = new TouchControls();
        controls.control.touch.performed += _ => isPressed = true;
        controls.control.touch.canceled += _ => isPressed = false;
    }

    private void Update()
    {
        if (Pointer.current == null || isPressed == false)
            return;

        var touchPosition = Pointer.current.position.ReadValue();

        if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (plane == null && !isMoonSurfaceSpawned)
            {
                plane = Instantiate(target, hitPose.position, hitPose.rotation);
                StartCoroutine(WaitAndShoot());
            }
        }
    }

    private IEnumerator WaitAndShoot()
    {
        yield return new WaitForSeconds(1f); 
        projectileFired = false;
    }

    private void ShootProjectile()
    {
        Vector3 spawnPosition = Camera.main.transform.position;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        projectile.transform.forward = Camera.main.transform.forward;

        projectileFired = true;

        Destroy(projectile, 1f);

        Invoke("ResetProjectileFired", 1f);
    }


    private void OnEnable()
    {
        controls.control.Enable();
    }

    private void OnDisable()
    {
        controls.control.Disable();
    }

    public void ResetProjectileFired()
    {
        projectileFired = false;
    }
}
