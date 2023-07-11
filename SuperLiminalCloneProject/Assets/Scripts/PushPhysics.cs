using UnityEngine;

/// <summary>
/// A character controller does not apply physics naturally to objects, meaning this needs to be done manually.
/// This class applies force to objects the player hits.
/// </summary>
public class PushPhysics : MonoBehaviour
{
    [SerializeField] private float _force;
    /// <summary>
    /// Upon hitting an object apply appropriate force to it
    /// </summary>
    /// <param name="hit"> The object the player hit</param>
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.rigidbody;
        if (rb != null)
        {
            Vector3 forceDir = hit.transform.position - transform.position;
            //forceDir.y = 0;
            forceDir.Normalize();
            forceDir *= _force;
            rb.AddForce(forceDir, ForceMode.Impulse);
        }
    }
}
