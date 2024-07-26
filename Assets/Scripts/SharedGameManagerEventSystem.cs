using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SharedGameManagerEventSystem : MonoBehaviour
{
    public static event Action<Texture> OnNewFactToDisplay;

    public static void TriggerNewFactToDisplay(Texture factTexture)
    {
        OnNewFactToDisplay?.Invoke(factTexture);
    }
}
