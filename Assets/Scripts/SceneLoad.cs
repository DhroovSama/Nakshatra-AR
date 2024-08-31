using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{

    private void Start()
    {
        Application.targetFrameRate = 70;
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadScene3()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadScene4()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadScene5()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadScene6()
    {
        SceneManager.LoadScene(6);
    }
}
