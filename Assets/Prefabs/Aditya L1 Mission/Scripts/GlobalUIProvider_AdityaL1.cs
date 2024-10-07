using UnityEngine;
using UnityEngine.UI;

public class GlobalUIProvider_AdityaL1 : MonoBehaviour
{
    private static GlobalUIProvider_AdityaL1 instance;

    [SerializeField]
    private GameObject seperationPhaseTutorial;

    [SerializeField]
    private Button nextPhaseButton;

    [SerializeField]
    private GameObject seperationPhaseUI;

    [Header("UI Elements"), Tooltip("Auto Assigned")]
    public GameObject userTap;

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

    public static GameObject getuserTap()
    {
        return instance.userTap;
    }

    public static Button getNextPhaseButton()
    {
        return instance.nextPhaseButton;
    }

    public static GameObject getSeperationPhaseUI()
    {
        return instance.seperationPhaseUI;  
    }
}
