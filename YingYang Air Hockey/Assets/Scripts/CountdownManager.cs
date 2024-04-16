using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    public TMP_Text countdownText; // Reference to the TMP_Text component for displaying countdown
    public float countdownDuration = 5f; // Duration of the countdown in seconds

    private void Start()
    {
        StartCoroutine(StartGameCountdown()); // Start the countdown coroutine
    }

    private IEnumerator StartGameCountdown()
    {
        float timer = countdownDuration; // Set the initial countdown timer

        while (timer > 0f) // Loop until the timer reaches zero
        {
            countdownText.text = Mathf.Ceil(timer).ToString(); // Display the current countdown value (rounded up)
            timer -= Time.deltaTime; // Decrease the timer based on the time passed in each frame
            yield return null; // Wait for the next frame
        }

        // Start the game (load the Main scene) after the countdown finishes
        SceneManager.LoadScene("Main");
    }
}
