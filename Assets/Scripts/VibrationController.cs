using UnityEngine;
using CandyCoded;
using CandyCoded.HapticFeedback;
using System;

[CreateAssetMenu(fileName = "VibrationFeedback", menuName = "ScriptableObjects/VibrationFeedback")]
public class VibrationController : ScriptableObject
{
    public void VibratePhone_Light()
    {
        try
        {
            HapticFeedback.LightFeedback();
        }
        catch (Exception)
        {
            // Ignore exceptions
        }
    }

    public void VibratePhone_Medium()
    {
        try
        {
            HapticFeedback.MediumFeedback();
        }
        catch (Exception)
        {
            // Ignore exceptions
        }
    }

    public void VibratePhone_Heavy()
    {
        try
        {
            HapticFeedback.HeavyFeedback();
        }
        catch (Exception)
        {
            // Ignore exceptions
        }
    }
}
