using UnityEngine;
using UnityEngine.UI;

public class GlobalUIProvider_AdityaL1 : MonoBehaviour
{
    private static GlobalUIProvider_AdityaL1 instance;

    [SerializeField]
    private GameObject seperationPhaseTutorial, orbitShiftPhaseTutorial, blurBG;

    [SerializeField]
    private Button nextPhaseButton;

    [SerializeField]
    private GameObject seperationPhaseUI;

    [SerializeField]
    private Button orbitShiftButton;

    [Header("UI Elements"), Tooltip("Auto Assigned")]
    public GameObject userTap;
    public GameObject UserTap
    {
        get => userTap;
        set => userTap = value;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(PlaceOnPlane.IsPSLVSpawned() && !PlaceOnPlane.IsPhase1Finished_PSLV())
        {
            UserTap_Handler userTapHandler = FindObjectOfType<UserTap_Handler>();

            if (userTapHandler != null)
            {
                userTap = userTapHandler.userTap;
            }
            else
            {
                Debug.LogError("UserTap_Handler not found in the scene.");
            }
        }
    }

    public static GameObject getSeperationPhaseTutorial()
    {
        return instance.seperationPhaseTutorial;
    }
    public static GameObject getOrbitShiftPhaseTutorial()
    {
        return instance.orbitShiftPhaseTutorial;
    }

    public static GameObject getuserTap()
    {
        return instance.userTap;
    }

    public static Button getNextPhaseButton()
    {
        return instance.nextPhaseButton;
    }
    public static Button getOrbitShiftButton()
    {
        return instance.orbitShiftButton;
    }

    public static GameObject getSeperationPhaseUI()
    {
        return instance.seperationPhaseUI;  
    }
    public static GameObject getBlurBG()
    {
        return instance.blurBG;
    }
}

