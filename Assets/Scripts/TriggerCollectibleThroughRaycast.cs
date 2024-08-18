using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCollectibleThroughRaycast : MonoBehaviour
{
    public static TriggerCollectibleThroughRaycast Instance { get; private set; }

    [Header("Vibration Settings")]
    [Tooltip("Reference to the VibrationController SO for handling haptic feedback.")]
    [SerializeField]
    private VibrationController vibration;

    [SerializeField]
    private LocationButtonUIProvider locationButtonUIProvider;

    [Header("UI Elements")]
    [Tooltip("Progress bar UI element to show collectible progress.")]
    [SerializeField]
    private GameObject collectiblesProgressBarGameobject;

    [Tooltip("Slider for the collectible progress bar.")]
    [SerializeField]
    private Slider collectiblesProgressBar;

    [Header("Raycast Settings")]
    [Tooltip("The range of the raycast for detecting collectibles.")]
    [SerializeField]
    private float raycastRange = 10f;

    [Tooltip("Duration to fill the progress bar.")]
    [SerializeField]
    private float fillDuration = 2f;

    [Tooltip("Delay before resetting the progress bar.")]
    [SerializeField]
    private float resetDelay = 0.5f;

    [Tooltip("Duration for the location reveal UI to be visible.")]
    [SerializeField]
    private float durationForLoactionRevealUI = 3f;

    [Header("References to Other Scripts")]
    [Tooltip("Reference to the TriggerTerrainScanner script.")]
    [SerializeField]
    private TriggerTerrainScanner triggerTerrainScanner;

    [Tooltip("Button to enable the terrain scanner.")]
    [SerializeField]
    private Button enableTerrainScannerButton;

    [Tooltip("Reference to the CollectibleManager script.")]
    [SerializeField]
    private CollectibleManager collectibleManager;

    [Header("Voice Over Data")]
    [Tooltip("VoiceOverData for Element VO")]
    [SerializeField]
    private VoiceOverData Collectible_1_vo;

    [Tooltip("VoiceOverData for Lunar Surface Image VO")]
    [SerializeField]
    private VoiceOverData Collectible_2_vo;

    [Tooltip("VoiceOverData for Temprature Data VO")]
    [SerializeField]
    private VoiceOverData Collectible_3_vo;

    [Tooltip("VoiceOverData for Topographical Data VO")]
    [SerializeField]
    private VoiceOverData Collectible_4_vo;

    [Tooltip("VoiceOverData for South Pole VO")]
    [SerializeField]
    private VoiceOverData Collectible_5_vo;

    [Space(10)]
    [Tooltip("The current collectible targetted by the raycast.")]
    [SerializeField]
    public GameObject CurrentTargetCollectible;

    [Space]
    [SerializeField]
    private List<GameObject> collectible_1_Element, collectible_2_LunarSurfaceImages, collectible_3_TempratureData, collectible_4_TopographicalData, collectible_5_SouthPole;

    private bool isFilling = false;
    private bool shouldRaycast = false;
    private Coroutine fillCoroutine = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of TriggerCollectibleThroughRaycast detected! Destroying extra instance.");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
        if (PlaceOnPlane.IsMoonSurfaceSpawned())
        {
            collectibleManager = FindObjectOfType<CollectibleManager>();

            locationButtonUIProvider = FindObjectOfType<LocationButtonUIProvider>();

            if(locationButtonUIProvider != null )
            {
                collectible_1_Element = locationButtonUIProvider.GetCollectible_1_Element();
                collectible_2_LunarSurfaceImages = locationButtonUIProvider.GetCollectible_2_LunarSurfaceImages();
                collectible_3_TempratureData = locationButtonUIProvider.GetCollectible_3_TempratureDatas();
                collectible_4_TopographicalData = locationButtonUIProvider.GetCollectible_4_TopographicalData();
                collectible_5_SouthPole = locationButtonUIProvider.GetCollectible_5_SouthPole();
            }
        }

        if (shouldRaycast)
        {
            PerformRaycast();
        }
    }

    private void PerformRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastRange))
        {
            if (IsCollectible(hit.collider.tag) && collectibleManager.IsCollectibleActive(hit.collider.gameObject))
            {
                if (hit.collider.gameObject != CurrentTargetCollectible)
                {
                    // If raycast hits a different collectible or the same one for the first time
                    CurrentTargetCollectible = hit.collider.gameObject;
                    RestartFilling();
                }
            }
            else
            {
                // Raycast is not hitting a collectible, reset progress
                StopFillingAndReset();
            }
        }
        else
        {
            // Raycast is not hitting anything, reset progress
            StopFillingAndReset();
        }
    }

    private void RestartFilling()
    {
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }
        fillCoroutine = StartCoroutine(FillCollectiblesProgressBar(CurrentTargetCollectible));

        // Trigger light vibration when the progress bar starts to fill
        vibration.VibratePhone_Light();
    }

    private void StopFillingAndReset()
    {
        if (isFilling)
        {
            StopCoroutine(fillCoroutine);
            isFilling = false;
        }
        CurrentTargetCollectible = null;

        // Reset the progress bar with a delay
        StartCoroutine(ResetProgressBarAfterDelay());
    }

    private IEnumerator ResetProgressBarAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);
        if (!shouldRaycast || CurrentTargetCollectible == null)
        {
            collectiblesProgressBar.value = 0;
        }
    }

    public void StartRaycasting()
    {
        shouldRaycast = true;
        collectiblesProgressBarGameobject.SetActive(true);
        StartCoroutine(DisableProgressBarAfterDuration());
    }

    public void StopRaycasting()
    {
        shouldRaycast = false;
        StopFillingAndReset();
    }

    private IEnumerator DisableProgressBarAfterDuration()
    {
        yield return new WaitForSeconds(durationForLoactionRevealUI);
        collectiblesProgressBarGameobject.SetActive(false);
    }

    private bool IsCollectible(string tag)
    {
        return tag == "Collectible 1" || tag == "Collectible 2" || tag == "Collectible 3" ||
               tag == "Collectible 4" || tag == "Collectible 5";
    }

    private void HandleCollectibleInteraction(GameObject collectible)
    {
        switch (collectible.tag)
        {
            case "Collectible 1":
                HandleCollectible_1_Element();
                break;
            case "Collectible 2":
                HandleCollectible_2_LunarSurfaceImage();
                break;
            case "Collectible 3":
                HandleCollectible_3_TempratureData();
                break;
            case "Collectible 4":
                HandleCollectible_4_TopographicalData();
                break;
            case "Collectible 5":
                HandleCollectible_5_SouthPole();
                break;
            default:
                Debug.LogWarning("Unknown collectible tag: " + collectible.tag);
                break;
        }
    }

    private void HandleCollectible_1_Element()
    {
        Debug.Log("Handling interaction for Collectible 1");

        VoiceOverManager.Instance.TriggerVoiceOver(Collectible_1_vo);

        foreach(GameObject collectibles in collectible_1_Element)
        {
            collectibles.SetActive(true);
        }
    }

    private void HandleCollectible_2_LunarSurfaceImage()
    {
        Debug.Log("Handling interaction for Collectible 2");

        VoiceOverManager.Instance.TriggerVoiceOver(Collectible_2_vo);

        foreach (GameObject collectibles in collectible_2_LunarSurfaceImages)
        {
            collectibles.SetActive(true);
        }
    }

    private void HandleCollectible_3_TempratureData()
    {
        Debug.Log("Handling interaction for Collectible 3");

        VoiceOverManager.Instance.TriggerVoiceOver(Collectible_3_vo);

        foreach (GameObject collectibles in collectible_3_TempratureData)
        {
            collectibles.SetActive(true);
        }
    }

    private void HandleCollectible_4_TopographicalData()
    {
        Debug.Log("Handling interaction for Collectible 4");

        VoiceOverManager.Instance.TriggerVoiceOver(Collectible_4_vo);

        foreach (GameObject collectibles in collectible_4_TopographicalData)
        {
            collectibles.SetActive(true);
        }
    }

    private void HandleCollectible_5_SouthPole()
    {
        Debug.Log("Handling interaction for Collectible 5");

        VoiceOverManager.Instance.TriggerVoiceOver(Collectible_5_vo);

        foreach (GameObject collectibles in collectible_5_SouthPole)
        {
            collectibles.SetActive(true);
        }
    }

    private IEnumerator FillCollectiblesProgressBar(GameObject collectible)
    {
        isFilling = true;
        float elapsedTime = 0f;

        while (elapsedTime < fillDuration)
        {
            if (CurrentTargetCollectible != collectible)
            {
                isFilling = false;
                yield break;
            }

            elapsedTime += Time.deltaTime;
            collectiblesProgressBar.value = Mathf.Lerp(0, 1, elapsedTime / fillDuration);
            yield return null;
        }

        // Trigger medium vibration when the progress bar is completely filled
        vibration.VibratePhone_Medium();

        collectiblesProgressBar.value = 1;
        collectible.SetActive(true);

        // Set the target collectible in TriggerTerrainScanner
        triggerTerrainScanner.targetCollectible = collectible;

        // Handle the specific collectible interaction
        HandleCollectibleInteraction(collectible);

        // Disable all other collectibles except the current target
        DisableOtherCollectibles(collectible);

        enableTerrainScannerButton.interactable = false;

        isFilling = false;
    }

    private void DisableOtherCollectibles(GameObject collectible)
    {
        foreach (GameObject obj in triggerTerrainScanner.ListContainCollectibleGameobjects)
        {
            if (obj != collectible)
            {
                obj.SetActive(false);
            }
        }
    }
}
