using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScenePlayTransition : MonoBehaviour
{
    [SerializeField]
    private SceneTransition sceneTransition;

    void Start()
    {
        sceneTransition.SceneLoadTransition_Start();
    }

}
