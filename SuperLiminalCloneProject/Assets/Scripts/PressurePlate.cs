using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// The pressure plate for puzzle 2.
/// With a minimum and maximum mass the 
/// </summary>
public class PressurePlate : MonoBehaviour
{
    [FormerlySerializedAs("minimumMass")] [SerializeField] private float _minimumMass;
    [FormerlySerializedAs("maximumMass")] [SerializeField] private float _maximumMass;
    private float _value;
    private float _endValue;
    [FormerlySerializedAs("RegistrationTime")] [SerializeField] private float _registrationTime;
    private float _timer;

    [FormerlySerializedAs("OnWeightRegistrationComplete")] [SerializeField] private UnityEvent _onWeightRegistrationComplete;
    [FormerlySerializedAs("fillImage")] [SerializeField] private Image _fillImage;


    private void Start()
    {
        _timer = 0;
    }
    /// <summary>
    /// If a rigidbody enters the trigger, set its mass to the end value.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() is Rigidbody rb && rb != null)
        {
            _endValue = rb.mass;
                
        }
    }
    /// <summary>
    /// If something exits the trigger, reset the values of the pressure plate.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        _endValue = 0;
        _value = 0;
        _fillImage.fillAmount = 0;
    }
    
    /// <summary>
    /// Lerp a value between 0 and the end value by time and then fill an image for visual clarity.
    /// </summary>
    private void Update()
    {
        _timer += Time.deltaTime;
        _value = Mathf.Lerp(0, _endValue / _maximumMass, _timer / _registrationTime);
        _fillImage.fillAmount = _value;
        if (_timer >= _registrationTime && _endValue >= _minimumMass && _endValue <= _maximumMass)
        {
            _onWeightRegistrationComplete?.Invoke();
        }
    }
}
