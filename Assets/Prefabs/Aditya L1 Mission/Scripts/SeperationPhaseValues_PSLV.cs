using System.Collections;
using TMPro;
using UnityEngine;

public class SeperationPhaseValues_PSLV : MonoBehaviour, ISeperationPhaseValues
{
    [SerializeField] private float initialTime = 0f;
    [SerializeField] private float initialVelocity = 0f;
    [SerializeField] private float initialRange = 0f;
    [SerializeField] private float initialAltitude = 0f;
    [SerializeField] private float initialAzimuth = 0f;


    public float time { get; set; }
    public float velocity { get; set; }
    public float range { get; set; }
    public float altitude { get; set; }
    public float azimuth { get; set; }

    [SerializeField]
    private TextMeshProUGUI timeText, velocityText, rangeText, altitudeText, azimuthText;

    [Space]
    [SerializeField, Range(0f, 1f)]
    private float velocityIncrement = 0.1f;
    [SerializeField, Range(0f, 1f)]
    private float rangeIncrement = 0.05f;
    [SerializeField, Range(0f, 1f)]
    private float altitudeIncrement = 0.07f;
    [SerializeField, Range(0f, 1f)]
    private float azimuthIncrement = 0.03f;

    private bool isMissionRunning = false;

    public void MissionTimeStart()
    {
        time = initialTime;
        velocity = initialVelocity;
        range = initialRange;
        altitude = initialAltitude;
        azimuth = initialAzimuth;

        UpdateAllInitialValues();

        isMissionRunning = true;
        StartCoroutine(UpdateMissionTime());
    }

    private void UpdateAllInitialValues()
    {
        timeText.text = $"{time:F1}";
        velocityText.text = $"{velocity:F1}";
        rangeText.text = $"{range:F1}";
        altitudeText.text = $"{altitude:F1}";
        azimuthText.text = $"{azimuth:F1}";
    }

    private IEnumerator UpdateMissionTime()
    {
        while (isMissionRunning)
        {
            time += Time.deltaTime;
            timeText.text = $"{time:F1}";
            yield return null;
        }
    }

    public void StartVelocityUpdate()
    {
        StartCoroutine(UpdateVelocity());
    }

    private IEnumerator UpdateVelocity()
    {
        while (isMissionRunning)
        {
            velocity += velocityIncrement * Time.deltaTime;
            velocityText.text = $"{velocity:F1}";
            yield return null;
        }
    }

    public void StartRangeUpdate()
    {
        StartCoroutine(UpdateRange());
    }

    private IEnumerator UpdateRange()
    {
        while (isMissionRunning)
        {
            range += rangeIncrement * Time.deltaTime;
            rangeText.text = $"{range:F1}";
            yield return null;
        }
    }

    public void StartAltitudeUpdate()
    {
        StartCoroutine(UpdateAltitude());
    }

    private IEnumerator UpdateAltitude()
    {
        while (isMissionRunning)
        {
            altitude += altitudeIncrement * Time.deltaTime;
            altitudeText.text = $"{altitude:F1}";
            yield return null;
        }
    }

    public void StartAzimuthUpdate()
    {
        StartCoroutine(UpdateAzimuth());
    }

    private IEnumerator UpdateAzimuth()
    {
        while (isMissionRunning)
        {
            azimuth += azimuthIncrement * Time.deltaTime;
            azimuthText.text = $"{azimuth:F1}";
            yield return null;
        }
    }

    public void StopMission()
    {
        isMissionRunning = false;
    }
}

public interface ISeperationPhaseValues
{
    float time { get; set; }
    float velocity { get; set; }
    float range { get; set; }
    float altitude { get; set; }
    float azimuth { get; set; }
}
