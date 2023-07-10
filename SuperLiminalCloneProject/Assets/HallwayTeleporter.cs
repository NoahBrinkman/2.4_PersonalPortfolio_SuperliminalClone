using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayTeleporter : MonoBehaviour
{
    [SerializeField] private HallwayTeleporter otherPortal;
    public Vector3 offSet;
    [SerializeField] private float rotateY;
    public bool enableTeleport = true;
    private void OnTriggerEnter(Collider other)
    {
        if (!enableTeleport) return;
        if (Vector3.Dot(other.transform.forward, transform.forward) >= 0)
        {
            Debug.Log("Hi");
            //TP player to the other side
            Vector3 diff = transform.position - other.transform.position ;
            diff = Quaternion.AngleAxis(rotateY, new Vector3(0, 1, 0)) * diff;
            other.GetComponent<CharacterController>().enabled = false;
            other.GetComponent<FPSController>().AddToYaw(rotateY);
            diff.y = 0;
            other.transform.position =
                new Vector3(otherPortal.transform.position.x, other.transform.position.y, otherPortal.transform.position.z) - diff + (otherPortal.offSet - otherPortal.transform.forward * .5f);

            other.GetComponent<CharacterController>().enabled = true;
            other.GetComponent<FPSController>().enabled = true;
            //otherPortal.GetComponent<HallwayTeleporter>().enableTeleport = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enableTeleport = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(otherPortal.transform.position + otherPortal.offSet, (Vector3.one/4) );
        Gizmos.color = Color.red;
        Gizmos.DrawCube(otherPortal.transform.position , (Vector3.one/4) );
    }
}
