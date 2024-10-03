using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField] GameObject moonSurfacePrefab;
    [SerializeField] private DisableARPlaneWhenObjectSpawned disableARPlane;

    [SerializeField]
    private VibrationController vibrationController;

    GameObject spawnedObjectMoonSurface;
    public GameObject SpawnedObjectMoonSurface
    {
        get { return spawnedObjectMoonSurface; }
        set { spawnedObjectMoonSurface = value; }
    }

    [SerializeField]
    private Vector3 moonSurfacePosition;
    [SerializeField]
    private Quaternion moonSurfaceRotation;

    TouchControls controls;
    bool isPressed;
    ARRaycastManager aRRaycastManager;
    ARPlaneManager aRPlaneManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [Space]
    [SerializeField] private bool isMoonSurfaceSpawned = false;

    [Space]
    [SerializeField] private bool isPSLVSpawned = false;

    private static PlaceOnPlane instance;

    [SerializeField]
    private VoiceOverData MoonSurfaceInitialisedSO;

    private void Awake()
    {
        instance = this;
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
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

            if (spawnedObjectMoonSurface == null && !isMoonSurfaceSpawned)
            {
                ARPlane lowestPlane = GetLowestPlane();

                if (lowestPlane != null)
                {
                    spawnedObjectMoonSurface = Instantiate(moonSurfacePrefab, lowestPlane.center, Quaternion.identity);

                    moonSurfacePosition = lowestPlane.center;
                    moonSurfaceRotation = Quaternion.identity;

                    isMoonSurfaceSpawned = true;
                    isPSLVSpawned = true;

                    disableARPlane.getDisableARPlane();
                }
                else
                {
                    spawnedObjectMoonSurface.transform.position = hitPose.position;
                    spawnedObjectMoonSurface.transform.rotation = hitPose.rotation;

                    // Store the position and rotation
                    moonSurfacePosition = hitPose.position;
                    moonSurfaceRotation = hitPose.rotation;
                }

                vibrationController.VibratePhone_Medium();

                VoiceOverManager.Instance.TriggerVoiceOver(MoonSurfaceInitialisedSO);
            }
        }
    }

    private ARPlane GetLowestPlane()
    {
        ARPlane lowestPlane = null;
        float lowestY = float.MaxValue;

        foreach (var plane in aRPlaneManager.trackables)
        {
            if (plane.transform.position.y < lowestY)
            {
                lowestY = plane.transform.position.y;
                lowestPlane = plane;
            }
        }

        return lowestPlane;
    }

    private void OnEnable()
    {
        controls.control.Enable();
    }

    private void OnDisable()
    {
        controls.control.Disable();
    }

    private void _MoonSurfaceSpawned()
    {
        isMoonSurfaceSpawned = true;
    }

    public static bool IsMoonSurfaceSpawned()
    {
        return instance.isMoonSurfaceSpawned;
    }

    public static bool IsPSLVSpawned()
    {
        return instance.isPSLVSpawned;
    }

    public static GameObject getSpawnedObjectMoonSurface()
    {
        return instance.spawnedObjectMoonSurface;
    }

    public Vector3 GetMoonSurfacePosition()
    {
        return moonSurfacePosition;
    }

    public Quaternion GetMoonSurfaceRotation()
    {
        return moonSurfaceRotation;
    }
}
