using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Teleporter : MonoBehaviour
{
    [FormerlySerializedAs("position")] [SerializeField] private Vector3 _position;
    
    /// <summary>
    /// A simple teleport method.
    /// It temporarily disables character controllers as well to make sure that you can also teleport players
    /// as character controllers override transform changes.
    /// </summary>
    /// <param name="target"> Object you want to teleport</param>
    public void Teleport(Transform target)
    {
        Debug.Log("Hi");
        if (target.GetComponent<CharacterController>() is CharacterController c)
        {
            c.enabled = false; 
        }
            target.position = _position;
            if (target.GetComponent<CharacterController>() is CharacterController controller)
            {
                controller.enabled = true; 
            }
    }
}
