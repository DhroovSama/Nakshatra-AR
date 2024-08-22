using UnityEngine;
using UnityEngine.Rendering;

public class PhoneRotationChecker : MonoBehaviour
{
    private bool isWaitingForRotation = false;
    private bool hasPlayedVoiceOver = false;

    [SerializeField]
    private VoiceOverData scanSurfaceVO;

    [SerializeField]
    private VibrationController vibrationController;

    [SerializeField]
    private GameObject rotatePhoneCnavas, AImuteButton;

    public void CheckAndSetLeftLandscape()
    {
        // Start waiting for the user to rotate the phone
        isWaitingForRotation = true;
        hasPlayedVoiceOver = false;
    }

    void Update()
    {
        if (isWaitingForRotation)
        {
            if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
            {
                Screen.orientation = ScreenOrientation.LandscapeLeft;

                vibrationController.VibratePhone_Light();

                isWaitingForRotation = false;

                if (!hasPlayedVoiceOver)
                {
                    VoiceOverManager.Instance.TriggerVoiceOver(scanSurfaceVO);
                    hasPlayedVoiceOver = true;
                }
                AImuteButton.SetActive(true);

                rotatePhoneCnavas.SetActive(false); 
            }
        }
    }
}
