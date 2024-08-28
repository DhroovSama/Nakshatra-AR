using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionChecker : MonoBehaviour
{
    [SerializeField]
    private VibrationController vibrationController;

    [SerializeField]
    [Space]
    private GameObject informationCanvases;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("Camera entered");

            vibrationController.VibratePhone_Light(); 
             
            informationCanvases.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("Camera exited");

            vibrationController.VibratePhone_Light();

            informationCanvases.SetActive(false);
        }
    }
}
