using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject CanvasGame;    // Reference to the main game canvas
    public GameObject CanvasRestart; // Reference to the restart canvas
    public GameObject Portals;       // Reference to the GameObject containing portals

    [Header("CanvasRestart Children")]
    public GameObject WinTxt;    // Text displaying win message
    public GameObject LoseTxt;   // Text displaying lose message
    public GameObject RestartBtn; // Button to restart the game

    [Header("Other")]
    public AudioManager audioManager; // Reference to the audio manager

    public ScoreScript scoreScript;    // Reference to the score script
    public PuckScript puckScript;      // Reference to the puck script
    public PlayerMovement playerMovement; // Reference to the player movement script
    public AiScript aiScript;          // Reference to the AI script
    public Portal portalScript;        // Reference to the portal script

    private void Start()
    {
        // Ensure CanvasGame is active and CanvasRestart is inactive at the beginning
        if (CanvasGame != null)
            CanvasGame.SetActive(true);

        if (CanvasRestart != null)
            CanvasRestart.SetActive(false);  // Deactivate CanvasRestart initially

        // Deactivate portals at the start of the game
        DeactivatePortals();

        // Start the coroutine for portal management
        StartCoroutine(ManagePortals());
    }

    // Coroutine to manage the activation and deactivation of portals
    private IEnumerator ManagePortals()
    {
        yield return new WaitForSeconds(5f); // Initial delay before portals activate

        while (true)
        {
            ActivatePortals();                  // Activate portals
            audioManager.PlayPortalActivation(); // Play portal activation audio

            yield return new WaitForSeconds(15f); // Portals remain active for 15 seconds

            DeactivatePortals(); // Deactivate portals

            yield return new WaitForSeconds(10f); // Portals remain inactive for 10 seconds
        }
    }

    // Activate the portals GameObject
    private void ActivatePortals()
    {
        if (Portals != null)
            Portals.SetActive(true); // Activate the Portals GameObject
    }

    // Deactivate the portals GameObject
    private void DeactivatePortals()
    {
        if (Portals != null)
            Portals.SetActive(false); // Deactivate the Portals GameObject
    }

    // Show the restart canvas based on whether AI won or not
    public void ShowRestartCanvas(bool didAiWin)
    {
        Time.timeScale = 0; // Pause the game

        CanvasGame.SetActive(false);  // Deactivate CanvasGame
        CanvasRestart.SetActive(true);  // Activate CanvasRestart

        if (didAiWin)
        {
            audioManager.PlayLostGame();

            // Show "YOU LOSE!" text and restart button
            LoseTxt.SetActive(true);
            RestartBtn.SetActive(true);

            // Hide "YOU WIN!" text
            WinTxt.SetActive(false);
        }
        else
        {
            audioManager.PlayWonGame();

            // Show "YOU WIN!" text and restart button
            WinTxt.SetActive(true);
            RestartBtn.SetActive(true);

            // Hide "YOU LOSE!" text
            LoseTxt.SetActive(false);
        }
    }

    // Restart the game
    public void RestartGame()
    {
        Time.timeScale = 1; // Resume time scale (unpause game)

        CanvasGame.SetActive(true);  // Activate CanvasGame
        CanvasRestart.SetActive(false);  // Deactivate CanvasRestart

        // Reset game components
        scoreScript.ResetScores();    // Reset scores
        puckScript.CenterPuck();      // Center the puck
        playerMovement.ResetPosition(); // Reset player's position
        aiScript.ResetPosition();     // Reset AI's position

        // Restart the coroutine for portal management
        StartCoroutine(ManagePortals());
    }

    // Load the main menu scene
    public void ShowMenu()
    {
        Time.timeScale = 1; // Resume time scale (unpause game)
        SceneManager.LoadScene("Menu"); // Load the menu scene
    }
}
