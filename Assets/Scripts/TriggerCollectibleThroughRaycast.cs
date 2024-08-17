using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCollectibleThroughRaycast : MonoBehaviour
{
    public static TriggerCollectibleThroughRaycast Instance { get; private set; }

    [SerializeField]
    private VibrationController vibration;  // Reference to the VibrationController

    [SerializeField]
    private GameObject collectiblesProgressBarGameobject;

    [SerializeField]
    private Slider collectiblesProgressBar;

    [SerializeField]
    private float raycastRange = 10f;

    [SerializeField]
    private float fillDuration = 2f;

    [SerializeField]
    private float resetDelay = 0.5f;

    [SerializeField]
    private float durationForLoactionRevealUI = 3f;

    [SerializeField]
    private TriggerTerrainScanner triggerTerrainScanner;

    [SerializeField]
    private Button enableTerrainScannerButton;

    [SerializeField]
    private CollectibleManager collectibleManager;

    [SerializeField]
    public GameObject CurrentTargetCollectible;

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
