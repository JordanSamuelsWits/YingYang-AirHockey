using System.Collections;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;
    public GameObject Portals; // Reference to the GameObject containing portals

    [Header("CanvasRestart Children")]
    public GameObject WinTxt;
    public GameObject LoseTxt;
    public GameObject RestartBtn;

    [Header("Other")]
    public AudioManager audioManager;

    public ScoreScript scoreScript;
    public PuckScript puckScript;
    public PlayerMovement playerMovement;
    public AiScript aiScript;
    public Portal portalScript;

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

    private IEnumerator ManagePortals()
    {
        yield return new WaitForSeconds(5f); // Initial delay before portals activate

        while (true)
        {
            ActivatePortals(); // Activate portals
            audioManager.PlayPortalActivation(); // Play portal activation audio

            yield return new WaitForSeconds(15f); // Portals remain active for 15 seconds

            DeactivatePortals(); // Deactivate portals

            yield return new WaitForSeconds(10f); // Portals remain inactive for 10 seconds
        }
    }

    private void ActivatePortals()
    {
        if (Portals != null)
            Portals.SetActive(true); // Activate the Portals GameObject
    }

    private void DeactivatePortals()
    {
        if (Portals != null)
            Portals.SetActive(false); // Deactivate the Portals GameObject
    }

    public void ShowRestartCanvas(bool didAiWin)
    {
        Time.timeScale = 0;

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

    public void RestartGame()
    {
        Time.timeScale = 1;

        CanvasGame.SetActive(true);  // Activate CanvasGame
        CanvasRestart.SetActive(false);  // Deactivate CanvasRestart

        // Reset game components
        scoreScript.ResetScores();
        puckScript.CenterPuck();
        playerMovement.ResetPosition();
        aiScript.ResetPosition();

        // Restart the coroutine for portal management
        StartCoroutine(ManagePortals());
    }
}
