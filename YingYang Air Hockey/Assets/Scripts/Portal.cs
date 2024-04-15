using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject destinationPortal;
    public float teleportDistanceThreshold = 0.2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Puck"))
        {
            // Check distance between portal and puck
            if (Vector2.Distance(transform.position, other.transform.position) > teleportDistanceThreshold)
            {
                // Teleport the puck to the destination portal
                other.transform.position = destinationPortal.transform.position;
            }
        }
    }
}
