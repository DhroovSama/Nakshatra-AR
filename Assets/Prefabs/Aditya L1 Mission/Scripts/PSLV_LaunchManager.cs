using UnityEngine;
using UnityEngine.UI;

public class PSLV_LaunchManager : MonoBehaviour
{
    [SerializeField]
    private Button launchButton;

    [SerializeField]
    private Slider preCheckSlider;

    [SerializeField]
    private VibrationController vibrationController;

    private HoldButton holdButton;
    private float holdTime = 0f;

    [SerializeField, Range(0f, 15f), Tooltip("How long it will take for the pre check slider to fill")]
    private float fillDuration = 5f; 

    private void Start()
    {
        holdButton = launchButton.GetComponent<HoldButton>();

        if (holdButton == null)
        {
            holdButton = launchButton.gameObject.AddComponent<HoldButton>();
        }

        preCheckSlider.value = 0f;
    }

    private void Update()
    {
        if (holdButton.isHolding)
        {
            holdTime += Time.deltaTime;

            preCheckSlider.value = Mathf.Clamp01(holdTime / fillDuration);

            if (preCheckSlider.value >= 1f)
            {
                OnLaunchReady();
            }
        }
        else
        {
            holdTime = 0f;
            preCheckSlider.value = 0f;
        }
    }

    private void OnLaunchReady()
    {
        Debug.Log("Launch Ready!");

        if (vibrationController != null)
        {
            vibrationController.VibratePhone_Medium();
        }

        this.enabled = false;

    }
}
