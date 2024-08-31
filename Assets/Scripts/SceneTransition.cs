using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private SceneLoad sceneLoad;

    [SerializeField]
    private GameObject startingTransition, endingTransition;

    public void StartSceneLoadTransition_Scene_1()
    {
        StartCoroutine(SceneLoadTransition_End_Scene_1());
    }

    public void StartSceneLoadTransition_Scene_2()
    {
        StartCoroutine(SceneLoadTransition_End_Scene_2());
    }

    public void StartSceneLoadTransition_Scene_3()
    {
        StartCoroutine(SceneLoadTransition_End_Scene_3());
    }

    public void StartSceneLoadTransition_Scene_4()
    {
        StartCoroutine(SceneLoadTransition_End_Scene_4());
    }

    public void StartSceneLoadTransition_Scene_5()
    {
        StartCoroutine(SceneLoadTransition_End_Scene_5());
    }

    public void StartSceneLoadTransition_Scene_6()
    {
        StartCoroutine(SceneLoadTransition_End_Scene_6());
    }

    private IEnumerator SceneLoadTransition_End_Scene_1()
    {
        endingTransition.SetActive(true);
        Invoke("disableStartingSceneTransition", 5f);

        yield return new WaitForSeconds(1.5f);

        sceneLoad.LoadScene1();
    }
    

    private IEnumerator SceneLoadTransition_End_Scene_2()
    {
        endingTransition.SetActive(true);
        Invoke("disableStartingSceneTransition", 5f);

        yield return new WaitForSeconds(1.5f);

        sceneLoad.LoadScene2();
    }

    private IEnumerator SceneLoadTransition_End_Scene_3()
    {
        endingTransition.SetActive(true);
        Invoke("disableStartingSceneTransition", 5f);

        yield return new WaitForSeconds(1.5f);

        sceneLoad.LoadScene3();
    }
    private IEnumerator SceneLoadTransition_End_Scene_4()
    {
        endingTransition.SetActive(true);
        Invoke("disableStartingSceneTransition", 5f);

        yield return new WaitForSeconds(1.5f);

        sceneLoad.LoadScene4();
    }
    private IEnumerator SceneLoadTransition_End_Scene_5()
    {
        endingTransition.SetActive(true);
        Invoke("disableStartingSceneTransition", 5f);

        yield return new WaitForSeconds(1.5f);

        sceneLoad.LoadScene5();
    }
    private IEnumerator SceneLoadTransition_End_Scene_6()
    {
        endingTransition.SetActive(true);
        Invoke("disableStartingSceneTransition", 5f);

        yield return new WaitForSeconds(1.5f);

        sceneLoad.LoadScene6();
    }

    private void disableStartingSceneTransition()
    {
        startingTransition.SetActive(false);
    }

    public void SceneLoadTransition_Start()
    {
        startingTransition.SetActive(true);
        Invoke("disableEndingSceneTransition", 1.5f);
    }

    private void disableEndingSceneTransition()
    {
        startingTransition.SetActive(false);
    }
}
