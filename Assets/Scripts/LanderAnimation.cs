using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderAnimation : MonoBehaviour
{
    [SerializeField] private LanderMechanics landerMechanics;
    [SerializeField] private UI_Manager ui_Manager;
    [SerializeField] private LanderAnimEventsHandler landerAnimEventsHandler;

    [Space]

    [SerializeField] private GameObject LanderParent;

    [SerializeField] private AnimationClip LandingAnimation;
    [SerializeField] private AnimationClip DoorAnimation;

    [Header("Animator Component")]
    [SerializeField] private Animator landerAnimator;

    [Header("Animations Timer")]
    [SerializeField] private float playDurationOfLandingAnim, playDurationOfDoorAnim, playDurationOfRoverAnim = 0.0f;

    [SerializeField] private bool isDoorAnimEnded = false;

    private bool isDoorAnimationPlaying = false;

    [SerializeField]
    [Tooltip("offset for the door animation start position")]
    private float offsetForLanderDoorAnim = 0.5f;

    [SerializeField]
    private Rigidbody roverRB;

    [SerializeField]
    private bool isRoverAnimEnded = false;
    public bool IsRoverAnimEnded { get { return isRoverAnimEnded; } }

    //[SerializeField]
    //private GameObject doorAnimationStartPoint;

    private void Update()
    {
        //if (doorAnimationStartPoint == null && landerMechanics.IsLanderInstanceSpawned)
        //{
        //    doorAnimationStartPoint = FindObjectOfType<LanderCollisionHandler>().gameObject;
        //}

        if (landerAnimEventsHandler == null && ui_Manager.HasLanderSpawned)
        {
            landerAnimEventsHandler = FindObjectOfType<LanderAnimEventsHandler>();
            LanderParent = FindObjectOfType<LanderAnimEventsHandler>().gameObject;
        }

        if(roverRB == null && landerMechanics.IsLanderInstanceSpawned)
        {
            roverRB = FindObjectOfType<FindRoverByScript>().gameObject.GetComponentInChildren<Rigidbody>();
        }
    }

    public void getLanderInstanceAnimatorComponent()
    {
        landerAnimator = landerMechanics.LanderInstance.GetComponent<Animator>();
    }

    public void getLandingAnimation()
    {
        StartCoroutine(playLandingAnimation());
    }

    private IEnumerator playLandingAnimation()
    {
        DisableLanderRB();

        landerAnimator.SetBool("LandingAnim", true);

        yield return new WaitForSeconds(playDurationOfLandingAnim);

        landerAnimator.SetBool("LandingAnim", false);

        landerMechanics.LanderThrustSlider.value = 0.1f;        
    }

    public void OnLandingAnimationEnd()
    {
        Debug.Log("OnLandingAnimationEnd() running");
        landerMechanics.LanderThrustSlider.value = 0.1f;
        DisableLanderAnimator();
        EnableLanderRB();
    }

    private void ModifyDoorNAimationStartingPoint()
    {
        
    }

    public void getDoorAnimation(Vector3 landingPoint) // Modified to accept a Vector3 parameter
    {
        if (!isDoorAnimationPlaying)
        {
            StartCoroutine(playDoorAnimation(landingPoint)); // Pass the landing point to the coroutine
        }
    }

    private IEnumerator playDoorAnimation(Vector3 landingPoint)
    {
        isDoorAnimationPlaying = true;
        DisableLanderRB();
        if (landerAnimEventsHandler.RoverPrefab)
        {
            landerAnimEventsHandler.RoverPrefab.SetActive(true);
        }
        else { Debug.Log("Rover NO"); }

        //// Set the door animation's position to the landing point
        //doorAnimationStartPoint.transform.position = landingPoint;

        Debug.Log(landingPoint);

        landingPoint.y += offsetForLanderDoorAnim; 

        LanderParent.transform.position = landingPoint;

        Debug.Log("new: " + landingPoint);

        EnableLanderAnimator();
        landerAnimator.SetBool("DoorAnim", true);
        yield return new WaitForSeconds(playDurationOfDoorAnim);
        landerAnimator.SetBool("DoorAnim", false);
        isDoorAnimationPlaying = false;
    }


    public void OnDoorAnimationEnd()
    {
        landerAnimator.SetBool("DoorAnim", false);

        Debug.Log("OnDoorAnimationEnd() running");
        //DisableLanderAnimator();
        DisableLanderRB();

        isDoorAnimEnded = true;

        //StartCoroutine(PlayRoverAnimationAfterSomeTime());

        if (isDoorAnimEnded)
        {
            Debug.Log("hehehehhe");
            EnableLanderAnimator();

            StartCoroutine(playRoverAnimation());
        }
    }

    private IEnumerator playRoverAnimation()
    {
        //EnableLanderAnimator();

        landerAnimator.SetBool("RoverAnim", true);

        yield return new WaitForSeconds(playDurationOfRoverAnim);

        landerAnimator.SetBool("RoverAnim", false);
    }

    //private IEnumerator PlayRoverAnimationAfterSomeTime()
    //{
    //    if (isDoorAnimEnded)
    //    {
    //        Debug.Log("hehehehhe");
    //        EnableLanderAnimator();
    //    }
    //    else { Debug.Log("isDoorAnimEnded = false"); }

    //    yield return new WaitForSeconds(2f);
    //    StartCoroutine(playRoverAnimation());
    //}

    public void OnRoverAnimationEnd()
    {
        DisableLanderAnimator();
        DisableLanderRB();

        StartCoroutine( EnableLanderRBAndControls());
    }

    private IEnumerator EnableLanderRBAndControls()
    {
        //add enable rover rb and also hand here the user the controls of the rover
        isRoverAnimEnded = true;

        yield return new WaitForSeconds(1.5f);

        roverRB.isKinematic = false;
    }

    private void EnableLanderAnimator()
    {
        landerAnimator.enabled = true;
    }

    private void DisableLanderAnimator()
    {
        landerAnimator.enabled = false;
    }

    private void EnableLanderRB()
    {
        landerMechanics.LanderRB.isKinematic = false;
    }
    private void DisableLanderRB()
    {
        landerMechanics.LanderRB.isKinematic = true;
    }
}
