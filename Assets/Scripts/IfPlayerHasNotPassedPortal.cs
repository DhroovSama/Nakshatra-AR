using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPlayerHasNotPassedPortal : MonoBehaviour
{
    [SerializeField, Tooltip("Auto Assigned")]
    private PortalDoor portalDoor;

    private bool isTimerRunning = false;
    private bool hasLoggedPortalPassed = false;

    [SerializeField]
    private float timer = 0f;

    [SerializeField]
    private VoiceOverData enterPortalDoor;

    [SerializeField, Tooltip("How much time to wait after the portal has spawned before playing the voice over"), Range(1f, 40f)]
    private float timeToWait;

    private bool hasTimerStarted = false;  // New flag to track if the timer has started before

    private void Update()
    {
        if (PlaceOnPlane.IsMoonSurfaceSpawned())
        {
            if (portalDoor == null)
            {
                portalDoor = FindObjectOfType<PortalDoor>();
            }

            if (!hasTimerStarted)
            {
                hasTimerStarted = true;
                isTimerRunning = true;
                timer = 0f;
                hasLoggedPortalPassed = false;  // Reset the flag only once
            }
        }

        if (isTimerRunning)
        {
            if (portalDoor != null && portalDoor.IsPortalPassed)
            {
                isTimerRunning = false;

                if (!hasLoggedPortalPassed)
                {
                    Debug.Log("Player has passed the portal. Timer stopped.");
                    hasLoggedPortalPassed = true;  // Set the flag to true to prevent further logging
                }
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
