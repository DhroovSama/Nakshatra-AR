using UnityEngine;

public class FootprintColliderManager : MonoBehaviour
{
    //[SerializeField]
    //private FootPrintPath footPrintPath;

    [SerializeField] private GameObject nextFootPrint;

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

            this.gameObject.SetActive(false);

            nextFootPrint.SetActive(true);
        }
    }
}
