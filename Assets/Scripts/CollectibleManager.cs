using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField]
    private DisplayFactsWhenTriggered displayFactsScript;

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
            displayFactsScript = collectible.GetComponent<DisplayFactsWhenTriggered>();
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
    }

    public void DisableCollectible(GameObject collectible)
    {
        if (collectibleState.ContainsKey(collectible))
        {
            collectibleState[collectible] = false;
            collectible.SetActive(false);
        }
    }

    public bool IsCollectibleActive(GameObject collectible)
    {
        return collectibleState.ContainsKey(collectible) && collectibleState[collectible];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rover") || other.CompareTag("MainCamera"))
        {
            foreach (var collectible in collectibles)
            {
                if (collectible.activeSelf && Vector3.Distance(other.transform.position, collectible.transform.position) < 1.0f)
                {
                    DisableCollectible(collectible);
                }
            }
        }
    }
}
