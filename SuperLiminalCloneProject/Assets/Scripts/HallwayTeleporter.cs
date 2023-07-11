using UnityEngine;
using UnityEngine.Serialization;

public class HallwayTeleporter : MonoBehaviour
{
    [SerializeField,FormerlySerializedAs("otherPortal")] private HallwayTeleporter _otherPortal;
    [FormerlySerializedAs("offSet")] public Vector3 OffSet;
    [FormerlySerializedAs("rotateY")] [SerializeField] private float _rotateY;

    /// <summary>
    /// Upon entering the trigger teleport the player if the player is facing the same direction as the teleporter.
    /// Keeping in mind the relative position and applying an offset for a seamless teleport
    /// <remarks>Make sure that the player is on the player layer and that your trigger excludes all layers bar the player</remarks>
    /// </summary>
    /// <param name="other"> Other should always be a player due to include and exclude layers of the collider</param>
    private void OnTriggerEnter(Collider other)
    {
        if (Vector3.Dot(other.transform.forward, transform.forward) >= 0)
        {
            Debug.Log("Hi");
            //TP player to the other side
            Vector3 diff = transform.position - other.transform.position ;
            diff = Quaternion.AngleAxis(_rotateY, new Vector3(0, 1, 0)) * diff;
            other.GetComponent<CharacterController>().enabled = false;
            other.GetComponent<FPSController>().AddToYaw(_rotateY);
            diff.y = 0;
            other.transform.position =
                new Vector3(_otherPortal.transform.position.x, other.transform.position.y, _otherPortal.transform.position.z) - diff + (_otherPortal.OffSet - _otherPortal.transform.forward * .5f);

            other.GetComponent<CharacterController>().enabled = true;
            other.GetComponent<FPSController>().enabled = true;
            //otherPortal.GetComponent<HallwayTeleporter>().enableTeleport = false;
        }
    }

    
    /// <summary>
    /// <para>Some gizmos as a visual aid.</para>
    /// <para><b>Blue -</b> Where the player will end up. </para>
    /// <para><b>Red -</b> Where the player will be teleported from. </para> 
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(_otherPortal.transform.position + _otherPortal.OffSet, (Vector3.one/4) );
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_otherPortal.transform.position , (Vector3.one/4) );
    }
}
