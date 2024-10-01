using UnityEngine;

public class GlobalUIProvider_AdityaL1 : MonoBehaviour
{
    private static GlobalUIProvider_AdityaL1 instance;

    [Header("UI Elements")]
    public GameObject userTap;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(PlaceOnPlane.IsPSLVSpawned())
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

    public static GameObject getuserTap()
    {
        return instance.userTap;
    }
}
