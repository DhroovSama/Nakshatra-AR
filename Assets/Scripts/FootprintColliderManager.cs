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
