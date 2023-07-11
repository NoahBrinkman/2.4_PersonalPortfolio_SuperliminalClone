using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A relatively simplel class tahat allows me to call events upon entering a collider. 
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class EventCollider : MonoBehaviour
{
    public BoxCollider Collider { get; private set; }
    public UnityEvent<Collider> OnCollisionEnterEvent;
    public UnityEvent<Collider> OnCollisionExitEvent;
    public UnityEvent<Collider> OnCollisionStayEvent;


    private void Awake()
    {
        Collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
       OnCollisionEnterEvent?.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
       OnCollisionExitEvent?.Invoke(other);
    }

    private void OnTriggerStay(Collider collisionInfo)
    {
        OnCollisionStayEvent?.Invoke(collisionInfo);
    }
}
