using System.Collections;
using UnityEngine;

public class UserTap_Handler : MonoBehaviour
{
    [SerializeField]
    public GameObject userTap;

    [SerializeField]
    private VibrationController vibrationController;

    [SerializeField]
    private UISoundSO uISoundSO;

    [Space]
    [SerializeField]
    [Range(0f, 15f)]
    [Tooltip("How much time to enable user tap UI appears after delay")]
    private float delay;

    void Awake()
    {
        StartCoroutine(EnableUserTapAfterDelay(delay));
    }

    IEnumerator EnableUserTapAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        uISoundSO.PlayTapSound();
        vibrationController.VibratePhone_Medium();

        userTap.SetActive(true);
    }
}
