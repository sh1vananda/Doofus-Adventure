using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to Doofus's Transform
    public Vector3 offset;   // Offset from the target position

    void Start()
    {
        // If no offset is set in the Inspector, calculate it based on initial positions
        if (target != null && offset == Vector3.zero)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Update the camera's position to follow the target with the offset
            transform.position = target.position + offset;
        }
    }
}