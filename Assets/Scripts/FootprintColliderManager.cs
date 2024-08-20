using Unity.VisualScripting;
using UnityEngine;

public class FootprintColliderManager : MonoBehaviour
{
    //[SerializeField]
    //private FootPrintPath footPrintPath;

    [SerializeField] private GameObject nextFootPrint;

    [SerializeField]
    private GameObject PlayerCheckerCircle;

    [SerializeField]
    private bool isLastFoot;

    [SerializeField]
    private VibrationController vibrationController;

    [SerializeField]
    private AudioClip footStepSFX;

    private void Start()
    {
        //// Assuming FootPrintPath is attached to the same GameObject
        //footPrintPath = FindObjectOfType<FootPrintPath>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            //footPrintPath.SpawnFootprints();

            vibrationController.VibratePhone_Light();

            GlobalAudioPlayer.GetPlaySound(footStepSFX);

            if(isLastFoot)
            {
                PlayerCheckerCircle.SetActive(true);

                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(false);

                nextFootPrint.SetActive(true);
            }   
        }
    }
}
