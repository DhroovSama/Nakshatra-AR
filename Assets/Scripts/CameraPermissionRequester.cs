using UnityEngine;
using UnityEngine.Android;

public class CameraPermissionRequester : MonoBehaviour
{
    void Start()
    {
        // Check and request camera permission
        CheckCameraPermission();
    }

    void CheckCameraPermission()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            // Request camera permission
            Permission.RequestUserPermission(Permission.Camera);
        }
        else
        {
            // Permission already granted, proceed to start AR experience
            StartARExperience();
        }
#else
        // Not on Android, proceed to start AR experience
        StartARExperience();
#endif
    }

    void OnApplicationFocus(bool hasFocus)
    {
#if UNITY_ANDROID
        if (hasFocus)
        {
            if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                // Permission granted, proceed to start AR experience
                StartARExperience();
            }
            else
            {
                // Permission denied, handle accordingly
                HandlePermissionDenied();
            }
        }
#endif
    }

    void StartARExperience()
    {
        // Your code to start the AR experience
        Debug.Log("Camera permission granted. Starting AR experience.");
    }

    void HandlePermissionDenied()
    {
        // Inform the user that camera permission is necessary
        Debug.LogWarning("Camera permission was denied. The AR experience cannot start without it.");
        // Optionally, display a UI message or prompt to the user
    }
}
