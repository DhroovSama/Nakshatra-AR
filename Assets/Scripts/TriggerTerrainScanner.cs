using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TriggerTerrainScanner : MonoBehaviour
{
    [SerializeField]
    private TerrainScanner terrainScanner;

    [SerializeField]
    private TerrainTextureChanger terrainTextureChanger;

    [SerializeField]
    private LocationButtonUIProvider locationButtonUIProvider;

    [SerializeField]
    private TriggerCollectibleThroughRaycast collectibleRaycast;

    [SerializeField]
    private UI_Manager uiManager;

    [SerializeField]
    private CollectibleManager collectibleManager;

    [SerializeField]
    private Button EnableTerrainScanner;

    [SerializeField]
    private Material terrainNoLandZoneMaterial;

    [SerializeField]
    private AudioClip terrainScanSFX;

    [SerializeField]
    private AudioSource globalAudioPLayer;

    [Space]
    [SerializeField]
    private int terrainPulseCount = 0;

    [SerializeField]
    private int sliderMaxValue = 10;

    [Header("Material Shader Values")]
    [SerializeField]
    private float rimFalloffDuration = 0.75f;

    private bool isScanning = false;

    [Header("Cooldown variables")]
    [SerializeField]
    private float cooldownTime = 1f;
    [SerializeField]
    private float nextCooldownTime = 0f;

    [SerializeField]
    private GameObject noLandingZones;

    public GameObject NoLandingZones { get { return noLandingZones; } }

    [SerializeField]
    [Tooltip("Will be assigned Automatically")]
    public List<GameObject> ListContainCollectibleGameobjects = new List<GameObject>(); // Updated to public to be accessed by TriggerCollectibleThroughRaycast

    [SerializeField]
    [Tooltip("Will be assigned Automatically")]
    private List<RawImage> uiList = new List<RawImage>();

    [SerializeField]
    [Space]
    [Tooltip("Time for which the Location Reveal UI will be visible for when the Terrain Scanner Button is clicked and then disappear again")]
    private float durationForLoactionRevealUI = 0f;

    [SerializeField]
    public GameObject targetCollectible;

    private void Start()
    {
        EnableTerrainScanner.onClick.AddListener(() => TriggerTerrainScanEffect());

        terrainNoLandZoneMaterial.SetFloat("_Fade", 1);
        terrainNoLandZoneMaterial.SetInt("_RimFalloff", 0);
    }

    private void Update()
    {
        if (PlaceOnPlane.IsMoonSurfaceSpawned())
        {
            terrainScanner = FindObjectOfType<TerrainScanner>();

            collectibleManager = FindObjectOfType<CollectibleManager>();

            locationButtonUIProvider = FindObjectOfType<LocationButtonUIProvider>();

            if (locationButtonUIProvider != null)
            {
                ListContainCollectibleGameobjects = locationButtonUIProvider.GetLocationUIListContainGameobjects();
                uiList = locationButtonUIProvider.GetLocationUIList();
            }
            else { Debug.LogWarning("locationButtonUIProvider is null"); }
        }

        DisableNoLandingZones();
    }

    private void OnDestroy()
    {
        EnableTerrainScanner.onClick.RemoveListener(() => TriggerTerrainScanEffect());
    }

    public void TriggerTerrainScanEffect()
    {
        if (Time.time >= nextCooldownTime)
        {
            terrainScanner.SpawnTerrainScanner();
            GlobalAudioPlayer.getPlaySound(terrainScanSFX);
            terrainPulseCount++;

            DisplayNoLandingZones();

            //EnableNextCollectible(); // Enable the next collectible here when scanner is used

            nextCooldownTime = Time.time + cooldownTime;
        }
    }

    private void DisplayNoLandingZones()
    {
        StartCoroutine(DisplayLandingAreasCoroutine());
    }

    private IEnumerator DisplayLandingAreasCoroutine()
    {
        float duration = 1.5f;
        float elapsedTime = 0f;

        float initialFade = 1f;
        float finalFade = 0.5f;
        float initialRimFalloff = 0f;
        float finalRimFalloff = 1f;

        terrainNoLandZoneMaterial.SetFloat("_Fade", initialFade);
        terrainNoLandZoneMaterial.SetFloat("_RimFalloff", initialRimFalloff);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            float fadeValue = Mathf.Lerp(initialFade, finalFade, t);
            float rimFalloffValue = Mathf.Lerp(initialRimFalloff, finalRimFalloff, t);

            terrainNoLandZoneMaterial.SetFloat("_Fade", fadeValue);
            terrainNoLandZoneMaterial.SetInt("_RimFalloff", Mathf.RoundToInt(rimFalloffValue));

            yield return null;
        }

        terrainNoLandZoneMaterial.SetFloat("_Fade", finalFade);
        terrainNoLandZoneMaterial.SetFloat("_RimFalloff", finalRimFalloff);

        yield return new WaitForSeconds(0.5f);
    }

    private void DisableNoLandingZones()
    {
        noLandingZones = GameObject.FindGameObjectWithTag("No Landing Zones");

        if (LanderCollisionHandler.Instance.HasLandedSafely || LanderCollisionHandler.Instance.HadNotLandedSafely)
        {
            StartCoroutine(FadeOutNoLandingZones());

            noLandingZones.SetActive(false);
        }
    }

    private IEnumerator FadeOutNoLandingZones()
    {
        yield return new WaitForSeconds(0.5f);

        float fadeDelay = 0.1f;
        float fadeStartTime = Time.time + fadeDelay;
        while (Time.time < fadeStartTime + 1f)
        {
            float t = (Time.time - fadeStartTime) / 1f;
            terrainNoLandZoneMaterial.SetFloat("_Fade", Mathf.Lerp(1, 0.5f, t));
            yield return null;
        }

        float cooldownStartTime = Time.time;
        while (Time.time < cooldownStartTime + 2f)
        {
            float t = (Time.time - cooldownStartTime) / 2f;
            terrainNoLandZoneMaterial.SetFloat("_RimFalloff", Mathf.Lerp(1, 0, t));
            terrainNoLandZoneMaterial.SetFloat("_Fade", Mathf.Lerp(0.5f, 1, t));
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        terrainTextureChanger.ChangeTexture_NormalTerrain();
    }

    private void EnableNextCollectible()
    {
        foreach (var collectible in ListContainCollectibleGameobjects)
        {
            if (collectibleManager.IsCollectibleActive(collectible))
            {
                collectibleManager.EnableCollectible(collectible); 
                break;
            }
        }
    }

    public void ShowAndHideFactsUIElements()
    {
        if (uiManager.HasLanderSpawned)
        {
            StartCoroutine(ShowAndHideUIElementsCoroutine());

            if (collectibleRaycast != null)
            {
                collectibleRaycast.StartRaycasting();
                StartCoroutine(StopRaycastingAfterDuration());
            }
        }
    }

    private IEnumerator StopRaycastingAfterDuration()
    {
        yield return new WaitForSeconds(durationForLoactionRevealUI);

        if (collectibleRaycast != null)
        {
            collectibleRaycast.StopRaycasting();
        }
    }

    private IEnumerator ShowAndHideUIElementsCoroutine()
    {
        // Enable all collectibles in the list
        foreach (GameObject uiObject in ListContainCollectibleGameobjects)
        {
            if (collectibleManager.IsCollectibleActive(uiObject)) // Enable only those not yet collected
            {
                uiObject.SetActive(true);
            }
        }

        // Gradually show the UI elements
        yield return LerpUIAlpha(1f, 1f);

        // Wait for the specified duration
        yield return new WaitForSeconds(durationForLoactionRevealUI);

        // Gradually hide the UI elements
        yield return LerpUIAlpha(1f, 0f);

        // Disable all collectibles except the one that was looked at
        foreach (GameObject uiObject in ListContainCollectibleGameobjects)
        {
            if (uiObject != targetCollectible && collectibleManager.IsCollectibleActive(uiObject))
            {
                uiObject.SetActive(false);
            }
        }

        // Ensure the target collectible stays active
        if (targetCollectible != null)
        {
            targetCollectible.SetActive(true);

            targetCollectible = null;
        } 
        else
        {
            Debug.LogWarning("targetCollectible is NULL");
        }
    }

    private IEnumerator LerpUIAlpha(float duration, float targetAlpha)
    {
        float elapsedTime = 0f;

        List<float> initialAlphas = new List<float>();
        foreach (RawImage uiImage in uiList)
        {
            initialAlphas.Add(uiImage.color.a);
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            for (int i = 0; i < uiList.Count; i++)
            {
                RawImage uiImage = uiList[i];
                Color newColor = uiImage.color;
                newColor.a = Mathf.Lerp(initialAlphas[i], targetAlpha, t);
                uiImage.color = newColor;
            }

            yield return null;
        }

        for (int i = 0; i < uiList.Count; i++)
        {
            RawImage uiImage = uiList[i];
            Color newColor = uiImage.color;
            newColor.a = targetAlpha;
            uiImage.color = newColor;
        }
    }
}
