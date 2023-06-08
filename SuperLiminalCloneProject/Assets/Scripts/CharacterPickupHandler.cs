using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is meant to hndle the picking up of objects in the scene.
/// </summary>
public class CharacterPickupHandler : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private Camera _cam;
    [SerializeField] private Image _image;
    [SerializeField] private float _pickupDistance;

    [SerializeField] private GameObject _currentPickedUpObject;
    /// <summary>
    /// Cast a ray from the center of the creen, look if it hit an object the player can pick up
    /// and pick up the object.
    /// </summary>
    private void PickUp()
    {
         RaycastHit ray;
         Debug.DrawRay(_cam.transform.position, _cam.transform.forward*_range);
         if(Physics.Raycast(_cam.transform.position,_cam.transform.forward, out ray, _range, _layers))
         {
             Debug.Log("Hit");
             _image.color = new Color(1, 1, 1, 0);
             if (Input.GetKeyDown(KeyCode.E) && _currentPickedUpObject == null)
             {
                 _currentPickedUpObject = ray.collider.gameObject;
                 _currentPickedUpObject.GetComponent<Rigidbody>().isKinematic = true;
             }
         }  
         else
         {
             Debug.Log("!Hit");
             _image.color = new Color(1, 1, 1, 1);
         }
    }

 

    private void Update()
    {
        PickUp();
        if (_currentPickedUpObject != null)
        {
            _currentPickedUpObject.transform.position  = _cam.transform.position + _cam.transform.forward * _pickupDistance;
            if(Input.GetKeyUp(KeyCode.E))
            {
                _currentPickedUpObject.GetComponent<Rigidbody>().isKinematic = false;
                _currentPickedUpObject = null;
            }
        }
    }
}
