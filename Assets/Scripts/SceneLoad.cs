using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    private Slider startChandrayaanSlider;

    private void Start()
    {
        Application.targetFrameRate = 70;
        // Subscribe to the slider's onValueChanged event
        startChandrayaanSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        // Check if the slider's value is greater than or equal to 0.9
        if (value >= 0.95)
        {
            LoadScene2();
        }
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
}
