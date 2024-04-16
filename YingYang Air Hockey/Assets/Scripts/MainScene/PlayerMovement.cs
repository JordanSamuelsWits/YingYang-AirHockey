using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool wasJustClicked = true; // Flag to track if the mouse button was just clicked
    bool canMove; // Flag to determine if the player can move

    Rigidbody2D rb; // Reference to the Rigidbody2D component
    Vector2 startingPosition; // Starting position of the player

    public Transform BoundaryHolder; // Reference to the GameObject holding boundary positions

    Boundary playerBoundary; // Boundary that restricts player movement

    Collider2D playerCollider; // Collider of the player

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player
        startingPosition = rb.position; // Store the starting position of the player
        playerCollider = GetComponent<Collider2D>(); // Get the Collider2D component of the player

        // Define the player boundary using positions of child GameObjects of BoundaryHolder
        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Check if the left mouse button is pressed
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get mouse position in world coordinates

            if (wasJustClicked) // Check if the mouse button was just clicked
            {
                wasJustClicked = false;

                // Check if the mouse click is within the player's collider
                if (playerCollider.OverlapPoint(mousePos))
                {
                    canMove = true; // Enable player movement
                }
                else
                {
                    canMove = false; // Disable player movement
                }
            }

            if (canMove) // Check if player can move
            {
                // Clamp mouse position within the player boundary
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left, playerBoundary.Right),
                                                      Mathf.Clamp(mousePos.y, playerBoundary.Down, playerBoundary.Up));

                // Move the player to the clamped mouse position
                rb.MovePosition(clampedMousePos);
            }
        }
        else
        {
            wasJustClicked = true; // Reset the flag when mouse button is released
        }
    }

    // Reset the player's position to the starting position
    public void ResetPosition()
    {
        rb.position = startingPosition;
    }
}
