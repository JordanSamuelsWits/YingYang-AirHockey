using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject destinationPortal; // Reference to the destination portal
    public float teleportDistanceThreshold = 0.2f; // Minimum distance for teleportation to occur

    // Triggered when another collider enters this portal's trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Puck")) // Check if the entering collider is the puck
        {
            // Check the distance between the portal and the puck
            if (Vector2.Distance(transform.position, other.transform.position) > teleportDistanceThreshold)
            {
                // Teleport the puck to the destination portal's position
                other.transform.position = destinationPortal.transform.position;
            }
        }
    }
}
