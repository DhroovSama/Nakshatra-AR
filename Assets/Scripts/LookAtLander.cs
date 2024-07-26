using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtLander : MonoBehaviour
{
    [SerializeField]
    private GameObject Lander;

    private void Update()
    {
        if(Lander == null)
        {
            Lander = FindObjectOfType<LanderCollisionHandler>().gameObject;
        }

        transform.LookAt(Lander.transform);
    }
}
