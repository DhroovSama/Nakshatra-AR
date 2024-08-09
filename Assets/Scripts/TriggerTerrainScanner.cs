using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerTerrainScanner : MonoBehaviour
{
    [SerializeField]
    private TerrainScanner terrainScanner;

    [SerializeField]
    private TerrainTextureChanger terrainTextureChanger;

    [SerializeField]
    private Button EnableTerrainScanner;

    //[SerializeField]
    //private Slider terrainScannedCompletionSlider;

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

    private void Start()
    {
        EnableTerrainScanner.onClick.AddListener(() => TriggerTerrainScanEffect());

        terrainNoLandZoneMaterial.SetFloat("_Fade", 1);
        terrainNoLandZoneMaterial.SetInt("_RimFalloff", 0);

        //terrainScannedCompletionSlider.maxValue = sliderMaxValue;
    }

    private void Update()
    {
        if (PlaceOnPlane.IsMoonSurfaceSpawned())
        {
            terrainScanner = FindObjectOfType<TerrainScanner>();
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
            //terrainScannedCompletionSlider.value = terrainPulseCount;

            DisplayNoLandingZones();

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

        // Initial values
        float initialFade = 1f;
        float finalFade = 0.5f;
        float initialRimFalloff = 0f;
        float finalRimFalloff = 1f;

        // Set initial values
        terrainNoLandZoneMaterial.SetFloat("_Fade", initialFade);
        terrainNoLandZoneMaterial.SetFloat("_RimFalloff", initialRimFalloff);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Lerp values
            float fadeValue = Mathf.Lerp(initialFade, finalFade, t);
            float rimFalloffValue = Mathf.Lerp(initialRimFalloff, finalRimFalloff, t);

            terrainNoLandZoneMaterial.SetFloat("_Fade", fadeValue);
            terrainNoLandZoneMaterial.SetInt("_RimFalloff", Mathf.RoundToInt(rimFalloffValue));

            yield return null;
        }

        // Ensure final values are set
        terrainNoLandZoneMaterial.SetFloat("_Fade", finalFade);
        terrainNoLandZoneMaterial.SetFloat("_RimFalloff", finalRimFalloff);

        yield return new WaitForSeconds(0.5f);

        ////Add Sound Later when the texture changes to indicate landing
        //terrainTextureChanger.ChangeTexture_LandingZone();
    }

    public void DisableNoLandingZones()
    {
        GameObject noLandingZones = GameObject.FindGameObjectWithTag("No Landing Zones");

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

        // Cooldown phase
        float cooldownStartTime = Time.time;
        while (Time.time < cooldownStartTime + 2f) // Cooldown lasts 2 seconds
        {
            float t = (Time.time - cooldownStartTime) / 2f;
            terrainNoLandZoneMaterial.SetFloat("_RimFalloff", Mathf.Lerp(1, 0, t));
            terrainNoLandZoneMaterial.SetFloat("_Fade", Mathf.Lerp(0.5f, 1, t));
            yield return null;
        }

        
        yield return new WaitForSeconds(0.5f);

        //Add Sound When Landing area visual cue Reverted back
        terrainTextureChanger.ChangeTexture_NormalTerrain();
    }

    #region Needed if you want the no landing zone to fade in and fade out after some time
    //private IEnumerator DisplayLandingAreasCoroutine()
    //{
    //    terrainNoLandZoneMaterial.SetFloat("_Fade", 0.5f);
    //    terrainNoLandZoneMaterial.SetInt("_RimFalloff", 1);

    //    float rimStartTime = Time.time;
    //    while (Time.time < rimStartTime + rimFalloffDuration)
    //    {
    //        float t = (Time.time - rimStartTime) / rimFalloffDuration;
    //        terrainNoLandZoneMaterial.SetFloat("_RimFalloff", Mathf.Lerp(terrainNoLandZoneMaterial.GetInt("_RimFalloff"), 1, t));
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(0.25f); // Short delay before starting the fade

    //    float fadeDelay = 0.1f;
    //    float fadeStartTime = Time.time + fadeDelay;
    //    while (Time.time < fadeStartTime + 1f)
    //    {
    //        float t = (Time.time - fadeStartTime) / 1f;
    //        terrainNoLandZoneMaterial.SetFloat("_Fade", Mathf.Lerp(1, 0.5f, t));
    //        yield return null;
    //    }

    //    // Cooldown phase
    //    float cooldownStartTime = Time.time;
    //    while (Time.time < cooldownStartTime + 2f) // Cooldown lasts 2 seconds
    //    {
    //        float t = (Time.time - cooldownStartTime) / 2f;
    //        terrainNoLandZoneMaterial.SetFloat("_RimFalloff", Mathf.Lerp(1, 0, t));
    //        terrainNoLandZoneMaterial.SetFloat("_Fade", Mathf.Lerp(0.5f, 1, t));
    //        yield return null;
    //    }
    //}
    #endregion

}
