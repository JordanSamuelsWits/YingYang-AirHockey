using System.Collections;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    public ScoreScript ScoreScriptInstance;
    public static bool WasGoal { get; private set; }
    public float MaxSpeed;

    public AudioManager audioManager;

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
        if (!WasGoal)
        {
            if (other.CompareTag("PlayerRed") || other.CompareTag("PlayerBlue")) // Check player tags
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
                ScoreScriptInstance.IncrementScore("PlayerGoal"); // Player scores
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(false));
            }
            else if (other.CompareTag("PlayerGoal"))
            {
                ScoreScriptInstance.IncrementScore("AiGoal"); // AI scores
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(true));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlayPuckCollision();
    }

    private IEnumerator ResetPuck(bool didAiScore)
    {
        yield return new WaitForSecondsRealtime(1f); // Wait for a moment before resetting
        WasGoal = false;
        rb.velocity = Vector2.zero;
        CenterPuck(); // Reset puck position

        if (didAiScore)
            rb.position = new Vector2(0f, -1.6f); // AI scored, reset to player side
        else
            rb.position = new Vector2(0f, 1.6f); // Player scored, reset to AI side
    }

    private void DeductPoint(GameObject player)
    {
        // Determine which player to deduct a point from based on tag
        if (player.CompareTag("PlayerRed"))
        {
            ScoreScriptInstance.DecrementScore("PlayerGoal"); // Deduct point from player (Blue)
        }
        else if (player.CompareTag("PlayerBlue"))
        {
            ScoreScriptInstance.DecrementScore("AiGoal"); // Deduct point from AI (Red)
        }
    }

    public void CenterPuck()
    {
        rb.position = Vector2.zero; // Reset puck to the center
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
    }
}
