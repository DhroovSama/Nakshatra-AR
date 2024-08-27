using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionChecker : MonoBehaviour
{
    [SerializeField]
    private VibrationController vibrationController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("Camera entered");

            vibrationController.VibratePhone_Light();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("Camera exited");

            vibrationController.VibratePhone_Light();
        }
    }
}
