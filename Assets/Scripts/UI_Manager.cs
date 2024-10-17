using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private PlayerHasArrivedCheckTrigger playerHasArrivedCheckTrigger;
    [Space]

    [SerializeField]
    private GameObject landerPointingArrow;

    [Header("Countdown Seconds")]
    [Tooltip("Seconds countdown after which the lander is spawned")]
    [SerializeField] private int secondsToSpawn = 6;

    [Space]
    [SerializeField] private bool isSpawningCountdownFinished = false;

    [Header("Lander Spawner Settings")]
    [SerializeField] private GameObject landerSpawnerButtonContainer;
    public GameObject LanderSpawnerButtonContainer
    {
        get { return landerSpawnerButtonContainer; }
        set { landerSpawnerButtonContainer = value; }
    }

    [SerializeField] private Button landerSpawnerButton;

    [Header("Lander Spawning Timer")]
    [SerializeField] private GameObject landerSpawningTimerCountdownContainer;
    [SerializeField] private TextMeshProUGUI landerSpawningTimerCountdown;

    public bool testing_isPlayerArrivedAtLandingZone;

    public static UI_Manager Instance { get; private set; }

    [SerializeField]
    private bool hasLanderSpawned;
    public bool HasLanderSpawned
    {
        get { return hasLanderSpawned; }
        private set { hasLanderSpawned = value; }
    }

    [SerializeField]
    private bool isTerrainScannerButtonClickedOnce = false;

    [SerializeField]
    private VoiceOverData dontLandInRedZone_VO;

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip countdownSound;  // Sound for each countdown second

    [SerializeField]
    private AudioClip zeroSound;  // Sound for when the countdown reaches zero

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Optional: if you want the UI_Manager to persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(PlaceOnPlane.IsMoonSurfaceSpawned())
        {
            playerHasArrivedCheckTrigger = FindObjectOfType<PlayerHasArrivedCheckTrigger>();

            if(playerHasArrivedCheckTrigger != null)
            {
                spawnLanderSequence();
            }
            else { Debug.Log("playerHasArrivedCheckTrigger is NULL"); }
        }

        if (landerPointingArrow == null)
        {
            landerPointingArrow = LanderControlsUIManager.GetLanderPointingArrow();
        }
    }

    private IEnumerator LanderSpawningTimer()
    {
        landerSpawningTimerCountdownContainer.SetActive(true);

        while (secondsToSpawn > 0)
        {
            GlobalAudioPlayer.GetPlaySound(countdownSound);  // Play countdown sound

            yield return new WaitForSeconds(1);
            secondsToSpawn--;
            landerSpawningTimerCountdown.text = secondsToSpawn.ToString();
        }

        GlobalAudioPlayer.GetPlaySound(zeroSound);  // Play sound for zero

        EnableLanderPointingArrow();

        isSpawningCountdownFinished = true;

        landerSpawningTimerCountdownContainer.SetActive(false);

        SpawnLander();
    }


    private void EnableLanderPointingArrow()
    {
        //enable lander pointing arrow

        landerPointingArrow.SetActive(true);
    }

    public void EnableLander_Controls_Info_UI()
    {
        landerPointingArrow.SetActive(false);

        LanderControlsUIManager.GetLanderInfoUIContainer().SetActive(true);
        LanderControlsUIManager.GetLanderControlsUIContainer().SetActive(true);
    }

    private void SpawnLander()
    {
        if (isSpawningCountdownFinished)
        {
            //Debug.Log("lander spawned");
            landerSpawnerButtonContainer.SetActive(false);
            LanderMechanics.getSpawnLanderOnMoonSurface();

            hasLanderSpawned = true;
        }
    }

    //method called through LAUNCH button
    public void startLanderSpawningTimer()
    {
        StartCoroutine(LanderSpawningTimer());
    }

    private void landerSpawnerButtonSequence()
    {
        //landerSpawnerButtonContainer.SetActive(true);

        landerSpawnerButton.interactable = true;

        ColorBlock colors = landerSpawnerButton.colors;

        colors.normalColor = Color.green;

        landerSpawnerButton.colors = colors;
    }

    private void spawnLanderSequence()
    {
        //enable when the player has walked and landed where we wanted him to

        testing_isPlayerArrivedAtLandingZone = PlayerHasArrivedCheckTrigger.get_IsPlayerArrivedAtLandingZone();

        if (testing_isPlayerArrivedAtLandingZone)
        {
            enableLanderSpawningUI();
        }
    }

    private void enableLanderSpawningUI()
    {
        landerSpawnerButtonSequence();
    }

    public void LandingButtonDisableFor_TerrainScannerButton()
    {
        if (!isTerrainScannerButtonClickedOnce)
        {
            VoiceOverManager.Instance.TriggerVoiceOver(dontLandInRedZone_VO);

            landerSpawnerButtonContainer.SetActive(true); 

            LanderControlsUIManager.getTerrainScannerControlButton().SetActive(false); 

            isTerrainScannerButtonClickedOnce = true;
        }
    }

}
