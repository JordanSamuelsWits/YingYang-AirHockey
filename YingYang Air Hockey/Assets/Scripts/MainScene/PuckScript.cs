using System.Collections;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    public ScoreScript ScoreScriptInstance; // Reference to the ScoreScript instance
    public static bool WasGoal { get; private set; } // Flag to track if a goal was scored
    public float MaxSpeed; // Maximum speed limit for the puck

    public AudioManager audioManager; // Reference to the AudioManager for playing audio

    private Rigidbody2D rb;
    private GameObject lastPlayer; // Track the last player who hit the puck

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WasGoal = false;
        CenterPuck(); // Ensure puck starts at the center
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal) // Check if a goal was not scored yet
        {
            if (other.CompareTag("PlayerRed") || other.CompareTag("PlayerBlue")) // Check for player collisions
            {
                if (lastPlayer == null || lastPlayer != other.gameObject)
                {
                    lastPlayer = other.gameObject; // Update last player who hit the puck
                }
                else
                {
                    // Deduct a point from the player who hit the puck more than once
                    ScoreScriptInstance.DecrementScore(other.CompareTag("PlayerRed") ? "PlayerGoal" : "AiGoal");
                }
            }
            else if (other.CompareTag("AiGoal"))
            {
                // Player scores a goal
                ScoreScriptInstance.IncrementScore("PlayerGoal");
                WasGoal = true;
                audioManager.PlayGoal(); // Play goal audio
                StartCoroutine(ResetPuck(false)); // Reset puck after scoring
            }
            else if (other.CompareTag("PlayerGoal"))
            {
                // AI scores a goal
                ScoreScriptInstance.IncrementScore("AiGoal");
                WasGoal = true;
                audioManager.PlayGoal(); // Play goal audio
                StartCoroutine(ResetPuck(true)); // Reset puck after scoring
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlayPuckCollision(); // Play collision audio when puck hits something
    }

    private IEnumerator ResetPuck(bool didAiScore)
    {
        yield return new WaitForSecondsRealtime(1f); // Wait for a moment before resetting
        WasGoal = false;
        rb.velocity = Vector2.zero;
        CenterPuck(); // Reset puck position to the center

        // Move puck to the appropriate side based on which team scored
        if (didAiScore)
            rb.position = new Vector2(0f, -1.6f); // AI scored, reset to player side
        else
            rb.position = new Vector2(0f, 1.6f); // Player scored, reset to AI side
    }

    public void CenterPuck()
    {
        rb.position = Vector2.zero; // Reset puck to the center of the playing area
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed); // Limit the puck's velocity
    }
}
