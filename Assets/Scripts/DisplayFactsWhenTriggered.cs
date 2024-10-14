using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DisplayFactsWhenTriggered : MonoBehaviour
{
    [SerializeField]
    private GameObject factToDisplay;

    [SerializeField]
    private RawImage factToDisplay_RawImage;

    //[SerializeField]
    //private GameObject Collider;

    [SerializeField]
    private UnityEvent onFactEnabled;

    public event Action onFactEnabledAction;

    private CollectibleManager collectibleManager;

    [SerializeField]
    private UISoundSO UISoundSO;

    [SerializeField]
    private VibrationController vibrationController;

    private void Start()
    {
        collectibleManager = FindObjectOfType<CollectibleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rover") || other.gameObject.CompareTag("MainCamera"))
        {
            UISoundSO.PlayFactCollectedSound();
            vibrationController.VibratePhone_Medium();


            factToDisplay.SetActive(true);

            if (factToDisplay.activeSelf)
            {
                FactToDisplayToGlobalFactDisplayer(factToDisplay_RawImage.texture);

                onFactEnabled.Invoke();

                onFactEnabledAction?.Invoke(); // Trigger the event for the CollectibleManager

                if (collectibleManager != null)
                {
                    collectibleManager.OnRoverCollision(gameObject); // Correct method call
                }
            }
            else { Debug.Log("factToDisplay is setActive is false"); }

            //Collider.SetActive(false);

            FactsCollectedGlobal.getIncrementFactsCollected(1);
        }
    }

    private void FactToDisplayToGlobalFactDisplayer(Texture rawImage)
    {
        Texture displayTexture = factToDisplay_RawImage.GetComponent<RawImage>().texture;

        SharedGameManagerEventSystem.TriggerNewFactToDisplay(displayTexture);
    }
}
