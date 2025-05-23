using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUIProvider : MonoBehaviour
{
    private static GlobalUIProvider instance;

    [Header("UI Elements")]

    [SerializeField]
    private Button resetObejctButton;

    [SerializeField]
    private Button placeButton;

    private void Awake()
    {
        instance = this;    
    }

    public static Button getResetObejctButton()
    {
        return instance.resetObejctButton;
    }

    public static Button getPlaceButton()
    {
        return instance.placeButton;    
    }
}
