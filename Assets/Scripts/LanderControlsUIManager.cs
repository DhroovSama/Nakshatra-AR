using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanderControlsUIManager : MonoBehaviour
{
    private static LanderControlsUIManager instance;

    [Header("Lander Thrust Controls")]
    [SerializeField] private GameObject thrustersControlContainer;
    [SerializeField] private Slider thrustersControlSlider;

    [Space]
    [SerializeField] private GameObject directionalThrustJoystickContainer;
    [SerializeField] private FixedJoystick directionalThrustJoystick;

    [Space]
    [SerializeField] private TextMeshProUGUI descendingVelocity, Thrust, Fuel, Altitude;

    [Space]
    [SerializeField]
    private GameObject LanderInfoUIContainer, LanderControlsUIContainer, LanderPointingArrow, UserWarningToGiveControlOver;

    [Space]
    [SerializeField]
    private GameObject HandTouchUI, HandSwipeUI;

    [Space]
    [SerializeField]
    private GameObject RoverTutorialControls;

    [Space]
    [Header("Lander Spawner Button")]
    [SerializeField] private GameObject landerSpawnerButtonContainer;

    [Header("Lander Mission Status UI")]
    [SerializeField] private GameObject LanderMissionPass;
    [SerializeField] private GameObject LanderMissionFail;


    private void Awake()
    {
        instance = this;
    }

    public static GameObject getDirectionalThrustJoystickContainer()
    {
        return instance.directionalThrustJoystickContainer;
    }

    public static FixedJoystick getDirectionalThrustJoystick()
    {
        return instance.directionalThrustJoystick;
    }

    public static GameObject getsliderControlsContainer()
    {
        return instance.thrustersControlContainer;
    }

    public static Slider getsliderControls()
    {
        return instance.thrustersControlSlider;
    }

    public static TextMeshProUGUI getDescendingVelocity()
    {
        return instance.descendingVelocity;
    }

    public static TextMeshProUGUI getThrust()
    {
        return instance.Thrust;
    }

    public static TextMeshProUGUI getFuel()
    {
        return instance.Fuel;
    }
    public static TextMeshProUGUI getAltitude()
    {
        return instance.Altitude;
    }

    public static GameObject GetLanderInfoUIContainer()
    {
        return instance.LanderInfoUIContainer;
    }

    public static GameObject GetLanderControlsUIContainer()
    {
        return instance.LanderControlsUIContainer;
    }

    public static GameObject GetLanderPointingArrow()
    {
        return instance.LanderPointingArrow;
    }

    public static GameObject GetUserWarningToGiveControlOver()
    {
        return instance.UserWarningToGiveControlOver;
    }

    public static GameObject GetHandTouchUI()
    {
        return instance.HandTouchUI;
    }
    public static GameObject GetHandSwipeUI()
    {
        return instance.HandSwipeUI;
    }

    public static GameObject GetLanderMissioonPassUI()
    {
        return instance.LanderMissionPass;
    }
    public static GameObject GetLanderMissioonFailUI()
    {
        return instance.LanderMissionFail;
    }

    public static GameObject GetRoverTutorialControls()
    {
        return instance.RoverTutorialControls;
    }

    public static GameObject getLanderSpawnerButtonContainer()
    {
        return instance.landerSpawnerButtonContainer;
    }
}
