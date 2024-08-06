using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class LanderMechanics : MonoBehaviour
{
    private static LanderMechanics instance;

    [SerializeField]
    private UI_Manager uI_Manager;

    [SerializeField]
    private LanderCollisionHandler landerCollisionHandler;

    [SerializeField]
    private ARSession ARSession;

    [SerializeField]
    private GameObject landerInstance;
    public GameObject LanderInstance { get { return landerInstance; } }

    [SerializeField]
    private GameObject landerInstanceChild;

    private Animator landerInstanceAnimator;

    [SerializeField] private GameObject Lander;

    [SerializeField] private GameObject moonTerrain;

    [SerializeField] private LanderAnimation landerAnimation;

    [Header("Lander Thrust Controls")]
    [SerializeField] private GameObject landerThrustSliderContainer;
    [SerializeField] private Slider landerThrustSlider;
    public Slider LanderThrustSlider
    {
        get { return landerThrustSlider; }
        set { landerThrustSlider = value; }
    }

    [Header("Lander Directional Thrust Controls")]
    [SerializeField] private GameObject landerDirectionalThrustJoystickContainer;
    [SerializeField] private FixedJoystick landerDirectionalThrustJoystick;

    [Header("Lander UI")]
    [SerializeField] private TextMeshProUGUI landerDescendVelocityUI, ThrustValueUI, FuelValueUI, AltitudeValueUI;

    [SerializeField] private float initialFuel = 100; // Initial fuel amount

    private float currentFuel = 0; // Current fuel amount
    public float CurrentFuel
    {
        get { return currentFuel; }
        set { currentFuel = value; }
    }

    //[Space]
    //[SerializeField] private Vector3 joystickDirection;

    [Header("Joystick Values")]
    [SerializeField] private Vector3 thrustDirection;
    [SerializeField] private float directionForceMagnitudeApplied = 0;
    [SerializeField] private float directionsThrustValue = 15;
    [SerializeField] private float degreesToRotate = 0;


    [Header("Upward Thrust Value")]
    [SerializeField] private float sliderThrustValue;

    [Space]
    [Tooltip("The offsetVector in your LanderMechanics script is used to adjust the position of the lander when it is spawned on the moon's surface. This vector is added to the position of the moon's surface to determine the final spawn position of the lander. ")]
    [SerializeField] private Vector3 offsetVector;

    [SerializeField] private Vector3 worldPositionToSpawn;

    [Header("Lander Rigidbodies")]
    [SerializeField] Rigidbody landerRB;
    public Rigidbody LanderRB
    {
        get { return landerRB; }
        set { landerRB = value; }
    }

    [Header("Upward Thrust Value")]
    [SerializeField] private float descendVelocityValue;
    public float DescendVelocityValue
    {
        get { return descendVelocityValue; }
        set { descendVelocityValue = value; }
    }

    [Header("To Be Deleted Later")]
    [SerializeField] private Button reset;

    [SerializeField] bool resetOnceCalled = false;

    [SerializeField] bool isLanderInstanceSpawned = false;
    public bool IsLanderInstanceSpawned
    {
        get { return isLanderInstanceSpawned; }
    }

    private bool missionFailed = false; // Flag to indicate if the mission failure sequence has started

    [SerializeField]
    private float altitudeDifference;


    private void Awake()
    {
        instance = this;

        landerThrustSlider = LanderControlsUIManager.getsliderControls();
        landerThrustSliderContainer = LanderControlsUIManager.getsliderControlsContainer();

        landerDirectionalThrustJoystickContainer = LanderControlsUIManager.getDirectionalThrustJoystickContainer();
        landerDirectionalThrustJoystick = LanderControlsUIManager.getDirectionalThrustJoystick();

        landerDescendVelocityUI = LanderControlsUIManager.getDescendingVelocity();

        ThrustValueUI = LanderControlsUIManager.getThrust();

        FuelValueUI = LanderControlsUIManager.getFuel();

        AltitudeValueUI = LanderControlsUIManager.getAltitude();

    }

    private void Update()
    {
        if (uI_Manager.HasLanderSpawned)
        {
            landerCollisionHandler = FindObjectOfType<LanderCollisionHandler>();
        }

        if(PlaceOnPlane.IsMoonSurfaceSpawned() && moonTerrain == null)
        {
            moonTerrain = GameObject.FindGameObjectWithTag("moon");
        }

        AddThrust();

        VelocityManager();

        UpdateUI();
        //AddDirectionalThrust();
    }

    private void FixedUpdate()
    {
        AddDirecThrust();
    }

    private void UpdateUI()
    {
        ThrustValueUI.text = sliderThrustValue.ToString("F1") + "N";
        FuelValueUI.text = currentFuel.ToString("F1") + "%";


        if(landerInstanceChild != null)
        {
            altitudeDifference = landerInstanceChild.transform.position.y - moonTerrain.transform.position.y;

            AltitudeValueUI.text = altitudeDifference.ToString("F1") + " m";
        }
        
    }



    private void SpawnLanderOnMoonSurface()
    {
        Vector3 moonSurfacePosition = PlaceOnPlane.getSpawnedObjectMoonSurface().transform.position;
        Quaternion moonSurfaceRotation = PlaceOnPlane.getSpawnedObjectMoonSurface().transform.rotation;

        Vector3 _moonSurfacePosition = moonSurfacePosition + offsetVector;

        landerInstance = Instantiate(Lander, worldPositionToSpawn, moonSurfaceRotation);

        currentFuel = initialFuel;
        UpdateUI();

        isLanderInstanceSpawned = true;

        landerRB = landerInstance.GetComponentInChildren<Rigidbody>();

        landerInstanceChild = FindObjectOfType<LanderCollisionHandler>().gameObject;

        landerAnimation.getLanderInstanceAnimatorComponent();

        landerThrustSliderContainer.SetActive(true);

        landerAnimation.getLandingAnimation();

    }

    public static void getSpawnLanderOnMoonSurface()
    {
        instance.SpawnLanderOnMoonSurface();
    }

    private void AddThrust()
    {
        if (landerRB != null && !missionFailed)
        {
            sliderThrustValue = landerThrustSlider.value;
            landerRB.AddForce(Vector3.up * sliderThrustValue, ForceMode.Force);

            // Decrease fuel based on thrust
            currentFuel -= sliderThrustValue * Time.deltaTime;

            if (currentFuel <= 0)
            {
                if (!missionFailed) 
                {
                    landerCollisionHandler.StartMissionFailSequence();
                    missionFailed = true; 
                    currentFuel = 0; 

                    LanderControlsUIManager.GetLanderInfoUIContainer().SetActive(false);

                    LanderControlsUIManager.getsliderControls().value = 0;  

                    LanderControlsUIManager.GetLanderControlsUIContainer().SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("Rigidbody not assigned");
        }
    }


    private void AddDirecThrust()
    {
        thrustDirection = Vector3.forward * landerDirectionalThrustJoystick.Vertical + Vector3.right * landerDirectionalThrustJoystick.Horizontal;

        directionForceMagnitudeApplied = thrustDirection.magnitude * directionsThrustValue;
        landerRB.AddForce(thrustDirection * directionForceMagnitudeApplied, ForceMode.Force);

        float targetRotationAngleX = 0f;
        float targetRotationAngleZ = 0f;

        if (landerDirectionalThrustJoystick.Horizontal != 0)
        {
            targetRotationAngleZ = -landerDirectionalThrustJoystick.Horizontal * degreesToRotate;
        }

        if (landerDirectionalThrustJoystick.Vertical != 0)
        {
            targetRotationAngleX = landerDirectionalThrustJoystick.Vertical * degreesToRotate;
        }

        Quaternion targetRotation = Quaternion.Euler(targetRotationAngleX, 0, targetRotationAngleZ);
        Quaternion currentRotation = landerRB.rotation;

        float rotationSpeed = 5f;
        landerRB.MoveRotation(Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime));
    }

    private void VelocityManager()
    {
        descendVelocityValue = landerRB.velocity.y;

        landerDescendVelocityUI.text = descendVelocityValue.ToString("F1") + " m/s";
    }

    public void ResetScene()
    {
        if(ARSession != null)
        {
            ARSession.Reset();
            LoaderUtility.Deinitialize();
            LoaderUtility.Initialize();

            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("ARSession is null");
        }
       
        resetOnceCalled = true;
    }
    public void ResetToAnyScene( int sceneNumber)
    {
        if (ARSession != null)
        {
            ARSession.Reset();
            LoaderUtility.Deinitialize();
            LoaderUtility.Initialize();

            SceneManager.LoadScene(sceneNumber, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("ARSession is null");
        }

        resetOnceCalled = true;
    }

    public static void getResetARToAnyScene(int sceneNumber)
    {
        instance.ResetToAnyScene(sceneNumber);
    }

    public void ResetSceneOnce()
    {
        if(!resetOnceCalled)
        {
            Debug.Log("Start Die Sequence");
            ResetScene(); 
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("moon"))
    //    {
    //        Debug.Log("yello");
    //        if (descendVelocityValue > 1)
    //        {
    //            ResetSceneOnce();
    //        }
    //    }
    //}

}

// Right - x = 1, y = 0
// Left - x = -1, y = 0

// Up - x = 0, y = 1
// Down - x = 0, y = -1

