using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeHandler_PSLV : MonoBehaviour
{
    [SerializeField]
    private FadeHandler fadeHandler;

    private void Awake()
    {
        fadeHandler = FindObjectOfType<FadeHandler>();
    }

    public void Trigger_FadeIn()
    {
        fadeHandler.FadeIn();
    }

    public void Trigger_FadeOut()
    {
        fadeHandler.FadeOut();
    }

    public void DestroyPSLV()
    {
        this.gameObject.SetActive(false);

        Destroy(gameObject);
    }
}
