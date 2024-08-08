using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalDoor : MonoBehaviour
{
    private bool isCameraInside = false;
    private bool isCollisionProcessing = false;

    [SerializeField]
    private MaterialStencilToggleDataSO materialStencilData;

    [SerializeField]
    private bool isPortalPassed = false;
    public bool IsPortalPassed {  get { return isPortalPassed; } }

    //private bool isPortalPassedOnce = false;

    //[SerializeField]
    //private VoiceOverData welcomeVO;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            isPortalPassed = true;

        }

        if (other.gameObject.CompareTag("MainCamera") && !isCollisionProcessing)
        {
            //Debug.Log("hot");

            //if(!isPortalPassedOnce)
            //{
            //    isPortalPassedOnce = true;

            //    VoiceOverManager.Instance.TriggerVoiceOver(welcomeVO);
            //}
            
            isCollisionProcessing = true;
            StartCoroutine(ProcessCollision(other)); 
        }
    }

    private IEnumerator ProcessCollision(Collider other) 
    {
        isCameraInside = !isCameraInside;

        Material[] allMaterials = materialStencilData.GetAllMaterials();

        // Iterate through all materials and apply the changes
        foreach (Material material in allMaterials)
        {
            // If the tag is "MainCamera", set "stest" to NotEqual, otherwise Equal
            material.SetInt("stest", isCameraInside && other.gameObject.CompareTag("MainCamera") ? (int)CompareFunction.NotEqual : (int)CompareFunction.Equal);
        }

        yield return new WaitForSeconds(3f);

        isCollisionProcessing = false;
    }


}



