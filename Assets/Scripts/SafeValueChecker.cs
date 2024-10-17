using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SafeValueChecker : MonoBehaviour
{
    [SerializeField]
    private LanderMechanics landerMechanics;

    [SerializeField]
    private TextMeshProUGUI fuelValue, thrustValue, altitudeValue;

    [SerializeField]
    private float fuel, thrust, altitude;

    private void Update()
    {
        fuel = landerMechanics.CurrentFuel;
        thrust = landerMechanics.DescendVelocityValue;
        //altitude = ConvertTextToFloat(altitudeValue);

        UpdateFuelColor(fuel);
        UpdateThrustColor(thrust);
    }

    private float ConvertTextToFloat(TextMeshProUGUI textMeshPro)
    {
        if (string.IsNullOrEmpty(textMeshPro.text))
        {
            Debug.LogWarning("The TextMeshProUGUI field is empty.");
            return 0f;
        }

        if (float.TryParse(textMeshPro.text, out float result))
        {
            return result;
        }
        else
        {
            Debug.LogError("Failed to convert the text to a float.");
            return 0f;
        }
    }

    private void UpdateFuelColor(float fuel)
    {
        if (fuel < 20f)
        {
            fuelValue.color = Color.red;
        }
        else if (fuel < 60f)
        {
            fuelValue.color = Color.yellow;
        }
        else
        {
            fuelValue.color = Color.green; // Default color
        }
    }

    private void UpdateThrustColor(float thrust)
    {
        if (thrust < -2f)
        {
            thrustValue.color = Color.red;
        }
        else if (thrust < -1f)
        {
            thrustValue.color = Color.yellow;
        }
        else
        {
            thrustValue.color = Color.green;
        }
    }
}
