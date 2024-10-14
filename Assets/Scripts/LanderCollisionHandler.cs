using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderCollisionHandler : MonoBehaviour
{
    [SerializeField] private LanderMechanics landerMechanics;
    [SerializeField] private LanderAnimation landerAnimation;

    [SerializeField] private GameObject LanderFireVFX;

    [SerializeField] private AudioSource LanderAudioSource;
    [SerializeField] private AudioClip ExplosionSFX;

    [SerializeField] float speed;

    private bool hasLandedSafely = false;
    public bool HasLandedSafely { get { return hasLandedSafely; } }

    private bool hasNotLandedSafely = false;
    public bool HadNotLandedSafely { get { return hasNotLandedSafely; } }

    [SerializeField] Vector3 contactPoint;

    [Space]
    [SerializeField]
    private VoiceOverData landerMissionPassVO, landerMissionFailVO;

    [SerializeField]
    private AudioClip landerSuccessSFX;

    [SerializeField]
    private AudioClip failMissionSFX;

    [SerializeField]
    private VibrationController vibrationController;

    public static LanderCollisionHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want the LanderCollisionHandler to persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }

        landerMechanics = FindObjectOfType<LanderMechanics>();
        landerAnimation = FindObjectOfType<LanderAnimation>();
    }

    private void Update()
    {
        speed = landerMechanics.DescendVelocityValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("moon"))
        {
            IfLanderNotLandedSafely();

            // Check if the landing was not successful before proceeding
            if (!hasNotLandedSafely)
            {
                contactPoint = collision.contacts[0].point;
                IfLanderLandedSafely(collision.contacts[0].point);
            }
        }
    }

    private void IfLanderNotLandedSafely()
    {
        if (!hasNotLandedSafely && speed <= -2)
        {
            Handheld.Vibrate();

            VoiceOverManager.Instance.TriggerVoiceOver(landerMissionFailVO);

            LanderControlsUIManager.GetLanderInfoUIContainer().SetActive(false);

            LanderControlsUIManager.GetLanderControlsUIContainer().SetActive(false);

            landerAnimation.LanderAnimator.enabled = false; 

            Debug.Log("DieSequenceToBeAdded");

            StartMissionFailSequence();

            hasNotLandedSafely = true;
        }
    }

    public void StartMissionFailSequence()
    {
        LanderAudioSource.PlayOneShot(ExplosionSFX);
        LanderFireVFX.SetActive(true);

        LanderControlsUIManager.GetLanderMissioonFailUI().SetActive(true);

        GlobalAudioPlayer.GetPlaySound(failMissionSFX);
    } 

    private void IfLanderLandedSafely(Vector3 landingPoint)
    {
        if (!hasLandedSafely && !hasNotLandedSafely && speed >= -2.1)
        {
            VoiceOverManager.Instance.TriggerVoiceOver(landerMissionPassVO);

            Debug.Log("safely landed at " + landingPoint);

            LanderControlsUIManager.getsliderControls().value = 0;
            LanderControlsUIManager.getDirectionalThrustJoystickContainer().SetActive(false);
            LanderControlsUIManager.getsliderControlsContainer().SetActive(false);
            LanderControlsUIManager.GetLanderInfoUIContainer().SetActive(false);

            GlobalAudioPlayer.GetPlaySound(landerSuccessSFX);
            LanderControlsUIManager.GetLanderMissioonPassUI().SetActive(true);
            //StartCoroutine(ShowLanderMissionPassUI());

            PlayDoorOpeningAnimation(landingPoint);
            hasLandedSafely = true;
        }
    }

    private IEnumerator ShowLanderMissionPassUI()
    {
        LanderControlsUIManager.GetLanderMissioonPassUI().SetActive(true);

        yield return new WaitForSeconds(2f);

        LanderControlsUIManager.GetLanderMissioonPassUI().SetActive(false);
    }

    private void PlayDoorOpeningAnimation(Vector3 landingPoint)
    {
        landerAnimation.getDoorAnimation(landingPoint);
    }
}
