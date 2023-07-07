using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPhysics : MonoBehaviour
{
    [SerializeField] private float _force;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.rigidbody;
        if (rb != null)
        {
            Vector3 forceDir = hit.transform.position - transform.position;
            //forceDir.y = 0;
            forceDir.Normalize();
            forceDir *= _force;
            rb.AddForce(forceDir, ForceMode.Impulse);
        }
    }
}
