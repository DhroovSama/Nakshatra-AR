using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoverMechanics : MonoBehaviour
{
    #region Data Variables
    [SerializeField] private GameObject RoverParent;

    [SerializeField] private GameObject RoverParentChild;

    [SerializeField] private GameObject RoverWheels;

    [SerializeField] LanderMechanics landerMechanics;

    [SerializeField] LanderAnimation landerAnimation;

    [SerializeField]
    private Rigidbody roverRB;

    [Header("Data Variables")]

    [SerializeField]
    [Range(0f, 300f)]
    private int wheelsRotationSpeed = 10;

    [SerializeField]
    [Range(0f, 300f)]
    private int roverRotationSpeed = 10;

    [SerializeField]
    [Range(0f, 300f)]
    private int wheelsRotationForForward = 10;

    [SerializeField]
    [Range(0f, 300f)]
    private int wheelsRotationForBackward = 10;

    private bool isLeftPressed = false;

    [SerializeField]
    [Range(-0.5f, 50f)]
    private float forceAmount = 0f;

    #endregion

    #region UI Elements
    [Header("UI Elements")]

    [SerializeField] private GameObject thrustSliderUI;
    [SerializeField] private GameObject velocityLanderUI;
    [SerializeField] private GameObject directionalThrustUI;

    [Space]
    [SerializeField] private GameObject roverControls;
    public GameObject RoverControls { get { return roverControls; } set { roverControls = value; } }

    [Space]
    [SerializeField] private GameObject roverFPV;
    public GameObject RoverFPV { get { return roverFPV; } }
    #endregion

    #region Rover Wheels
    [Header("Rover Right Wheels")]

    [SerializeField] private GameObject leftWheel_1;
    [SerializeField] private GameObject leftWheel_2;
    [SerializeField] private GameObject leftWheel_3;

    [Header("Rover Left Wheels")]

    [SerializeField] private GameObject leftWheel_4;
    [SerializeField] private GameObject leftWheel_5;
    [SerializeField] private GameObject leftWheel_6;
    #endregion


    private void Update()
    {
        FindRoverGameObjectInSceneAndRB();

        EnableRoverControlUI();

        if(RoverWheels != null)
        {
            AssignRoverWheels();
        }

        AssignRoverParentChild();
    }

    private void OnEnable()
    {
        RoverEvents.OnRotateRoverRight += RotateRoverRight;
        RoverEvents.OnRotateRoverLeft += RotateRoverLeft;

        RoverEvents.OnRoverForward += MoveRoverForward;
        RoverEvents.OnRoverBackward += MoveRoverBackward;
    }

    private void OnDisable()
    {
        RoverEvents.OnRotateRoverRight -= RotateRoverRight;
        RoverEvents.OnRotateRoverLeft -= RotateRoverLeft;

        RoverEvents.OnRoverForward -= MoveRoverForward;
        RoverEvents.OnRoverBackward -= MoveRoverBackward;
    }

    private void AssignRoverParentChild()
    {
        if(RoverParent != null)
        {
            Transform roverParent = RoverParent.transform.Find("Rover1");

            if(roverParent != null)
            {
                RoverParentChild = roverParent.gameObject;
            }
        }
    }

    private void FindRoverGameObjectInSceneAndRB()
    {
        if (landerMechanics.IsLanderInstanceSpawned)
        {
            FindRoverByScript findRoverByScript = FindObjectOfType<FindRoverByScript>();

            FindRoverWheelsByScript findRoverWheelsByScript = FindObjectOfType<FindRoverWheelsByScript>(); 

            if (findRoverByScript != null)
            {
                RoverParent = findRoverByScript.gameObject;

                RoverWheels = findRoverWheelsByScript.gameObject;

                roverRB = findRoverByScript.gameObject.GetComponentInChildren<Rigidbody>();
            }
        }
    }

    private void EnableRoverControlUI()
    {
        if(landerAnimation.IsRoverAnimEnded) 
        {
            thrustSliderUI.SetActive(false);
            velocityLanderUI.SetActive(false);
            directionalThrustUI.SetActive(false);
        }
    }

    private void AssignRoverWheels()
    {
        RightWheelsAssignment();
        LeftWheelsAssignment();
    }

    private void RotateRoverLeft()
    {
        RotateLeftWheelsForward(wheelsRotationSpeed);
        RotateRoverRightAroundYAxis();
    }

    private void RotateRoverRight()
    {
        RotateRightWheelsForward(wheelsRotationSpeed);
        RotateRoverLeftAroundYAxis();
    }

    private void MoveRoverForward()
    {
        RotateRightWheelsForward(wheelsRotationForForward);
        RotateLeftWheelsForward(wheelsRotationForForward);

        MoveRoverInForwardDirection();
    }

    private void MoveRoverBackward()
    {
        RotateRightWheelsForward(-wheelsRotationForForward);
        RotateLeftWheelsForward(-wheelsRotationForForward);

        MoveRoverInBackwardDirection();
    }

    private void MoveRoverInForwardDirection()
    {
        Vector3 forceVector = -RoverParentChild.transform.right * forceAmount;

        roverRB.AddForce(forceVector);
    }

    private void MoveRoverInBackwardDirection()
    {
        Vector3 forceVector = RoverParentChild.transform.right * forceAmount;

        roverRB.AddForce(forceVector);
    }

    #region Rover Wheels Assignments
    private void LeftWheelsAssignment()
    {
        if (RoverWheels != null)
        {
            Transform wheel4Transform = RoverWheels.transform.Find("Wheel 4");
            Transform wheel5Transform = RoverWheels.transform.Find("Wheel 5");
            Transform wheel6Transform = RoverWheels.transform.Find("Wheel 6");

            if (wheel4Transform != null)
            {
                leftWheel_4 = wheel4Transform.gameObject;
                leftWheel_5 = wheel5Transform.gameObject;
                leftWheel_6 = wheel6Transform.gameObject;
            }
            else
            {
                Debug.LogError("Wheel 4 not found within RoverWheels.");
            }
        }
        else
        {
            Debug.LogError("RoverWheels is null.");
        }
    }

    private void RightWheelsAssignment()
    {
        if (RoverWheels != null)
        {
            Transform wheel1Transform = RoverWheels.transform.Find("Wheel 1");
            Transform wheel2Transform = RoverWheels.transform.Find("Wheel 2");
            Transform wheel3Transform = RoverWheels.transform.Find("Wheel 3");

            if (wheel1Transform != null)
            {
                leftWheel_1 = wheel1Transform.gameObject;
                leftWheel_2 = wheel2Transform.gameObject;
                leftWheel_3 = wheel3Transform.gameObject;
            }
            else
            {
                Debug.LogError("Wheel 4 not found within RoverWheels.");
            }
        }
        else
        {
            Debug.LogError("RoverWheels is null.");
        }
    }
    #endregion

    #region Rover Right Turn Mechanics
    private void RotateLeftWheelsForward(float rotationSpeed)
    {
        float rotationAngle = rotationSpeed * Time.deltaTime;

        leftWheel_4.transform.Rotate(0, 0, rotationAngle);
        leftWheel_5.transform.Rotate(0, 0, rotationAngle);
        leftWheel_6.transform.Rotate(0, 0, rotationAngle);
    }

    private void RotateRoverRightAroundYAxis()
    {
        float rotationAmount = roverRotationSpeed * Time.deltaTime;

        RoverParentChild.transform.Rotate(0, rotationAmount, 0);
    }
    #endregion

    #region Rover Left Turn Mechanics
    private void RotateRightWheelsForward(float rotationSpeed)
    {
        float rotationAngle = rotationSpeed * Time.deltaTime;

        leftWheel_1.transform.Rotate(0, 0, rotationAngle);
        leftWheel_2.transform.Rotate(0, 0, rotationAngle);
        leftWheel_3.transform.Rotate(0, 0, rotationAngle);
    }

    private void RotateRoverLeftAroundYAxis()
    {
        float rotationAmount = roverRotationSpeed * Time.deltaTime;

        RoverParentChild.transform.Rotate(0, -rotationAmount, 0);
    } 
    #endregion

}
