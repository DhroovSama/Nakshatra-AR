using UnityEngine;

public class SceneOrientationController : MonoBehaviour
{
    [SerializeField]
    private GameObject Canvas, potraitCanvas, XRController, QuizCanvas;

    [SerializeField] PlaceOnPlane placeOnPlane;

    private void Awake()
    {
        placeOnPlane = XRController.GetComponent<PlaceOnPlane>();
    }

    public void CHangeOrientation()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        potraitCanvas.SetActive(false);

        placeOnPlane.enabled = true;

        Canvas.SetActive(true);
    }

    public void NormalChangeOrientation_Potrait()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        Canvas.SetActive(false);

        QuizCanvas.SetActive(true);
    }
}
