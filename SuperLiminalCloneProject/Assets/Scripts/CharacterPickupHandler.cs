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
    [SerializeField] private float _offset;
    [SerializeField] private GameObject _currentPickedUpObject;
    private Vector3 targetScale = new Vector3();
    private  float _savedScale =0f;
    private float _savedDistance = 0.0f;
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
                 _savedScale = _currentPickedUpObject.transform.localScale.x;
                 targetScale = _currentPickedUpObject.transform.localScale;
                 _savedDistance = Vector3.Distance(transform.position, _currentPickedUpObject.transform.position);
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
            Vector3 pos;
            int i = 0;
            do
            {
               pos = ray.point - _cam.transform.forward * i;
               i++;
            } while (Physics.OverlapBox(pos, _savedScale * targetScale, _currentPickedUpObject.transform.rotation)
                         .Length > 0 && Vector3.Dot(pos, _cam.transform.position + _cam.transform.forward) > 0 && i <= 50);
            _currentPickedUpObject.transform.position = pos;
            //float desiredScale = 1 / ray.distance;
            float scale =   Vector3.Distance(transform.position, pos)/_savedDistance ;
            targetScale = new Vector3(scale, scale, scale);

            _currentPickedUpObject.transform.localScale = _savedScale * targetScale;

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
                _savedScale =0;
                targetScale = new Vector3();
                _savedDistance =0;
            }
        }
        else
        {
            PickUp();
        }
    }
}
