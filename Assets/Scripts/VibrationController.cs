using UnityEngine;
using CandyCoded;
using CandyCoded.HapticFeedback;

[CreateAssetMenu(fileName = "VibrationFeedback", menuName = "ScriptableObjects/VibrationFeedback")]
public class VibrationController : ScriptableObject
{
    public void VibratePhone_Light()
    {
        HapticFeedback.LightFeedback();
    }

    public void VibratePhone_Medium()
    {
        HapticFeedback.MediumFeedback();
    }

    public void VibratePhone_Heavy()
    {
        HapticFeedback.HeavyFeedback();
    } 
}
