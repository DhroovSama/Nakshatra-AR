using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField] GameObject moonSurfacePrefab;

    [SerializeField] private DisableARPlaneWhenObjectSpawned disableARPlane;


    GameObject spawnedObjectMoonSurface;
    public GameObject SpawnedObjectMoonSurface
    {
        get { return spawnedObjectMoonSurface; }
        set { spawnedObjectMoonSurface = value;}
    }

    TouchControls controls;

    bool isPressed;

    ARRaycastManager aRRaycastManager;

    //[SerializeField] private bool isMoon = false;
    //public bool IsMoon { get { return isMoon; } }


    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [Space]
    [SerializeField] private bool isMoonSurfaceSpawned = false;

    private static PlaceOnPlane instance;
    

    private void Awake()
    {
        instance = this;

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

            if (spawnedObjectMoonSurface == null && !isMoonSurfaceSpawned)
            {
                spawnedObjectMoonSurface = Instantiate(moonSurfacePrefab, hitPose.position, hitPose.rotation);

                isMoonSurfaceSpawned = true;

                disableARPlane.getDisableARPlane();

                //isMoon = true;

                //_MoonSurfaceSpawned();
            }
        }
    }

    //public void SetMoonSurfaceSpawned()
    //{
    //    isMoonSurfaceSpawned = true;
    //    // Assuming you have a reference to UI_Manager or a way to access it
    //    // For example, if UI_Manager is a singleton or you have a static reference to it
    //    UI_Manager.Instance.landerSpawnerButtonSequence();
    //}

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

    public static GameObject getSpawnedObjectMoonSurface()
    {
        return instance.spawnedObjectMoonSurface;
    }


}

