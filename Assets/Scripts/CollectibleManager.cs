using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField]
    private LanderControlsUIManager landerControlsUIManager;

    [SerializeField]
    private List<GameObject> collectibles;

    [SerializeField]
    private Dictionary<GameObject, bool> collectibleState = new Dictionary<GameObject, bool>();

    private void Start()
    {
        foreach (var collectible in collectibles)
        {
            collectibleState[collectible] = true; // All collectibles are initially active

            // Subscribe to the event in DisplayFactsWhenTriggered
            var displayFactsScript = collectible.GetComponent<DisplayFactsWhenTriggered>();
            if (displayFactsScript != null)
            {
                displayFactsScript.onFactEnabledAction += () => OnCollectibleTriggered(collectible);
            }
        }
    }

    private void OnCollectibleTriggered(GameObject collectible)
    {
        // This method is called when a collectible's fact is triggered
        DisableCollectible(collectible);
        EnableTerrainScanner(); // Only enable the scanner, don't enable the next collectible yet
    }

    public void DisableCollectible(GameObject collectible)
    {
        if (collectibleState.ContainsKey(collectible))
        {
            collectibleState[collectible] = false;
            collectible.SetActive(false);
        }
    }

    public void EnableCollectible(GameObject collectible)
    {
        if (collectibleState.ContainsKey(collectible))
        {
            collectibleState[collectible] = true;
            collectible.SetActive(true);
        }
    }

    public bool IsCollectibleActive(GameObject collectible)
    {
        return collectibleState.ContainsKey(collectible) && collectibleState[collectible];
    }

    public void OnRoverCollision(GameObject collectible)
    {
        // Logic to handle when the rover collides with a collectible
        DisableCollectible(collectible);
        EnableTerrainScanner();  // Enable the terrain scanner button
    }

    private void EnableTerrainScanner()
    {
        LanderControlsUIManager.getTerrainScannerControl().interactable = true;
    }
}
