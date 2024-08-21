using System.Collections;
using UnityEngine;
using TMPro;

public class SunlightTimer : MonoBehaviour
{
    [SerializeField]
    private Light sunLight;

    [SerializeField]
    [Range(0, 300)]
    private float rotationDuration = 180f;

    [SerializeField]
    private GameObject dayCounter;

    [SerializeField]
    private TextMeshProUGUI dayCounterText;

    [SerializeField]
    private GameObject missionFail;

    private float totalLunarDays = 14f;

    public void StartRotateSunLight_Night()
    {
        EnableDayCounter();
        StartCoroutine(RotateSunLight(180f));
    }

    private IEnumerator RotateSunLight(float targetRotationX)
    {
        float elapsedTime = 0f;
        Quaternion initialRotation = sunLight.transform.rotation;
        Quaternion finalRotation = Quaternion.Euler(targetRotationX, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z);

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotationDuration;

            sunLight.transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, t);

            float currentLunarDays = Mathf.Lerp(0, totalLunarDays, t);
            dayCounterText.text = $"{Mathf.FloorToInt(currentLunarDays)}/14 days";

            yield return null;
        }

        sunLight.transform.rotation = finalRotation;
        dayCounterText.text = "14/14 days";

        TriggerMissionFail();
    }

    private void EnableDayCounter()
    {
        dayCounter.SetActive(true);
    }

    private void DisableDayCounter()
    {
        dayCounter.SetActive(false);
    }

    private void TriggerMissionFail()
    {
        DisableDayCounter();
        missionFail.SetActive(true);
    }
}
