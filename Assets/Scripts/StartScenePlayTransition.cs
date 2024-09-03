using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScenePlayTransition : MonoBehaviour
{
    [SerializeField]
    private SceneTransition sceneTransition;

    [SerializeField]
    private Animator animator;
    [SerializeField]    
    private AnimationClip clip;    

    private static bool hasTransitioned = false;

    void Start()
    {
        if (!hasTransitioned)
        {
            //sceneTransition.SceneLoadTransition_Start();

            animator.SetTrigger("Start_MenuScreenLoad");

            hasTransitioned = true;
        }
        else
        {
            animator.Play("FinalState_MenuScene");
        }
    }

    public void OnAnimationEnd()
    {
        animator.enabled = false;
    }
}
