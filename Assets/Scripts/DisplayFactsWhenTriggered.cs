using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DisplayFactsWhenTriggered : MonoBehaviour
{
    [SerializeField]
    private GameObject factToDisplay;

    [SerializeField]
    private RawImage factToDisplay_RawImage;

    [SerializeField]
    private GameObject factCylinder;

    [SerializeField]
    private UnityEvent onFactEnabled;

    public event Action onFactEnabledAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rover") || other.gameObject.CompareTag("MainCamera"))
        {
            factToDisplay.SetActive(true);


            if(factToDisplay.activeSelf)
            {
                FactToDisplayToGlobalFactDisplayer(factToDisplay_RawImage.texture);

                onFactEnabled.Invoke();

                if(onFactEnabledAction != null)
                {
                    onFactEnabledAction();
                }

            }
            else { Debug.Log("factToDisplay is setActive is false"); }


            factCylinder.SetActive(false);

            FactsCollectedGlobal.getIncrementFactsCollected(1);
        }
    }

    private void FactToDisplayToGlobalFactDisplayer(Texture rawImage)
    {
        Texture displayTexture = factToDisplay_RawImage.GetComponent<RawImage>().texture;

        SharedGameManagerEventSystem.TriggerNewFactToDisplay(displayTexture);
    }

}
