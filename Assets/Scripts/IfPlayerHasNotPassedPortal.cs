using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPlayerHasNotPassedPortal : MonoBehaviour
{
    [SerializeField,Tooltip("Auto Assigned")]
    private PortalDoor portalDoor;

    private bool isTimerRunning = false;

    [SerializeField]
    private float timer = 0f;

    [SerializeField]
    private VoiceOverData enterPortalDoor;

    [SerializeField, Tooltip("how much time to waiiit after the portal has spawned before playing the vopice over"), Range(1f,40f)]
    private float timeToWait;

    private void Update()
    {
        if (PlaceOnPlane.IsMoonSurfaceSpawned())
        {
            if (portalDoor == null)
            {
                portalDoor = FindObjectOfType<PortalDoor>();
            }

            if (!isTimerRunning)
            {
                isTimerRunning = true;
                timer = 0f;
            }
        }

        if (isTimerRunning)
        {
            if (portalDoor != null && portalDoor.IsPortalPassed)
            {
                isTimerRunning = false;
                Debug.Log("Player has passed the portal. Timer stopped.");
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= timeToWait)
                {
                    Debug.Log("20 seconds have passed and the player has not passed the portal.");
                    VoiceOverManager.Instance.TriggerVoiceOver(enterPortalDoor);

                    isTimerRunning = false; 
                }
            }
        }
    }
}
