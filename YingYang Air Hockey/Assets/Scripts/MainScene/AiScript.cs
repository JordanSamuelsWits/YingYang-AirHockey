using UnityEngine;

public class AiScript : MonoBehaviour
{
    public float MaxMovementSpeed;
    public Rigidbody2D Puck;
    public Transform PlayerBoundaryHolder;
    public Transform PuckBoundaryHolder;

    private Rigidbody2D rb;
    private Vector2 startingPosition;
    private Boundary playerBoundary;
    private Boundary puckBoundary;
    private Vector2 targetPosition;
    private bool isFirstTimeInOpponentsHalf = true;
    private float offsetXFromTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;

        // Define player and puck boundaries
        playerBoundary = GetBoundary(PlayerBoundaryHolder);
        puckBoundary = GetBoundary(PuckBoundaryHolder);

        // Set max movement speed based on difficulty
        switch (GameValues.Difficulty)
        {
            case GameValues.Difficulties.EASY:
                MaxMovementSpeed = 8;
                break;
            case GameValues.Difficulties.MEDIUM:
                MaxMovementSpeed = 15;
                break;
            case GameValues.Difficulties.HARD:
                MaxMovementSpeed = 25;
                break;
        }
    }

    private Boundary GetBoundary(Transform boundaryHolder)
    {
        Vector3 bottomLeft = boundaryHolder.GetChild(0).position;
        Vector3 topLeft = boundaryHolder.GetChild(1).position;
        Vector3 bottomRight = boundaryHolder.GetChild(2).position;
        Vector3 topRight = boundaryHolder.GetChild(3).position;

        return new Boundary(bottomLeft.y, topLeft.y, bottomRight.x, topRight.x);
    }

    private void FixedUpdate()
    {
        if (!PuckScript.WasGoal)
        {
            // Calculate movement speed based on difficulty
            float movementSpeed = isFirstTimeInOpponentsHalf ?
                Random.Range(MaxMovementSpeed * 0.4f, MaxMovementSpeed) :
                MaxMovementSpeed * Random.Range(0.1f, 0.3f);

            if (Puck.position.y < puckBoundary.Down)
            {
                // Puck is in AI's half, adjust target towards puck horizontally
                if (isFirstTimeInOpponentsHalf)
                {
                    isFirstTimeInOpponentsHalf = false;
                    offsetXFromTarget = Random.Range(-1f, 1f);
                }

                targetPosition = new Vector2(Mathf.Clamp(Puck.position.x + offsetXFromTarget,
                                                          playerBoundary.Left, playerBoundary.Right),
                                             startingPosition.y);
            }
            else
            {
                // Puck is in player's half, move towards puck
                isFirstTimeInOpponentsHalf = true;
                targetPosition = new Vector2(Mathf.Clamp(Puck.position.x, playerBoundary.Left, playerBoundary.Right),
                                             Mathf.Clamp(Puck.position.y, playerBoundary.Down, playerBoundary.Up));
            }

            // Move towards the calculated target position
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, movementSpeed * Time.fixedDeltaTime));
        }
    }

    public void ResetPosition()
    {
        rb.position = startingPosition;
    }
}