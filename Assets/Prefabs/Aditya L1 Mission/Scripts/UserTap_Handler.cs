using System.Collections;
using UnityEngine;

public class UserTap_Handler : MonoBehaviour
{
    [SerializeField]
    public GameObject userTap;

    [SerializeField]
    VibrationController vibrationController;

    [Space]
    [SerializeField]
    [Range(0f, 15f)]
    [Tooltip("How much time to enable user tap after delay")]
    private float delay;

    void Awake()
    {
        StartCoroutine(EnableUserTapAfterDelay(delay));
    }

    IEnumerator EnableUserTapAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        vibrationController.VibratePhone_Medium();
        userTap.SetActive(true);
    }
}
