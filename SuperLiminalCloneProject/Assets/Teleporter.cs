using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Vector3 position;
    
    public void Teleport(Transform target)
    {
        Debug.Log("Hi");
        if (target.GetComponent<CharacterController>() is CharacterController c)
        {
            c.enabled = false; 
        }
            target.position = position;
            if (target.GetComponent<CharacterController>() is CharacterController controller)
            {
                controller.enabled = true; 
            }
    }
}
