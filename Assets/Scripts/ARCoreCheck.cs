using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class ARCoreCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject permissionPanel, userWarningPanel;

    [SerializeField]
    private Button settingsButton, exitButton;

    private bool isCheckingPermission = false;

    [SerializeField]
    private ARSession arSession;

    [SerializeField]
    private VibrationController vibrationController;

    [SerializeField]
    private UISoundSO uISoundSO;

    private bool hasShownUserWarning = false;

    void Start()
    {
        // Only disable ARSession if permission is not granted
        if (arSession != null)
        {
            arSession.enabled = false;
        }

#if UNITY_ANDROID
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            // Permission is already granted, proceed to check AR support
            StartCoroutine(CheckARSupport());
        }
        else
        {
            // Permission is not granted, start the permission check
            StartCoroutine(CheckARSupportAndCamera());
        }
#else
        // For other platforms, proceed to check AR support
        StartCoroutine(CheckARSupport());
#endif
    }

    IEnumerator CheckARSupport()
    {
        // Begin checking for AR support
        if (ARSession.state == ARSessionState.None || ARSession.state == ARSessionState.CheckingAvailability)
        {
            yield return ARSession.CheckAvailability();
        }

        // Wait until the AR session state is determined
        while (ARSession.state == ARSessionState.CheckingAvailability)
        {
            yield return null;
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            // Device does not support ARCore
            SceneManager.LoadScene(1);
        }
        else
        {
            // Device supports ARCore
            OnPermissionGrantedAndARCoreSupported();
        }
    }

    IEnumerator CheckARSupportAndCamera()
    {
        // Check for camera permission
        yield return StartCoroutine(CheckCameraPermission());

#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            // Permission was not granted; exit the coroutine
            yield break;
        }
#endif

        // Proceed to check AR support
        yield return StartCoroutine(CheckARSupport());
    }

    IEnumerator CheckCameraPermission()
    {
#if UNITY_ANDROID
        // Check if camera permission is granted
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            isCheckingPermission = true;
            bool permissionGranted = false;
            bool permissionDenied = false;

            PermissionCallbacks permissionCallbacks = new PermissionCallbacks();
            permissionCallbacks.PermissionGranted += s => { permissionGranted = true; };
            permissionCallbacks.PermissionDenied += s => { permissionDenied = true; };
            permissionCallbacks.PermissionDeniedAndDontAskAgain += s => { permissionDenied = true; };

            // Request camera permission
            Permission.RequestUserPermission(Permission.Camera, permissionCallbacks);

            // Wait until the user responds to the permission request
            while (!permissionGranted && !permissionDenied)
            {
                yield return null;
            }

            isCheckingPermission = false;

            if (permissionDenied)
            {
                // User has denied the camera permission
                // Show UI prompt to guide the user to settings
                ShowPermissionDeniedUI();
                yield break;
            }
        }
#elif UNITY_IOS
        // Handle iOS permissions if necessary
#endif

        // Check if the camera is available
        if (!IsCameraAvailable())
        {
            // Camera is not available (e.g., in use by another app or hardware issue)
            SceneManager.LoadScene(1);
            yield break;
        }
    }

    void ShowPermissionDeniedUI()
    {
        if (permissionPanel != null)
        {
            vibrationController.VibratePhone_Heavy();
            uISoundSO.PlayWrongSound();

            permissionPanel.SetActive(true);

            // Remove previous listeners to avoid duplicates
            settingsButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();

            // Add the listener to open app settings
            settingsButton.onClick.AddListener(OpenAppSettings);

            // Optionally, add a listener for an exit button
            exitButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        }
        else
        {
            // If no UI is set up, log a message and open settings
            Debug.Log("Camera permission is required. Please enable it in the app settings.");
            OpenAppSettings();
        }
    }

    void OpenAppSettings()
    {
#if UNITY_ANDROID
        AndroidJavaObject currentActivity = null;

        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }

        using (AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", "android.settings.APPLICATION_DETAILS_SETTINGS"))
        {
            string packageName = currentActivity.Call<string>("getPackageName");
            using (AndroidJavaObject uri = new AndroidJavaClass("android.net.Uri").CallStatic<AndroidJavaObject>("parse", "package:" + packageName))
            {
                intent.Call<AndroidJavaObject>("setData", uri);
            }
            currentActivity.Call("startActivity", intent);
        }
#endif
    }

    bool IsCameraAvailable()
    {
        // Check if any camera device is available  
        return WebCamTexture.devices.Length > 0;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && !isCheckingPermission)
        {
            // The app has focus again, check the permission
            StartCoroutine(OnAppFocusRoutine());
        }
    }

    IEnumerator OnAppFocusRoutine()
    {
        // Small delay to ensure the app has fully regained focus
        yield return new WaitForSeconds(0.1f);

#if UNITY_ANDROID
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            // Permission has been granted
            if (permissionPanel != null && permissionPanel.activeSelf)
            {
                permissionPanel.SetActive(false);
            }

            // Proceed to check AR support
            yield return StartCoroutine(CheckARSupport());
        }
        else
        {
            // Permission still not granted, show the permission panel if not already visible
            if (permissionPanel != null && !permissionPanel.activeSelf)
            {
                ShowPermissionDeniedUI();
            }
        }
#endif
    }

    void OnPermissionGrantedAndARCoreSupported()
    {
        // Show the user warning panel only once
        if (!hasShownUserWarning)
        {
            if (userWarningPanel != null)
            {
                userWarningPanel.SetActive(true);
            }
            hasShownUserWarning = true;
        }

        // Start the AR session (enable the camera)
        if (arSession != null)
        {
            arSession.enabled = true;
        }
    }
}
