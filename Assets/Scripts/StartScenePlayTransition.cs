using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScenePlayTransition : MonoBehaviour
{
    [SerializeField]
    private SceneTransition sceneTransition;

    private static bool hasTransitioned = false;

    void Start()
    {
        if (!hasTransitioned)
        {
            sceneTransition.SceneLoadTransition_Start();
            hasTransitioned = true; 
        }
    }
}
