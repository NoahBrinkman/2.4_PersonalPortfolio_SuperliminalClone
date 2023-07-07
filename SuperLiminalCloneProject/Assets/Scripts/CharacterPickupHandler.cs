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
    [SerializeField] private LayerMask _pickupLayer;
    [SerializeField] private LayerMask _placeLayers;
    [SerializeField] private Camera _cam;
    [SerializeField] private Image _image;
    [SerializeField] private float offsetMultiplier = 1.1f;
    private GameObject _highlitObject;
    private GameObject _currentPickedUpObject;
    private Vector3 targetScale = new Vector3();
    private Vector3 _savedScale = new Vector3();
    private float _savedDistance = 0.0f;
    private float savedMass;
    private Vector3 hitOffset;
    
    /// <summary>
    /// Cast a ray from the center of the creen, look if it hit an object the player can pick up
    /// and pick up the object.
    /// </summary>
    private void PickUp()
    {
         RaycastHit ray;
         Debug.DrawRay(_cam.transform.position, _cam.transform.forward*_range);
         if(Physics.Raycast(_cam.transform.position,_cam.transform.forward, out ray, _range, _pickupLayer))
         {
             Debug.Log("Hit");
             _image.color = new Color(1, 1, 1, 0);
             if (_highlitObject == null)
             {
                 _highlitObject = ray.collider.gameObject;
                     _highlitObject.layer = 7;
                                     
                     foreach (Transform o in _highlitObject.transform)
                     {
                         o.gameObject.layer = 7;
                     }
             }
             if (_highlitObject != ray.collider.gameObject)
             {
                 _highlitObject.layer = 6;
                 foreach (Transform o in _highlitObject.transform)
                 {
                     o.gameObject.layer = 6;
                 }
                 _highlitObject = ray.collider.gameObject;
                 _highlitObject.layer = 7;
                 foreach (Transform o in _highlitObject.transform)
                 {
                     o.gameObject.layer = 7;
                 }
             }
         
             if (Input.GetKeyDown(KeyCode.E) && _currentPickedUpObject == null)
             {
                 _currentPickedUpObject = ray.collider.gameObject;
                 ray.collider.enabled = false;
                 _currentPickedUpObject.GetComponent<Rigidbody>().isKinematic = true;
                 savedMass = _currentPickedUpObject.GetComponent<Rigidbody>().mass;
                 _savedScale = _currentPickedUpObject.transform.localScale;
                 targetScale = _currentPickedUpObject.transform.localScale;
                 _savedDistance = Vector3.Distance(transform.position, _currentPickedUpObject.transform.position);
                 hitOffset = ray.collider.transform.InverseTransformPoint(ray.point); 
                 
             }
         }  
         else
         {
             if (_highlitObject != null)
             {
                 _highlitObject.layer = 6;
                 foreach (Transform o in _highlitObject.transform)
                 {
                     o.gameObject.layer = 6;
                 }
                 _highlitObject = null;
             }
             Debug.Log("!Hit");
             _image.color = new Color(1, 1, 1, 1);
         }
    }

    private void SetPickUpPosition()
    {
        RaycastHit ray;
        
        if(Physics.Raycast(_cam.transform.position,_cam.transform.forward, out ray, Mathf.Infinity,_placeLayers))
        {
            Debug.Log(ray.transform.name);
            Vector3 pos;
            pos = ray.point- _cam.transform.forward * _currentPickedUpObject.transform.localScale.x/2;
     
            //float desiredScale = 1 / ray.distance;
            float scale =   Vector3.Distance(transform.position, pos)/_savedDistance ;
            targetScale = new Vector3(scale, scale, scale);
            Vector3 newScale = _savedScale;
            newScale.Scale(targetScale);
            int index = 0;
            while (Physics.OverlapBox(pos, newScale * offsetMultiplier, _currentPickedUpObject.transform.rotation).Length > 0 && index <= 50)
            {
                index++;
                    pos -= _cam.transform.forward * .1f;
                    scale =   Vector3.Distance(transform.position, pos)/_savedDistance ;
                    targetScale = new Vector3(scale, scale, scale);
                    newScale = _savedScale;
                    newScale.Scale(targetScale);
            }
          //while(collisionDetector.)

          
            _currentPickedUpObject.transform.localScale = newScale;/*_savedScale * targetScale*/;
            _currentPickedUpObject.transform.position = pos ;

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
                _currentPickedUpObject.GetComponent<Rigidbody>().mass =
                    savedMass * _currentPickedUpObject.transform.localScale.x / _savedScale.x;
                _currentPickedUpObject.GetComponent<Collider>().enabled = true;
                _currentPickedUpObject =null;
                _savedScale = new Vector3();
                targetScale = new Vector3();
                _savedDistance = 0;
                
            }
            
        }
        else
        {
            PickUp();
        }
    }

    private void OnDrawGizmos()
    {
        if (_currentPickedUpObject != null)
        {
            Vector3 newScale = _savedScale;
            newScale.Scale(targetScale*2);
            Gizmos.color = Color.green;
            
            Gizmos.DrawCube(_currentPickedUpObject.transform.position, newScale);
        }
    }
}
