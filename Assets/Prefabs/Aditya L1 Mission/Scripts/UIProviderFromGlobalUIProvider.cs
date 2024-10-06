using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProviderFromGlobalUIProvider : MonoBehaviour
{
    private void EnablePSLVSeperationTutorial()
    {
        GlobalUIProvider_AdityaL1.getSeperationPhaseTutorial().SetActive(true);
    }
}
