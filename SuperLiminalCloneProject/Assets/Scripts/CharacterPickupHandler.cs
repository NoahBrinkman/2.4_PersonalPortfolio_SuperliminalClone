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
    private Vector3 _savedScale = new Vector3();
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
                 _savedScale = _currentPickedUpObject.transform.localScale;
             }
         }  
         else
         {
             Debug.Log("!Hit");
             _image.color = new Color(1, 1, 1, 1);
         }
    }

    private void SetPickUpPosition()
    {
        RaycastHit ray;
        
        if(Physics.Raycast(_cam.transform.position,_cam.transform.forward, out ray, Mathf.Infinity,~_layers))
        {
            Debug.Log(ray.transform.name);
            _currentPickedUpObject.transform.position = ray.point - _cam.transform.forward * (_currentPickedUpObject.transform.localScale.x/4);
            //float desiredScale = 1 / ray.distance;
            _currentPickedUpObject.transform.localScale = _savedScale * (ray.distance/4);
        }
    }

    private void Update()
    {
     
        if (_currentPickedUpObject != null)
        {
            SetPickUpPosition();
            if(Input.GetKeyUp(KeyCode.E))
            {
                _currentPickedUpObject.GetComponent<Rigidbody>().isKinematic = false;
                _currentPickedUpObject.GetComponent<Rigidbody>().mass *= _currentPickedUpObject.transform.localScale.x;
                _currentPickedUpObject = null;
            }
        }
        else
        {
            PickUp();
        }
    }
}
