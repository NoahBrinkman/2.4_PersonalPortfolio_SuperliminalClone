using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private float minimumMass;
    [SerializeField] private float maximumMass;
    private float value;
    private float endValue;
    [SerializeField] private float RegistrationTime;
    private float timer;

    [SerializeField] private UnityEvent OnWeightRegistrationComplete;
    [SerializeField] private Image fillImage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() is Rigidbody rb && rb != null)
        {
            endValue = rb.mass;
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        endValue = 0;
        value = 0;
        fillImage.fillAmount = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        value = Mathf.Lerp(0, endValue / maximumMass, timer / RegistrationTime);
        fillImage.fillAmount = value;
        if (timer >= RegistrationTime && endValue >= minimumMass && endValue <= maximumMass)
        {
            OnWeightRegistrationComplete?.Invoke();
        }
    }
}
